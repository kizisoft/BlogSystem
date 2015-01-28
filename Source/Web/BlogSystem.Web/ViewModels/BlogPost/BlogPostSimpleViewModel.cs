namespace BlogSystem.Web.ViewModels.BlogPost
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

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
    }
}