using Social_Media_Platform.Models;

namespace Social_Media_Platform.ViewModels
{
    public class PostPageViewModel
    {
        public PostViewModel? PostViewModel { get; set; }
        public List<Posts>? Posts { get; set; }

        public CommentViewModel? CommentViewModel { get; set; }
    }
}
