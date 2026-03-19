<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/1122703000/25.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1318444)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# Blazor Grid and Scheduler - Drag & Drop Items Between Components

This example demonstrates drag & drop functionality that allows you to move items from the [DevExpress Blazor Grid](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxGrid) to the [DevExpress Blazor Scheduler](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxScheduler). While Grid and Scheduler components support drag & drop operations within their own boundaries, this example implements a custom drag-and-drop functionality between the components.

![Drag and Drop Functionality](grid-scheduler-drag-and-drop.png)

## Main Page Structure

See [Index.razor](./CS/BlazorSchedulerDragAndDropInsideSchedulerFromGrid/Components/Pages/Index.razor).

- The [DragDropProvider](#drag-and-drop-provider) component wraps [Grid](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxGrid) and [Scheduler](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxScheduler) components and implements drag & drop logic.
- The [DevExpress Blazor Stack Layout](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxStackLayout) component arranges [Grid](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxGrid) and [Scheduler](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxScheduler) components on a page.

### Blazor Grid Configuration

See [Blazor Grid Markup](./CS/Components/Pages/Index.razor#L24).

- A `DxGridCommandColumn` template renders a drag handle.
- The `drag-handle` CSS class is used to locate the handle in JavaScript.
- `data-item-index="@context.VisibleIndex"` identifies the dragged row.
- `CssClass="grid-container"` allows JavaScript to locate the grid.

### Blazor Scheduler Configuration

See [Blazor Scheduler Markup](./CS/Components/Pages/Index.razor#L50).

- `CssClass="scheduler-container drop-zone"` is used to detect the drop zone in JavaScript.

## Drag & Drop Implementation

### JavaScript Logic

See [drag-drop.js](./CS/wwwroot/js/drag-drop.js).

Main event handlers:

* [handleMouseDown](./CS/wwwroot/js/drag-drop.js#L38) - Handles the start of a drag operation.
* [handleMouseMove](./CS/wwwroot/js/drag-drop.js#L59) - Updates the drag preview position and highlights drop zones.
* [handleMouseUp](./CS/wwwroot/js/drag-drop.js#L167) - Finalizes the drag operation and processes the drop.

### C# Logic

See [Index.razor](./CS/Components/Pages/Index.razor#L105).

Main event handlers:

- [HandleDragStart](./CS/Components/Pages/Index.razor#L101) - Stores dragged item data and renders the drag preview.
- [HandleDragEnterDropZone](./CS/Components/Pages/Index.razor#L130) - Stores target cell information.
- [HandleDragLeaveDropZone](./CS/Components/Pages/Index.razor#L134) - Clears target cell information.
- [HandleDragEnd](./CS/Components/Pages/Index.razor#L122) - Handles the drop event.
- [CreateAppointmentFromGrid](./CS/Components/Pages/Index.razor#L138) - Creates a new appointment from the dragged item data.

### Drag-and-Drop Provider

See [DragDropProvider.razor](./CS/Components/DragDropProvider.razor).

- The provider stores [DragAndDropHelper](#drag-and-drop-helper) and a drag template, registers required JavaScript/C# events, and exposes event handlers via parameters.
- Both the Grid and Scheduler [are wrapped](./CS/Components/Pages/Index.razor#L11) within this provider.

### Drag-and-Drop Helper

See [DragAndDropHelper.cs](./CS/Helpers/DragAndDropHelper.cs).


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=blazor-grid-and-scheduler-drag-and-drop&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=blazor-grid-and-scheduler-drag-and-drop&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
