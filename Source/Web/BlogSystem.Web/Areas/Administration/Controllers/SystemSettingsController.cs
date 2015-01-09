namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    public class SystemSettingsController : AdminBaseController
    {
        // GET: Administration/Settings
        public ActionResult Index()
        {
            return this.View();
        }
    }
}