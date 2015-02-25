namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.ViewModels.BlogPost;

    public class BlogController : BaseController
    {
        private readonly IRepository<BlogPost> blogPosts;

        public BlogController(IRepository<BlogPost> blogPosts)
        {
            this.blogPosts = blogPosts;
        }

        // GET: BlogPost
        [HttpGet]
        public ActionResult Index(int? id, int? page)
        {
            var model = this.blogPosts.All()
                .Where(x => x.Id == id)
                .Project()
                .To<BlogPostIndexViewModel>()
                .FirstOrDefault();

            if (model == null)
            {
                return this.HttpNotFound("No such Blog Post");
            }

            var prev = this.blogPosts.All()
                .Where(x => x.CreatedOn <= model.CreatedOn && x.Id != model.Id)
                .OrderByDescending(x => x.CreatedOn)
                .Take(1)
                .Project()
                .To<BlogPostSimpleViewModel>()
                .FirstOrDefault();

            var next = this.blogPosts.All()
                .Where(x => x.CreatedOn >= model.CreatedOn && x.Id != model.Id)
                .OrderBy(x => x.CreatedOn)
                .Take(1)
                .Project()
                .To<BlogPostSimpleViewModel>()
                .FirstOrDefault();

            model.Pager = new BlogPagerViewModel(prev, next);

            return this.View(model);
        }
    }
}