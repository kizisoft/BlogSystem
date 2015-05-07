namespace BlogSystem.Web.ViewModels.Comment
{
    using System.Collections.Generic;

    using Helpers.Server;

    public class CommentIndexViewModel : IPageable
    {
        public bool HasComments { get; set; }

        public int Id { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public bool IsCommentsDisabled { get; set; }
    }
}