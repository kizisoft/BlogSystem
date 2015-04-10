namespace BlogSystem.Web.Areas.Administration.ViewModels.Page
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class PageSimpleInputModel : IMapFrom<Page>
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_full")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Permalink")]
        public string Permalink { get; set; }

        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }

        [Display(Name = "Meta Keywords")]
        public string MetaKeywords { get; set; }
    }
}