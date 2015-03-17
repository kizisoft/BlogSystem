namespace BlogSystem.Web.ViewModels.Comment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Identity;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class CommentViewModel : SanitizableContentBaseViewModel, IMapFrom<Comment>
    {
        private ApplicationUser user;

        public CommentViewModel()
        {
            this.user = ((ICurrentUser)DependencyResolver.Current.GetService(typeof(ICurrentUser))).Get();
        }

        public CommentViewModel(ICurrentUser user)
        {
            this.user = user.Get();
        }

        public int Id { get; set; }

        public string Content
        {
            get { return this.SanitizedContent; }
            set { this.SanitizedContent = value; }
        }

        public virtual ApplicationUser Autor { get; set; }

        public bool IsVisible { get; set; }

        public string ReasonToHide { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<VoteUp> VotesUp { get; set; }

        public ICollection<VoteDown> VotesDown { get; set; }

        public string ActiveUp
        {
            get
            {
                return this.VotesUp.Any(x => x.CommentId == this.Id && x.ApplicationUserId == this.user.Id) ? " active" : string.Empty;
            }
        }

        public string ActiveDown
        {
            get
            {
                return this.VotesDown.Any(x => x.CommentId == this.Id && x.ApplicationUserId == this.user.Id) ? " active" : string.Empty;
            }
        }

        public ApplicationUser User
        {
            get { return this.user; }
        }
    }
}