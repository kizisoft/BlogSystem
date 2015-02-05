namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.ViewModels.BlogPost;
    using BlogSystem.Web.ViewModels.Sidebar;

    public class SidebarController : BaseController
    {
        private readonly IRepository<BlogPost> blogPosts;
        private readonly IRepository<Tag> tags;

        public SidebarController(IRepository<BlogPost> blogPosts, IRepository<Tag> tags)
        {
            this.blogPosts = blogPosts;
            this.tags = tags;   // TODO: make it work with tags
        }

        // GET: Sidebar
        [HttpGet]
        [ChildActionOnly]
        //// [OutputCache(Duration = 10 * 60)]
        public ActionResult Index()
        {
            var model = new SidebarViewModel
            {
                BlogPosts = this.blogPosts.All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(10)
                .Project()
                .To<BlogPostSimpleViewModel>()
                .ToArray()
            };

            return this.PartialView("_SidebarPartial", model);
        }
    }
}