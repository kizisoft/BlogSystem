namespace BlogSystem.Data.Models
{
    public class CommentsForPage : CommentForBase
    {
        public int PageId { get; set; }

        public virtual Page Page { get; set; }
    }
}