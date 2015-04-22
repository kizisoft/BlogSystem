namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.Areas.Administration.ViewModels.BlogPost;
    using BlogSystem.Web.Areas.Administration.ViewModels.Tag;
    using BlogSystem.Web.Infrastructure.Identity;

    public class BlogPostsController : AdminBaseController
    {
        private readonly IBlogSystemData data;
        private readonly ICurrentUser currentUser;

        public BlogPostsController(IBlogSystemData data, ICurrentUser currentUser)
        {
            this.data = data;
            this.currentUser = currentUser;
        }

        // GET: Administration/BlogPosts
        [HttpGet]
        public ActionResult Index()
        {
            var blogPosts = this.data.BlogPosts.All().OrderByDescending(x => x.CreatedOn).Project().To<BlogPostSimpleViewModel>().ToList();
            return this.View(blogPosts);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var blogPost = this.data.BlogPosts.All().Where(x => x.Id == id).Project().To<BlogPostViewModel>().FirstOrDefault();
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

            var user = this.currentUser.Get();

            this.data.BlogPosts.Add(new BlogPost
            {
                Title = blogPost.Title,
                SubTitle = blogPost.SubTitle,
                ShortContent = blogPost.ShortContent,
                Content = blogPost.Content,
                MetaDescription = blogPost.MetaDescription,
                MetaKeywords = blogPost.MetaKeywords,
                IsCommentsDisabled = blogPost.IsCommentsDisabled,
                AutorId = user.Id
            });
            this.data.BlogPosts.SaveChanges();

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var blogPost = this.data.BlogPosts.All().Where(x => x.Id == id).Project().To<BlogPostInputModel>().FirstOrDefault();
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

            var tagsDb = this.data.Tags.All().ToList();
            var blogPostDb = this.data.BlogPosts.GetById(blogPost.Id);
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

            this.ManageTags(blogPostDb, blogPost.Tags);
            this.data.BlogPosts.Update(blogPostDb);
            this.data.BlogPosts.SaveChanges();

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var blogPost = this.data.BlogPosts.All().Where(x => x.Id == id).Project().To<BlogPostViewModel>().FirstOrDefault();
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
            var blogPostDb = this.data.BlogPosts.GetById(id);
            if (blogPostDb == null)
            {
                return this.HttpNotFound("No such Blog Post");
            }

            this.data.BlogPosts.Delete(blogPostDb);
            this.data.BlogPosts.SaveChanges();

            return this.RedirectToAction("Index");
        }

        private void ManageTags(BlogPost blogPost, ICollection<TagViewModel> tags)
        {
            var tagsDb = this.data.Tags.All().ToList();
            var tagsDbNames = tagsDb.Select(x => x.Name);
            var tagsBlogPostNames = blogPost.Tags.Select(x => x.Name);
            var newTagsToAdd = tags.Where(x => !tagsDbNames.Contains(x.Name)).Select(x => new Tag { Name = x.Name });
            var existingTagsToAdd = tags.Where(x => !tagsBlogPostNames.Contains(x.Name)).Select(x => tagsDb.First(t => t.Name == x.Name));
            var tagsToAdd = newTagsToAdd.Union(existingTagsToAdd);
            foreach (var tag in tagsToAdd)
            {
                blogPost.Tags.Add(tag);
            }

            var tagNames = tags.Select(x => x.Name);
            List<Tag> tagsToRemove = new List<Tag>(blogPost.Tags.Where(x => !tagNames.Contains(x.Name)));
            for (int i = 0; i < tagsToRemove.Count(); i++)
            {
                blogPost.Tags.Remove(tagsToRemove[i]);
            }
        }
    }
}