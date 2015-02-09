namespace BlogSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    using BlogSystem.Data.Common.Models;

    public class ContentHolder : AuditInfo, IDeletableEntity
    {
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

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}