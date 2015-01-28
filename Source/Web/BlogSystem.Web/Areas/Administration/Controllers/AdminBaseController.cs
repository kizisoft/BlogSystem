namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using BlogSystem.Web.Controllers;
    using BlogSystem.Web.Infrastructure.Constants;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdminBaseController : BaseController
    {
    }
}