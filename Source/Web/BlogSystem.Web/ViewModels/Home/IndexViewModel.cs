namespace BlogSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using BlogSystem.Web.ViewModels.BlogPost;

    public class IndexViewModel : PageableViewModel
    {
        public IEnumerable<BlogPostHomeViewModel> BlogPosts { get; set; }
    }
}