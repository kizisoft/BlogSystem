namespace BlogSystem.Web.ViewModels.BlogPost
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.BlogURL;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class BlogPostHomeViewModel : BlogPostSimpleViewModel, IMapFrom<BlogPost>
    {
        [Display(Name = "Sub Title")]
        public string SubTitle { get; set; }

        [AllowHtml]
        [Display(Name = "Short Content")]
        public string ShortContent { get; set; }

        [Required]
        [Display(Name = "Autor")]
        public virtual ApplicationUser Autor { get; set; }
    }
}