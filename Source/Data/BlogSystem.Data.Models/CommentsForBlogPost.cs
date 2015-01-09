namespace BlogSystem.Data.Models
{
    public class CommentsForBlogPost : CommentForBase
    {
        public int BlogPostId { get; set; }

        public virtual BlogPost BlogPost { get; set; }
    }
}