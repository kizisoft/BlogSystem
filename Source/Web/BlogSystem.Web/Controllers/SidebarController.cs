namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class SidebarController : Controller
    {
        // GET: Sidebar
        public ActionResult Index()
        {
            return this.PartialView("_SidebarPartial");
        }
    }
}