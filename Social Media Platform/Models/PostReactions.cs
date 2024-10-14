using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media_Platform.Models
{
    public class PostReactions
    {
        public string UserId { get; set; }


        public int PostId { get; set; }

        public ApplicationUser User { get; set; }

    }
}
