namespace BlogSystem.Web.Areas.Administration.ViewModels.BlogPost
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class BlogPostSimpleViewModel : IMapFrom<BlogPost>
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Sub Title")]
        public string SubTitle { get; set; }

        [Display(Name = "Disable Comments")]
        public bool IsCommentsDisabled { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedOn { get; set; }
    }
}