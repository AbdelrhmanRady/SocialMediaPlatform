using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media_Platform.Models
{
    public class PostMedia
    {
        public int PostId { get; set; }


        public string MediaLocation { get; set; }
    }
}
