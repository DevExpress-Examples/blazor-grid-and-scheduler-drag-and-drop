namespace blazor_scheduler_drag_and_drop_inside_scheduler_from_grid.Data;

public class AppointmentData
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool AllDay { get; set; }
    public string? RecurrenceInfo { get; set; }
}
