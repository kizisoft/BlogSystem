namespace BlogSystem.Web.ViewModels.Home
{
    using System;
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
        [Display(Name = "Short Content")]
        public string ShortContent { get; set; }

        [Required]
        public virtual ApplicationUser Autor { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
    }
}