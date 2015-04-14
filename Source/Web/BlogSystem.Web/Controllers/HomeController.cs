namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.ViewModels.BlogPost;
    using BlogSystem.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private const int PostsPerPageDefaultValue = 5;

        private readonly IRepository<BlogPost> blogPosts;
        private readonly IRepository<Page> pages;

        public HomeController(IRepository<BlogPost> blogPosts, IRepository<Page> pages)
        {
            this.blogPosts = blogPosts;
            this.pages = pages;
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
                .To<HomeBlogPostViewModel>()
                .ToList();

            var indexViewModel = new HomeIndexViewModel
            {
                BlogPosts = blogPostsDb,
                CurrentPage = page,
                TotalPages = pagesCount
            };

            return this.View(indexViewModel);
        }

        [ChildActionOnly]
        ////[OutputCache(Duration = 6 * 10 * 60)]
        public ActionResult Menu()
        {
            var menuItems = this.pages
                .All()
                .Where(p => !p.IsDeleted)
                .Project().To<MenuItemViewModel>()
                .ToList();

            return this.PartialView("_Menu", menuItems);
        }
    }
}