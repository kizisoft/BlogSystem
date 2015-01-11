namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private const int PostsPerPageDefaultValue = 5;

        private readonly IRepository<BlogPost> blogPosts;

        public HomeController(IRepository<BlogPost> blogPosts)
        {
            this.blogPosts = blogPosts;
        }

        [HttpGet]
        public ActionResult Index(int page = 1, int perPage = PostsPerPageDefaultValue)
        {
            var pagesCount = (int)Math.Ceiling(this.blogPosts.All().Count() / (decimal)perPage);

            var blogPostsDb = this.blogPosts.All()
                .OrderByDescending(x => x.CreatedOn)
                .Skip(perPage * (page - 1))
                .Take(perPage)
                .Project()
                .To<BlogPostIndexViewModel>()
                .ToList();

            var indexViewModel = new IndexViewModel
            {
                BlogPosts = blogPostsDb,
                CurrentPage = page,
                PagesCount = pagesCount
            };

            return this.View(indexViewModel);
        }
    }
}