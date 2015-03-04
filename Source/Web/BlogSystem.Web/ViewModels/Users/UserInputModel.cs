namespace BlogSystem.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class UserInputModel : IMapFrom<ApplicationUser>
    {
        [Required]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Real Name")]
        public string RealName { get; set; }

        [Display(Name = "Avatar")]
        public string AvatarUrl { get; set; }
    }
}