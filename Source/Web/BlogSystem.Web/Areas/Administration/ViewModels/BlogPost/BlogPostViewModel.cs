namespace BlogSystem.Web.Areas.Administration.ViewModels.BlogPost
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class BlogPostViewModel : BlogPostSimpleViewModel
    {
        [AllowHtml]
        [Display(Name = "Short Content")]
        public string ShortContent { get; set; }

        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }

        [Display(Name = "Meta Keywords")]
        public string MetaKeywords { get; set; }

        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Deleted On")]
        public DateTime? DeletedOn { get; set; }
    }
}