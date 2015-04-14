namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.ViewModels.Pages;

    public class PagesController : BaseController
    {
        private readonly IRepository<Page> pages;

        public PagesController(IRepository<Page> pages)
        {
            this.pages = pages;
        }

        // GET: Page
        public ActionResult Index(string permalink)
        {
            var viewModel =
                this.pages.All()
                    .Where(x => x.Permalink.ToLower().Trim() == permalink.ToLower().Trim())
                    .Project()
                    .To<PageIndexViewModel>()
                    .FirstOrDefault();

            if (viewModel == null)
            {
                return this.HttpNotFound("Page not found!");
            }

            return this.View(viewModel);
        }
    }
}