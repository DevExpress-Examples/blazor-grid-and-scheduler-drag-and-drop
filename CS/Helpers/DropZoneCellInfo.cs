namespace blazor_grid_and_scheduler_drag_and_drop.Helpers;

public class DropZoneCellInfo {
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public bool IsAllDay { get; set; }
    
    public DropZoneCellInfo(DateTime start, DateTime end, bool isAllDay) {
        Start = start;
        End = end;
        IsAllDay = isAllDay;
    }
}
