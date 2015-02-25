namespace BlogSystem.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.ViewModels.BlogPost;

    public class HomeBlogPostViewModel : BlogPostSimpleViewModel
    {
        [Display(Name = "Sub Title")]
        public string SubTitle { get; set; }

        [Display(Name = "Short Content")]
        public string ShortContent
        {
            get { return this.SanitizedContent; }
            set { this.SanitizedContent = value; }
        }

        [Display(Name = "Autor")]
        public virtual ApplicationUser Autor { get; set; }
    }
}