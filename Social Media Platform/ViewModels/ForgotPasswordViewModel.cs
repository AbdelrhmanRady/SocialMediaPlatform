using System.ComponentModel.DataAnnotations;

namespace Social_Media_Platform.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
