using System.ComponentModel.DataAnnotations;

namespace Social_Media_Platform.ViewModels
{
    public class PhotoViewModel
    {
        [Required(ErrorMessage ="Please provide a profile picture")]
        public string photoBase64 { get; set; }
    }
}
