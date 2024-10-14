using System.ComponentModel.DataAnnotations;

namespace Social_Media_Platform.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and ConfirmPassword must match")]
        public string ConfirmPassword { get; set;}
    }
}
