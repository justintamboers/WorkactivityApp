namespace WorkactivityApp.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public Time Time { get; set; } = new Time();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
