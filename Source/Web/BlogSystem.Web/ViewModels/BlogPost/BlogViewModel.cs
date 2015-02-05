namespace BlogSystem.Web.ViewModels.BlogPost
{
    public class BlogViewModel
    {
        public BlogPostIndexViewModel BlogPost { get; set; }

        public BlogPostSimpleViewModel Previous { get; set; }

        public BlogPostSimpleViewModel Next { get; set; }
    }
}