namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.Areas.Administration.ViewModels.BlogPost;

    public class BlogPostsController : AdminBaseController
    {
        private readonly IRepository<BlogPost> blogPosts;

        public BlogPostsController(IRepository<BlogPost> blogPosts)
        {
            this.blogPosts = blogPosts;
        }

        // GET: Administration/BlogPosts
        [HttpGet]
        public ActionResult Index()
        {
            var blogPosts = this.blogPosts.All().OrderByDescending(x => x.CreatedOn).Project().To<BlogPostSimpleViewModel>().ToList();
            return this.View(blogPosts);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var blogPost = this.blogPosts.All().Where(x => x.Id == id).Project().To<BlogPostViewModel>().FirstOrDefault();
            if (blogPost == null)
            {
                return this.HttpNotFound("No such Blog Post");
            }

            return this.View(blogPost);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogPostSimpleInputModel blogPost)
        {
            if (!ModelState.IsValid)
            {
                return this.View(blogPost);
            }

            this.blogPosts.Add(new BlogPost
            {
                Title = blogPost.Title,
                SubTitle = blogPost.SubTitle,
                ShortContent = blogPost.ShortContent,
                Content = blogPost.Content,
                MetaDescription = blogPost.MetaDescription,
                MetaKeywords = blogPost.MetaKeywords,
                IsCommentsDisabled = blogPost.IsCommentsDisabled
            });
            this.blogPosts.SaveChanges();

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var blogPost = this.blogPosts.All().Where(x => x.Id == id).Project().To<BlogPostInputModel>().FirstOrDefault();
            if (blogPost == null)
            {
                return this.HttpNotFound("No such Blog Post");
            }

            return this.View(blogPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogPostInputModel blogPost)
        {
            if (!ModelState.IsValid)
            {
                return this.View(blogPost);
            }

            var blogPostDb = this.blogPosts.GetById(blogPost.Id);
            if (blogPostDb == null)
            {
                return this.HttpNotFound("No such Blog Post");
            }

            blogPostDb.Title = blogPost.Title;
            blogPostDb.SubTitle = blogPost.SubTitle;
            blogPostDb.ShortContent = blogPost.ShortContent;
            blogPostDb.Content = blogPost.Content;
            blogPostDb.MetaDescription = blogPost.MetaDescription;
            blogPostDb.MetaKeywords = blogPost.MetaKeywords;
            blogPostDb.IsCommentsDisabled = blogPost.IsCommentsDisabled;
            blogPostDb.ModifiedOn = DateTime.Now;

            this.blogPosts.Update(blogPostDb);
            this.blogPosts.SaveChanges();

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var blogPost = this.blogPosts.All().Where(x => x.Id == id).Project().To<BlogPostViewModel>().FirstOrDefault();
            if (blogPost == null)
            {
                return this.HttpNotFound("No such Blog Post");
            }

            return this.View(blogPost);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var blogPostDb = this.blogPosts.GetById(id);
            if (blogPostDb == null)
            {
                return this.HttpNotFound("No such Blog Post");
            }

            this.blogPosts.Delete(blogPostDb);
            this.blogPosts.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}