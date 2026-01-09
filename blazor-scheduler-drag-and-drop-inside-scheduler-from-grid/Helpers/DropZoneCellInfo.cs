namespace blazor_scheduler_drag_and_drop_inside_scheduler_from_grid.Helpers;

public class DropZoneCellInfo
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public bool IsAllDay { get; set; }
    
    public DropZoneCellInfo()
    {
    }
    
    public DropZoneCellInfo(DateTime start, DateTime end, bool isAllDay)
    {
        Start = start;
        End = end;
        IsAllDay = isAllDay;
    }
}
