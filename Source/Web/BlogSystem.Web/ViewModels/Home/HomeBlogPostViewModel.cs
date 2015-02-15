namespace BlogSystem.Web.ViewModels.BlogPost
{
    using System.ComponentModel.DataAnnotations;

    using BlogSystem.Data.Models;

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