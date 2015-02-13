namespace BlogSystem.Web.ViewModels.BlogPost
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class BlogPostIndexViewModel : IMapFrom<BlogPost>
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Sub Title")]
        public string SubTitle { get; set; }

        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        public virtual ApplicationUser Autor { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}