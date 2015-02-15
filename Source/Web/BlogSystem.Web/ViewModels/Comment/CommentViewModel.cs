namespace BlogSystem.Web.ViewModels.Comment
{
    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class CommentViewModel : SanitizableContentBaseViewModel, IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content
        {
            get { return this.SanitizedContent; }
            set { this.SanitizedContent = value; }
        }

        public virtual ApplicationUser Autor { get; set; }

        public bool IsVisible { get; set; }

        public string ReasonToHide { get; set; }
    }
}