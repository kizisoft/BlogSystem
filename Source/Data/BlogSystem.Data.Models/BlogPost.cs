namespace BlogSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class BlogPost : ContentHolder
    {
        [Required]
        [AllowHtml]
        public string ShortContent { get; set; }
    }
}