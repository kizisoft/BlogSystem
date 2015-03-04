namespace BlogSystem.Web.ViewModels.BlogPost
{
    using Helpers.Server;

    public class BlogPagerViewModel
    {
        private readonly IPagerTo previousPage;
        private readonly IPagerTo nextPage;

        public BlogPagerViewModel(BlogPostSimpleViewModel previousPage, BlogPostSimpleViewModel nextPage)
        {
            this.previousPage = this.GetPageTo(previousPage);
            this.nextPage = this.GetPageTo(nextPage);
        }

        public IPagerTo PreviousPage
        {
            get { return this.previousPage; }
        }

        public IPagerTo NextPage
        {
            get { return this.nextPage; }
        }

        private IPagerTo GetPageTo(BlogPostSimpleViewModel page)
        {
            var pagerTo = new PagerToBase();

            if (page == null)
            {
                pagerTo.Title = "Sorry, nothing here.";
                pagerTo.Url = string.Empty;
            }
            else
            {
                pagerTo.Title = page.Title;
                pagerTo.Url = page.Url;
            }

            return pagerTo;
        }
    }
}