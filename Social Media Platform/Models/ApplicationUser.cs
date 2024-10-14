using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Social_Media_Platform.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender gender { get; set; }
        public string City { get; set; }

        public DateTime Birthday { get; set; }

        public string? ImagePath { get; set; }

        public string? Biography { get; set; }

        public List<Posts> Posts { get; set; }
    }
}
