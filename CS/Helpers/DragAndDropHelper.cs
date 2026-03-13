using Microsoft.JSInterop;
using System.Text.Json;

namespace blazor_grid_and_scheduler_drag_and_drop.Helpers;

public class DragAndDropHelper : IAsyncDisposable {
    private readonly IJSRuntime _jsRuntime;
    private IJSObjectReference? _module;
    private DotNetObjectReference<DragAndDropHelper>? _dotNetRef;

    public event Func<double, double, int?, Task>? OnDragMove;
    public event Func<double, double, int?, Task>? OnDragEnd;
    public event Func<double, double, int?, Task>? OnDragStart;
    public event Func<DropZoneCellInfo?, Task>? OnDragEnterDropZone;
    public event Func<Task>? OnDragLeaveDropZone;

    public bool IsDragging { get; private set; }

    public DragAndDropHelper(IJSRuntime jsRuntime) {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync() {
        _module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/drag-drop.js");
        _dotNetRef = DotNetObjectReference.Create(this);
        await _module.InvokeVoidAsync("initialize", _dotNetRef);
    }

    public async Task StartDragging(string handleSelector) {
        if(_module != null) {
            await _module.InvokeVoidAsync("startDragging", handleSelector);
        }
    }

    public async Task RegisterDropZone(string dropZoneSelector, string cellSelector) {
        if(_module != null) {
            await _module.InvokeVoidAsync("registerDropZone", dropZoneSelector, cellSelector);
        }
    }

    public async Task UpdateDragPreviewPosition(double x, double y, double width, double height, string backgroundColor) {
        if(_module != null) {
            await _module.InvokeVoidAsync("updateDragPreviewPosition", x, y, width, height, backgroundColor);
        }
    }

    [JSInvokable]
    public async Task NotifyDragStart(double x, double y, int? itemIndex) {
        IsDragging = true;
        if(OnDragStart != null) {
            await OnDragStart.Invoke(x, y, itemIndex);
        }
    }

    [JSInvokable]
    public async Task NotifyDragMove(double x, double y, int? itemIndex) {
        if(OnDragMove != null) {
            await OnDragMove.Invoke(x, y, itemIndex);
        }
    }

    [JSInvokable]
    public async Task NotifyDragEnd(double x, double y, int? itemIndex) {
        IsDragging = false;
        if(OnDragEnd != null) {
            await OnDragEnd.Invoke(x, y, itemIndex);
        }
    }

    [JSInvokable]
    public async Task NotifyDragEnterDropZone(string? cellInfoJson) {
        if(OnDragEnterDropZone != null) {
            DropZoneCellInfo? cellInfo = null;
            
            if(!string.IsNullOrEmpty(cellInfoJson)) {
                try {
                    var jsonDoc = JsonDocument.Parse(cellInfoJson);
                    var root = jsonDoc.RootElement;
                    
                    if(root.TryGetProperty("dataAttributes", out var dataAttributes)) {
                        DateTime start = default;
                        DateTime end = default;
                        bool isAllDay = false;
                        
                        if(dataAttributes.TryGetProperty("data-start", out var startProp)) {
                            var startStr = startProp.GetString();
                            if(!string.IsNullOrEmpty(startStr)) {
                                if(DateTime.TryParse(startStr, System.Globalization.CultureInfo.InvariantCulture, 
                                    System.Globalization.DateTimeStyles.RoundtripKind, out var parsedStart)) {
                                    start = parsedStart;
                                }
                                else if(long.TryParse(startStr, out var milliseconds)) {
                                    start = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;
                                }
                            }
                        }
                        
                        if(dataAttributes.TryGetProperty("data-end", out var endProp)) {
                            var endStr = endProp.GetString();
                            if(!string.IsNullOrEmpty(endStr)) {
                                if(DateTime.TryParse(endStr, System.Globalization.CultureInfo.InvariantCulture, 
                                    System.Globalization.DateTimeStyles.RoundtripKind, out var parsedEnd)) {
                                    end = parsedEnd;
                                }
                                else if(long.TryParse(endStr, out var milliseconds)) {
                                    end = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;
                                }
                            }
                        }
                        
                        isAllDay = dataAttributes.TryGetProperty("data-allday", out _);
                        
                        cellInfo = new DropZoneCellInfo(start, end, isAllDay);
                    }
                }
                catch(JsonException) {
                }
            }
            
            await OnDragEnterDropZone.Invoke(cellInfo);
        }
    }

    [JSInvokable]
    public async Task NotifyDragLeaveDropZone() {
        if(OnDragLeaveDropZone != null) {
            await OnDragLeaveDropZone.Invoke();
        }
    }

    public async ValueTask DisposeAsync() {
        if(_module != null) {
            await _module.InvokeVoidAsync("dispose");
            await _module.DisposeAsync();
        }
        _dotNetRef?.Dispose();
    }
}
