using Microsoft.AspNetCore.Identity;

namespace WorkactivityApp.Models
{
    public class User : IdentityUser
    {
        public string? ProjectId { get; set; }
    }
}
