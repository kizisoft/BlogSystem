namespace BlogSystem.Web.Areas.Administration.ViewModels.Comments
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Areas.Administration.ViewModels.BlogPost;
    using BlogSystem.Web.Infrastructure.Mapping;
    using Helpers.Server;

    public class CommentsIndexViewModel : IPageable, IMapFrom<BlogPost>
    {
        public BlogPostSimpleViewModel BlogPost { get; set; }

        [Display(Name = "Comments")]
        public ICollection<CommentSimpleViewModel> Comments { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }
    }
}