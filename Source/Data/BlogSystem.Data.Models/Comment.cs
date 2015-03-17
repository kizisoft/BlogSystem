namespace BlogSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using BlogSystem.Data.Common.Models;

    public class Comment : AuditInfo, IDeletableEntity
    {
        public Comment()
        {
            this.IsVisible = true;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string AutorId { get; set; }

        public virtual ApplicationUser Autor { get; set; }

        [Required]
        public int BlogPostId { get; set; }

        public virtual BlogPost BlogPost { get; set; }

        public bool IsVisible { get; set; }

        [StringLength(100)]
        public string ReasonToHide { get; set; }

        public virtual ICollection<VoteUp> VotesUp { get; set; }

        public virtual ICollection<VoteDown> VotesDown { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}