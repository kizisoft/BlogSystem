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
        private readonly IRepository<Tag> tags;

        public HomeController(IRepository<BlogPost> blogPosts, IRepository<Page> pages, IRepository<Tag> tags)
        {
            this.blogPosts = blogPosts;
            this.pages = pages;
            this.tags = tags;
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

            return this.View("Index", indexViewModel);
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

        public ActionResult Tags(string tagName, int page = 1, int perPage = PostsPerPageDefaultValue)
        {
            if (string.IsNullOrEmpty(tagName))
            {
                return this.RedirectToAction("Index", "Home");
            }

            var tag = this.tags.All().Where(x => x.Name == tagName).FirstOrDefault();
            HomeIndexViewModel indexViewModel = null;
            if (tag != null)
            {
                var blogPosts = tag.BlogPosts;
                var pagesCount = (int)Math.Ceiling(blogPosts.Count() / (decimal)perPage);
                var blogPostsDb = blogPosts.OrderByDescending(x => x.CreatedOn)
                    .Skip(perPage * (page - 1))
                    .Take(perPage)
                    .AsQueryable()
                    .Project()
                    .To<HomeBlogPostViewModel>()
                    .ToList();

                indexViewModel = new HomeIndexViewModel
                {
                    BlogPosts = blogPostsDb,
                    CurrentPage = page,
                    TotalPages = pagesCount
                };
            }

            this.ViewBag.ArticlesForTag = tagName;
            return this.View("Index", indexViewModel);
        }
    }
}