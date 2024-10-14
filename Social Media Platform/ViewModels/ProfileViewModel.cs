using Social_Media_Platform.Models;
using System.ComponentModel.DataAnnotations;

namespace Social_Media_Platform.ViewModels
{
    public class ProfileViewModel
    {

        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LastName { get;set; }

        [EmailAddress]
        public string? Email { get; set; }

 

        [Required]
        public string City { get; set; }


        [Required(ErrorMessage = "Please Select Gender")]
        public Gender? Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        public string? Biography { get; set; }


        public string? ImagePath { get; set; }


    }
}
