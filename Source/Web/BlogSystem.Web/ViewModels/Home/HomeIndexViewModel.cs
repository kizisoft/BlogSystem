namespace BlogSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using Helpers.Server;

    public class HomeIndexViewModel : PageableBase
    {
        public IEnumerable<HomeBlogPostViewModel> BlogPosts { get; set; }
    }
}