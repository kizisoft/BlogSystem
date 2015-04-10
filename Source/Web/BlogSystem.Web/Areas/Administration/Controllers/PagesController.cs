namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.Areas.Administration.ViewModels.Page;

    public class PagesController : AdminBaseController
    {
        private readonly IRepository<Page> pages;

        public PagesController(IRepository<Page> pages)
        {
            this.pages = pages;
        }

        // GET: Administration/Pages
        public ActionResult Index()
        {
            var pages = this.pages.All().OrderByDescending(x => x.CreatedOn).Project().To<PageSimpleViewModel>().ToList();
            return this.View(pages);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var page = this.pages.All().Where(x => x.Id == id).Project().To<PageViewModel>().FirstOrDefault();
            if (page == null)
            {
                return this.HttpNotFound("No such Page!");
            }

            return this.View(page);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View(new PageSimpleInputModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PageSimpleInputModel page)
        {
            if (!ModelState.IsValid)
            {
                return this.View(page);
            }

            this.pages.Add(new Page
            {
                Title = page.Title,
                Content = page.Content,
                MetaDescription = page.MetaDescription,
                MetaKeywords = page.MetaKeywords,
                Permalink = page.Permalink
            });
            this.pages.SaveChanges();

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var page = this.pages.All().Where(x => x.Id == id).Project().To<PageInputModel>().FirstOrDefault();
            if (page == null)
            {
                return this.HttpNotFound("No such Page");
            }

            return this.View(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PageInputModel page)
        {
            if (!ModelState.IsValid)
            {
                return this.View(page);
            }

            var pageDb = this.pages.GetById(page.Id);
            if (pageDb == null)
            {
                return this.HttpNotFound("No such Blog Post");
            }

            pageDb.Title = page.Title;
            pageDb.Content = page.Content;
            pageDb.Permalink = page.Permalink;
            pageDb.MetaDescription = page.MetaDescription;
            pageDb.MetaKeywords = page.MetaKeywords;
            pageDb.IsDeleted = page.IsDeleted;
            pageDb.DeletedOn = page.DeletedOn;
            pageDb.ModifiedOn = DateTime.Now;

            this.pages.Update(pageDb);
            this.pages.SaveChanges();

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var page = this.pages.All().Where(x => x.Id == id).Project().To<PageViewModel>().FirstOrDefault();
            if (page == null)
            {
                return this.HttpNotFound("No such Page");
            }

            return this.View(page);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pageDb = this.pages.GetById(id);
            if (pageDb == null)
            {
                return this.HttpNotFound("No such Page");
            }

            this.pages.Delete(pageDb);
            this.pages.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}