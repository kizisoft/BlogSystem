﻿namespace BlogSystem.Web.ViewModels.BlogPost
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.BlogURL;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class BlogPostSimpleViewModel : SanitizableContentBaseViewModel, IMapFrom<BlogPost>, IHaveCustomMappings
    {
        private IBlogUrlGenerator blogUrlGenerator;

        public BlogPostSimpleViewModel()
        {
            this.blogUrlGenerator = (IBlogUrlGenerator)DependencyResolver.Current.GetService(typeof(IBlogUrlGenerator));
        }

        public BlogPostSimpleViewModel(IBlogUrlGenerator blogUrlGenerator)
        {
            this.blogUrlGenerator = blogUrlGenerator;
        }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "URL")]
        public string Url
        {
            get
            {
                return this.blogUrlGenerator.GenerateUrl(this.Id, this.Title, this.CreatedOn);
            }
        }

        public IEnumerable<Tag> Tags { get; set; }

        public int CommentsCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<BlogPost, BlogPostSimpleViewModel>().ForMember(m => m.CommentsCount, o => o.MapFrom(x => x.Comments.Count));
        }
    }
}