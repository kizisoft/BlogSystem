namespace BlogSystem.Web.Areas.Administration.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;
    using BlogSystem.Web.ViewModels;

    public class CommentInputModel : SanitizableContentBaseViewModel, IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int BlogPostId { get; set; }

        [AllowHtml]
        [UIHint("tinymce_small")]
        public string Content
        {
            get { return this.SanitizedContent; }
            set { this.SanitizedContent = value; }
        }

        [Display(Name = "Comment is visible")]
        public bool IsVisible { get; set; }

        [Display(Name = "Reason to hide")]
        public string ReasonToHide { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentInputModel>().ForMember(m => m.BlogPostId, o => o.MapFrom(x => x.BlogPost.Id));
        }
    }
}