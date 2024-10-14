using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media_Platform.Models
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }
        public string PostContent { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

        public List<PostMedia> Media { get; set; }
        public List<Comments> Comments { get; set; }
        public List<PostReactions> Reactions { get; set; }

        public ApplicationUser User { get; set; }


        

    }
}
