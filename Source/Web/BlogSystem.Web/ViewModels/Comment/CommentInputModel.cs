namespace BlogSystem.Web.ViewModels.Comment
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CommentInputModel
    {
        [Required]
        public int BlogPostId { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_small")]
        public string Content { get; set; }
    }
}