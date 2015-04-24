namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.Areas.Administration.ViewModels.Tag;
    using System.Collections.Generic;

    public class TagsController : AdminBaseController
    {
        private readonly IRepository<Tag> tags;

        public TagsController(IRepository<Tag> tags)
        {
            this.tags = tags;
        }

        // GET: Administration/Tags
        public ActionResult Index()
        {
            var tags = this.tags.All().Project().To<TagViewModel>().ToList();
            return this.View(tags);
        }

        public JsonResult Find(string key)
        {
            var tags = new List<TagViewModel>();
            if (!string.IsNullOrEmpty(key))
            {
                tags = this.tags.All().Where(x => x.Name.Contains(key)).Project().To<TagViewModel>().ToList();
            }

            return this.Json(tags, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            var tag = this.tags.All().Where(x => x.Id == id).Project().To<TagViewModel>().FirstOrDefault();
            if (tag == null)
            {
                return this.HttpNotFound("No such Tag");
            }

            return this.View(tag);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagViewModel tag)
        {
            if (ModelState.IsValid)
            {
                this.tags.Add(new Tag { Name = tag.Name.ToLower() });
                this.tags.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(tag);
        }

        public ActionResult Edit(int? id)
        {
            var tag = this.tags.All().Where(x => x.Id == id).Project().To<TagViewModel>().FirstOrDefault();
            if (tag == null)
            {
                return this.HttpNotFound("No such Tag");
            }

            return this.View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TagViewModel tag)
        {
            if (!ModelState.IsValid)
            {
                return this.View(tag);
            }

            var tagDb = this.tags.GetById(tag.Id);
            if (tag == null)
            {
                return this.HttpNotFound("No such Tag");
            }

            tagDb.Name = tag.Name.ToLower();
            this.tags.Update(tagDb);
            this.tags.SaveChanges();
            return this.RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            var tag = this.tags.All().Where(x => x.Id == id).Project().To<TagViewModel>().FirstOrDefault();
            if (tag == null)
            {
                return this.HttpNotFound("No such Tag");
            }

            return this.View(tag);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tagDb = this.tags.GetById(id);
            if (tagDb == null)
            {
                return this.HttpNotFound("No such Blog Post");
            }

            this.tags.Delete(tagDb);
            this.tags.SaveChanges();
            return this.RedirectToAction("Index");
        }
    }
}