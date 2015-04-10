namespace BlogSystem.Web.Areas.Administration.ViewModels.Page
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PageViewModel : PageSimpleViewModel
    {
        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Permalink")]
        public string Permalink { get; set; }

        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }

        [Display(Name = "Meta Keywords")]
        public string MetaKeywords { get; set; }
    }
}