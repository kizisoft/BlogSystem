namespace BlogSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.ViewModels.Tag;

    public class TagsController : BaseController
    {
        private readonly IRepository<Tag> tags;

        public TagsController(IRepository<Tag> tags)
        {
            this.tags = tags;
        }

        public JsonResult Find(string key)
        {
            var tags = new List<TagViewModel>();
            if (string.IsNullOrEmpty(key))
            {
                tags = this.tags.All().Project().To<TagViewModel>().ToList();
            }
            else
            {
                tags = this.tags.All().Where(x => x.Name.Contains(key)).Project().To<TagViewModel>().ToList();
            }

            return this.Json(tags, JsonRequestBehavior.AllowGet);
        }
    }
}