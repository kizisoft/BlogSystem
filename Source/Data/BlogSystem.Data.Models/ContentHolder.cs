namespace BlogSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    using BlogSystem.Data.Common.Models;

    public class ContentHolder : AuditInfo, IDeletableEntity
    {
        private ICollection<Tag> tags;
        private ICollection<Comment> comments;

        public ContentHolder()
        {
            this.tags = new HashSet<Tag>();
            this.comments = new HashSet<Comment>();
            this.IsCommentsDisabled = true;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 5)]
        public string Title { get; set; }

        [StringLength(300, MinimumLength = 5)]
        public string SubTitle { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public bool IsCommentsDisabled { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}