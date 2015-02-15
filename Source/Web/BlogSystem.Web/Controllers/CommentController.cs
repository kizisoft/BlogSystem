namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.ViewModels.Comment;

    using Microsoft.AspNet.Identity;

    public class CommentController : BaseController
    {
        private const int CommentsPerPageDefaultValue = 10;

        private readonly IRepository<Comment> comments;

        public CommentController(IRepository<Comment> comments)
        {
            this.comments = comments;
        }

        // GET: Comment
        [HttpGet]
        public ActionResult Index(int blogPostId, int page = 1, int perPage = CommentsPerPageDefaultValue)
        {
            var pagesCount = (int)Math.Ceiling(this.comments.All().Count(x => x.BlogPostId == blogPostId) / (decimal)perPage);

            var commentsDb = this.comments.All()
                .Where(x => x.BlogPostId == blogPostId)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(perPage * (page - 1))
                .Take(perPage)
                .Project()
                .To<CommentViewModel>()
                .ToArray();

            var model = new CommentIndexViewModel
            {
                CommentInputModel = new CommentInputModel
                {
                    BlogPostId = blogPostId,
                    Content = ""
                },
                Comments = commentsDb,
                CurrentPage = page,
                PagesCount = pagesCount
            };

            return this.PartialView("_CommentPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentInputModel model)
        {
            if (ModelState.IsValid)
            {
                this.comments.Add(new Comment
                {
                    AutorId = this.User.Identity.GetUserId(),
                    BlogPostId = model.BlogPostId,
                    Content = model.Content
                });
                this.comments.SaveChanges();

                return this.Index(model.BlogPostId);
            }

            return this.JsonError("Enter your comment!");
        }
    }
}