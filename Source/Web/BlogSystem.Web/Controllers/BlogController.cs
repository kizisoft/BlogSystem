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
            var blogPost = this.blogPosts.All()
                .Where(x => x.Id == id)
                .Project()
                .To<BlogPostIndexViewModel>()
                .FirstOrDefault();

            if (blogPost == null)
            {
                return this.HttpNotFound("No such Blog Post");
            }

            var blogPostPrev = this.blogPosts.All()
                .Where(x => x.CreatedOn <= blogPost.CreatedOn && x.Id != blogPost.Id)
                .OrderByDescending(x => x.CreatedOn)
                .Project()
                .To<BlogPostSimpleViewModel>()
                .FirstOrDefault();

            var blogPostNext = this.blogPosts.All()
                .Where(x => x.CreatedOn >= blogPost.CreatedOn && x.Id != blogPost.Id)
                .OrderBy(x => x.CreatedOn)
                .Project()
                .To<BlogPostSimpleViewModel>()
                .FirstOrDefault();

            var model = new BlogViewModel { BlogPost = blogPost, Previous = blogPostPrev, Next = blogPostNext };

            return this.View(model);
        }
    }
}