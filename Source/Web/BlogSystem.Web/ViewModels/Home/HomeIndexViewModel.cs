namespace BlogSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using Helpers.Server;

    public class HomeIndexViewModel : IPageable
    {
        public IEnumerable<HomeBlogPostViewModel> BlogPosts { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }
    }
}