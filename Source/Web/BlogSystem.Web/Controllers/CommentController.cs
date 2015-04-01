namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.ViewModels.Comment;

    using Microsoft.AspNet.Identity;

    public class CommentController : BaseController
    {
        private const int CommentsPerPageDefaultValue = 7;

        private readonly IRepository<Comment> comments;
        private readonly IRepository<ApplicationUser> users;
        private readonly IRepository<VoteUp> votesUp;
        private readonly IRepository<VoteDown> votesDown;

        public CommentController(IRepository<Comment> comments, IRepository<ApplicationUser> users, IRepository<VoteUp> votesUp, IRepository<VoteDown> votesDown)
        {
            this.comments = comments;
            this.users = users;
            this.votesUp = votesUp;
            this.votesDown = votesDown;
        }

        // GET: Comment
        [HttpGet]
        public ActionResult All(int id, int page = 1, int perPage = 0)
        {
            var model = this.GetCommentIndexViewModel(id, page, perPage);
            return this.PartialView("All", model);
        }

        [HttpGet]
        public ActionResult Items(int id, int page = 1, int perPage = 0)
        {
            var model = this.GetCommentIndexViewModel(id, page, perPage);
            return this.PartialView("_ItemsPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CommentInputModel model)
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

                var pagesCount = (int)Math.Ceiling(this.comments.All().Count(x => x.BlogPostId == model.Id) / decimal.Parse(this.ViewBag.Settings.Get["Comments Per Page"]));
                return this.All(model.Id, pagesCount);
            }

            return this.JsonError(HttpStatusCode.BadRequest, "Enter your comment!");
        }

        [HttpPost]
        //// [ValidateAntiForgeryToken]
        public ActionResult VoteUp(int id)
        {
            return this.Vote<VoteUp>(id, this.votesUp, 1);
        }

        [HttpPost]
        //// [ValidateAntiForgeryToken]
        public ActionResult VoteDown(int id)
        {
            return this.Vote<VoteDown>(id, this.votesDown, -1);
        }

        private CommentIndexViewModel GetCommentIndexViewModel(int id, int page, int perPage)
        {
            perPage = perPage > 0 ? perPage : int.Parse(this.ViewBag.Settings.Get["Comments Per Page"]);
            var totalPages = (int)Math.Ceiling(this.comments.All().Count(x => x.BlogPostId == id) / (decimal)perPage);

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
                Id = id,
                HasComments = commentsDb.Count() > 0,
                Comments = commentsDb,
                CurrentPage = page,
                TotalPages = totalPages
            };
            return model;
        }

        private ActionResult Vote<T>(int id, IRepository<T> votes, int factor) where T : Vote, new()
        {
            var commentDb = this.comments.All()
               .Where(x => x.Id == id)
               .FirstOrDefault();

            if (commentDb == null)
            {
                return this.JsonError(HttpStatusCode.NotFound, "Comment with such ID does not existe!");
            }

            if (this.User.Identity.IsAuthenticated)
            {
                var userDb = this.users.GetById(this.User.Identity.GetUserId());
                if (userDb.Id == commentDb.Autor.Id)
                {
                    return this.JsonError(HttpStatusCode.Forbidden, "You can not vote for your comment!");
                }

                var autorDb = this.users.GetById(commentDb.Autor.Id);
                autorDb.Points += this.DoVote(id, userDb.Id, votes, factor);
                this.users.Update(autorDb);
                this.users.SaveChanges();

                var model = Mapper.Map<Comment, CommentViewModel>(commentDb);
                return this.PartialView("_VotePartial", model);
            }

            return this.JsonError(HttpStatusCode.BadRequest, "You must be logged in to vote for comment!");
        }

        private int DoVote<T>(int commentId, string userId, IRepository<T> votes, int factor) where T : Vote, new()
        {
            var voteUp = this.GetVote(commentId, userId, this.votesUp);
            var voteDown = this.GetVote(commentId, userId, this.votesDown);

            if (voteUp != null)
            {
                this.votesUp.Delete(voteUp);
                this.votesUp.SaveChanges();
                return -1;
            }

            if (voteDown != null)
            {
                this.votesDown.Delete(voteDown);
                this.votesDown.SaveChanges();
                return 1;
            }

            votes.Add(new T
            {
                ApplicationUserId = userId,
                CommentId = commentId
            });
            votes.SaveChanges();
            return factor;
        }

        private T GetVote<T>(int commentId, string userId, IRepository<T> votes) where T : Vote
        {
            return votes.All()
                .Where(x => x.CommentId == commentId && x.ApplicationUserId == userId)
                .FirstOrDefault();
        }
    }
}