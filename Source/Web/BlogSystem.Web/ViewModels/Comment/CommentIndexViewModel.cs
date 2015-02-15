namespace BlogSystem.Web.ViewModels.Comment
{
    using System.Collections.Generic;

    public class CommentIndexViewModel : PageableBaseViewModel
    {
        public CommentInputModel CommentInputModel { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}