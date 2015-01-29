namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;

    public class SystemSettingsController : AdminBaseController
    {
        private readonly IRepository<SystemSetting> systemSettings;

        public SystemSettingsController(IRepository<SystemSetting> systemSettings)
        {
            this.systemSettings = systemSettings;
        }

        // GET: Administration/Settings
        [HttpGet]
        public ActionResult Index()
        {
            var model = this.systemSettings.All().OrderBy(x => x.Name).ToArray();
            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(string name)
        {
            var systemSetting = this.systemSettings.All().Where(x => x.Name == name).FirstOrDefault();
            if (systemSetting == null)
            {
                return this.HttpNotFound("No such System Setting!");
            }

            return this.View(systemSetting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SystemSetting setting)
        {
            if (ModelState.IsValid)
            {
                this.systemSettings.Update(setting);
                this.systemSettings.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(setting);
        }
    }
}