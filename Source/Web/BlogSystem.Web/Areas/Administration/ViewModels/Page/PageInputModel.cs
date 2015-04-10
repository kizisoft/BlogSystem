namespace BlogSystem.Web.Areas.Administration.ViewModels.Page
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PageInputModel : PageSimpleInputModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Deleted On")]
        public DateTime? DeletedOn { get; set; }
    }
}