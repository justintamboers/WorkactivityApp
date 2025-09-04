namespace WorkactivityApp.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

}
