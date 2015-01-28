namespace BlogSystem.Web.ViewModels.Sidebar
{
    using System.Collections.Generic;

    using BlogSystem.Web.ViewModels.BlogPost;

    public class SidebarViewModel
    {
        public IEnumerable<BlogPostSimpleViewModel> BlogPosts { get; set; }
    }
}