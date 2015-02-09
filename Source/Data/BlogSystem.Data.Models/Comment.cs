namespace BlogSystem.Data.Models
{
    using System;
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
        public virtual ApplicationUser Autor { get; set; }

        public int BlogPostId { get; set; }

        [Required]
        public virtual BlogPost BlogPost { get; set; }

        public bool IsVisible { get; set; }

        [StringLength(100)]
        public string ReasonToHide { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}