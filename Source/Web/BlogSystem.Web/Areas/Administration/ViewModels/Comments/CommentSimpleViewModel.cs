namespace BlogSystem.Web.Areas.Administration.ViewModels.Comments
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;
    using BlogSystem.Web.ViewModels;

    public class CommentSimpleViewModel : SanitizableContentBaseViewModel, IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content
        {
            get { return this.SanitizedContent; }
            set { this.SanitizedContent = value; }
        }

        public virtual ApplicationUser Autor { get; set; }

        [Display(Name = "Comment is visible")]
        public bool IsVisible { get; set; }

        [Display(Name = "Reason to hide")]
        public string ReasonToHide { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}