namespace BlogSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using BlogSystem.Web.ViewModels.BlogPost;

    public class HomeIndexViewModel : PageableBaseViewModel
    {
        public IEnumerable<HomeBlogPostViewModel> BlogPosts { get; set; }
    }
}