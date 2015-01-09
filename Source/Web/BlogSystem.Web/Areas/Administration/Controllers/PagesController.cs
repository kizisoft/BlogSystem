namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    public class PagesController : AdminBaseController
    {
        // GET: Administration/Pages
        public ActionResult Index()
        {
            return this.View();
        }
    }
}