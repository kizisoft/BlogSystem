﻿namespace BlogSystem.Web.Areas.Administration.ViewModels.BlogPost
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BlogPostInputModel : BlogPostSimpleInputModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Deleted On")]
        public DateTime? DeletedOn { get; set; }
    }
}