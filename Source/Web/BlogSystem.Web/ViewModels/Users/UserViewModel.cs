namespace BlogSystem.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class UserViewModel : UserInputModel
    {
        [Display(Name = "Points")]
        public int Points { get; set; }
    }
}