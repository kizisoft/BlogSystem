namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using BlogSystem.Common.Constants;
    using BlogSystem.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdminBaseController : BaseController
    {
    }
}