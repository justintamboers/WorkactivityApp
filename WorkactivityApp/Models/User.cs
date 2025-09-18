using Microsoft.AspNetCore.Identity;

namespace WorkactivityApp.Models
{
    public class User : IdentityUser
    {
        public int? ProjectId { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
