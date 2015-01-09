namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    public class HomeController : AdminBaseController
    {
        // GET: Administration/Home
        public ActionResult Index()
        {
            return this.View();
        }
    }
}