namespace BlogSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class BlogPost : ContentHolder
    {
        private ICollection<Tag> tags;
        private ICollection<Comment> comments;

        public BlogPost()
        {
            this.tags = new HashSet<Tag>();
            this.comments = new HashSet<Comment>();
            this.IsCommentsDisabled = true;
        }

        [StringLength(300, MinimumLength = 5)]
        public string SubTitle { get; set; }

        [Required]
        [AllowHtml]
        public string ShortContent { get; set; }

        public string AutorId { get; set; }

        public virtual ApplicationUser Autor { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public bool IsCommentsDisabled { get; set; }
    }
}