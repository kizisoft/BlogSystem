namespace BlogSystem.Web.Areas.Administration.ViewModels.BlogPost
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Areas.Administration.ViewModels.Tag;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class BlogPostSimpleInputModel : IMapFrom<BlogPost>
    {
        [Required]
        [StringLength(300, MinimumLength = 5)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(300, MinimumLength = 5)]
        [Display(Name = "Sub Title")]
        public string SubTitle { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_full")]
        [Display(Name = "Short Content")]
        public string ShortContent { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_full")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [UIHint("TagsViewModel")]
        [Display(Name = "Tags")]
        public ICollection<TagViewModel> Tags { get; set; }

        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }

        [Display(Name = "Meta Keywords")]
        public string MetaKeywords { get; set; }

        [Display(Name = "Disable Comments")]
        public bool IsCommentsDisabled { get; set; }
    }
}