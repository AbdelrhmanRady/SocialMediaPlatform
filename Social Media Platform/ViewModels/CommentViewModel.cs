namespace Social_Media_Platform.ViewModels
{
    public class CommentViewModel
    {
        public string CommentContent { get; set; }

        public int? PostId { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
