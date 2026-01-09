namespace blazor_scheduler_drag_and_drop_inside_scheduler_from_grid.Data;

public class DataService
{
    private List<AppointmentData> _appointments = new();
    private List<GridItemData> _gridItems = new();
    private int _appointmentIdCounter = 1;
    private int _gridItemIdCounter = 1;

    public DataService()
    {
        InitializeData();
    }

    public List<AppointmentData> GetAppointments() => _appointments;
    public List<GridItemData> GetGridItems() => _gridItems;

    public void AddAppointment(AppointmentData appointment)
    {
        appointment.Id = _appointmentIdCounter++;
        _appointments.Add(appointment);
    }

    private void InitializeData()
    {
        var today = DateTime.Today;

        _appointments = new List<AppointmentData>
        {
            new() { Id = _appointmentIdCounter++, Subject = "Team Meeting", Description = "Weekly sync meeting", StartDate = today.AddHours(9), EndDate = today.AddHours(10), AllDay = false },
            new() { Id = _appointmentIdCounter++, Subject = "Project Review", Description = "Q1 project review", StartDate = today.AddDays(1).AddHours(14), EndDate = today.AddDays(1).AddHours(15.5), AllDay = false },
            new() { Id = _appointmentIdCounter++, Subject = "Client Call", Description = "Discussion with client", StartDate = today.AddDays(-1).AddHours(11), EndDate = today.AddDays(-1).AddHours(12), AllDay = false },
            new() { Id = _appointmentIdCounter++, Subject = "Code Review", Description = "Review pull requests", StartDate = today.AddHours(15), EndDate = today.AddHours(16), AllDay = false },
            new() { Id = _appointmentIdCounter++, Subject = "Design Workshop", Description = "UI/UX workshop", StartDate = today.AddDays(2).AddHours(10), EndDate = today.AddDays(2).AddHours(12), AllDay = false },
            new() { Id = _appointmentIdCounter++, Subject = "Sprint Planning", Description = "Plan next sprint", StartDate = today.AddDays(3).AddHours(9), EndDate = today.AddDays(3).AddHours(11), AllDay = false },
            new() { Id = _appointmentIdCounter++, Subject = "Training Session", Description = "Team training", StartDate = today.AddDays(-2).AddHours(13), EndDate = today.AddDays(-2).AddHours(15), AllDay = false },
            new() { Id = _appointmentIdCounter++, Subject = "Lunch Break", Description = "Team lunch", StartDate = today.AddDays(4).AddHours(12), EndDate = today.AddDays(4).AddHours(13), AllDay = false },
            new() { Id = _appointmentIdCounter++, Subject = "Standup", Description = "Daily standup", StartDate = today.AddDays(-3).AddHours(9.5), EndDate = today.AddDays(-3).AddHours(10), AllDay = false },
            new() { Id = _appointmentIdCounter++, Subject = "Demo Preparation", Description = "Prepare demo", StartDate = today.AddDays(5).AddHours(14), EndDate = today.AddDays(5).AddHours(16), AllDay = false },
            new() { Id = _appointmentIdCounter++, Subject = "Conference", Description = "Tech conference", StartDate = today.AddDays(7), EndDate = today.AddDays(7), AllDay = true },
            new() { Id = _appointmentIdCounter++, Subject = "Architecture Discussion", Description = "System architecture review", StartDate = today.AddDays(-4).AddHours(10), EndDate = today.AddDays(-4).AddHours(11.5), AllDay = false },
            new() { Id = _appointmentIdCounter++, Subject = "Performance Review", Description = "1-on-1 review", StartDate = today.AddDays(6).AddHours(15), EndDate = today.AddDays(6).AddHours(16), AllDay = false },
            new() { Id = _appointmentIdCounter++, Subject = "Bug Triage", Description = "Weekly bug review", StartDate = today.AddDays(-5).AddHours(11), EndDate = today.AddDays(-5).AddHours(12), AllDay = false },
            new() { Id = _appointmentIdCounter++, Subject = "Release Planning", Description = "Plan next release", StartDate = today.AddDays(8).AddHours(13), EndDate = today.AddDays(8).AddHours(15), AllDay = false }
        };

        _gridItems = new List<GridItemData>
        {
            new() { Id = _gridItemIdCounter++, Title = "Website Redesign", Category = "Design", Description = "Redesign company website", Priority = 1 },
            new() { Id = _gridItemIdCounter++, Title = "API Development", Category = "Development", Description = "Build REST API", Priority = 2 },
            new() { Id = _gridItemIdCounter++, Title = "Database Migration", Category = "Infrastructure", Description = "Migrate to cloud database", Priority = 1 },
            new() { Id = _gridItemIdCounter++, Title = "User Testing", Category = "QA", Description = "Conduct user acceptance testing", Priority = 3 },
            new() { Id = _gridItemIdCounter++, Title = "Documentation Update", Category = "Documentation", Description = "Update technical docs", Priority = 2 },
            new() { Id = _gridItemIdCounter++, Title = "Security Audit", Category = "Security", Description = "Perform security review", Priority = 1 },
            new() { Id = _gridItemIdCounter++, Title = "Mobile App", Category = "Development", Description = "Develop mobile application", Priority = 2 },
            new() { Id = _gridItemIdCounter++, Title = "Marketing Campaign", Category = "Marketing", Description = "Launch new campaign", Priority = 3 },
            new() { Id = _gridItemIdCounter++, Title = "Customer Survey", Category = "Research", Description = "Gather customer feedback", Priority = 2 },
            new() { Id = _gridItemIdCounter++, Title = "Performance Optimization", Category = "Development", Description = "Optimize application performance", Priority = 1 },
            new() { Id = _gridItemIdCounter++, Title = "CI/CD Pipeline", Category = "DevOps", Description = "Set up continuous integration and deployment", Priority = 1 },
            new() { Id = _gridItemIdCounter++, Title = "Load Testing", Category = "QA", Description = "Perform load and stress testing", Priority = 2 },
            new() { Id = _gridItemIdCounter++, Title = "Email Template Design", Category = "Design", Description = "Create responsive email templates", Priority = 3 },
            new() { Id = _gridItemIdCounter++, Title = "Analytics Integration", Category = "Development", Description = "Integrate analytics tracking", Priority = 2 },
            new() { Id = _gridItemIdCounter++, Title = "Backup Strategy", Category = "Infrastructure", Description = "Implement automated backup solution", Priority = 1 },
            new() { Id = _gridItemIdCounter++, Title = "Accessibility Review", Category = "QA", Description = "Ensure WCAG compliance", Priority = 2 }
        };
    }
}
