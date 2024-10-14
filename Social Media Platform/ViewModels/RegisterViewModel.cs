using Social_Media_Platform.Models;
using System.ComponentModel.DataAnnotations;

namespace Social_Media_Platform.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [MaxLength(25)]
        [Display(Name ="FIIIII")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LastName { get;set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        public string City { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("password", ErrorMessage = "Password and confirm Password must match")]
        public string confirmPassword { get; set; }

        [Required(ErrorMessage = "Please Select Gender")]
        public Gender? Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        public string? Biography { get; set; }


        public IFormFile? Photo { get; set; }


    }
}
