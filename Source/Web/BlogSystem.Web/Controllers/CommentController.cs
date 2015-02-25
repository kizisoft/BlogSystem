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
        private const int MaxVisiblePagesDefaultValue = 5;

        private readonly IRepository<Comment> comments;

        public CommentController(IRepository<Comment> comments)
        {
            this.comments = comments;
        }

        // GET: Comment
        [HttpGet]
        public ActionResult Index(int id, int page = 1, int perPage = CommentsPerPageDefaultValue)
        {
            var pagesCount = (int)Math.Ceiling(this.comments.All().Count(x => x.BlogPostId == id) / (decimal)perPage);

            var commentsDb = this.comments.All()
                .Where(x => x.BlogPostId == id)
                .OrderBy(x => x.CreatedOn)
                .Skip(perPage * (page - 1))
                .Take(perPage)
                .Project()
                .To<CommentViewModel>()
                .ToArray();

            var model = new CommentIndexViewModel
            {
                BlogPostId = id,
                HasComments = commentsDb.Count() > 0,
                Comments = commentsDb,
                MaxVisiblePages = MaxVisiblePagesDefaultValue,
                CurrentPage = page,
                PagesCount = pagesCount
            };

            return this.PartialView("Index", model);
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
                    BlogPostId = model.Id,
                    Content = model.Content
                });
                this.comments.SaveChanges();

                var pagesCount = (int)Math.Ceiling(this.comments.All().Count(x => x.BlogPostId == model.Id) / (decimal)CommentsPerPageDefaultValue);

                return this.Index(model.Id, pagesCount);
            }

            return this.JsonError("Enter your comment!");
        }
    }
}