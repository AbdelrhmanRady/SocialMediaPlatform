using System.ComponentModel.DataAnnotations;

namespace Social_Media_Platform.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }


    }
}
