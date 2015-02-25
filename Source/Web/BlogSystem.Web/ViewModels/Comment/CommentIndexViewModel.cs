namespace BlogSystem.Web.ViewModels.Comment
{
    using System.Collections.Generic;
    using Helpers.Server;

    public class CommentIndexViewModel : PageableBase
    {
        public bool HasComments { get; set; }

        public int BlogPostId { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}