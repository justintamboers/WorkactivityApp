namespace WorkactivityApp.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public Time Time { get; set; } = new Time();
    }
}
