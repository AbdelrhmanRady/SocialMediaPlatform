using System.ComponentModel.DataAnnotations;

namespace Social_Media_Platform.ViewModels
{
    public class PostViewModel
    {
        [Required]
        public string Content { get; set; }

        public IEnumerable<IFormFile>? Files { get; set; }

    }
}
