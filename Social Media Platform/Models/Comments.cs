using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media_Platform.Models
{
    public class Comments
    {
        [Key]
        public int CommentId { get; set; }

        public string CommentContent { get; set; }

        public int PostId { get; set; }


        public string CommenterId { get; set; }


        public DateTime CreatedAt { get; set; }

        public int ParentCommentId { get; set; }

        //Navigation Properties
        public List<Comments> SubComments { get; set; } =  new List<Comments>();

        public ApplicationUser User { get; set; }

        public Posts Post { get; set; }
    }
}
