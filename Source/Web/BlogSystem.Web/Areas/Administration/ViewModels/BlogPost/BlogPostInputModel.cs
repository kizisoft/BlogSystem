namespace BlogSystem.Web.Areas.Administration.ViewModels.BlogPost
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using BlogSystem.Data.Models;

    public class BlogPostInputModel : BlogPostSimpleInputModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Deleted On")]
        public DateTime? DeletedOn { get; set; }

        [Display(Name = "Comments")]
        public int CommentsCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<BlogPost, BlogPostInputModel>().ForMember(m => m.CommentsCount, o => o.MapFrom(x => x.Comments.Count));
        }
    }
}