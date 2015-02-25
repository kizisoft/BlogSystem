namespace BlogSystem.Web.ViewModels.BlogPost
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BlogSystem.Data.Models;
    using Helpers.Server;

    public class BlogPostIndexViewModel : BlogPostSimpleViewModel
    {
        [Display(Name = "Sub Title")]
        public string SubTitle { get; set; }

        [Display(Name = "Content")]
        public string Content
        {
            get { return this.SanitizedContent; }
            set { this.SanitizedContent = value; }
        }

        [Display(Name = "Autor")]
        public virtual ApplicationUser Autor { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public BlogPagerViewModel Pager { get; set; }
    }
}