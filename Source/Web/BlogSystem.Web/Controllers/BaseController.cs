namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Routing;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure;

    [RequireHttps]
    public abstract class BaseController : Controller
    {
        private readonly IRepository<SystemSetting> settings;

        public BaseController()
        {
            this.settings = (IRepository<SystemSetting>)DependencyResolver.Current.GetService(typeof(IRepository<SystemSetting>));
        }

        public BaseController(IRepository<SystemSetting> settings)
        {
            this.settings = settings;
        }

        protected JsonResult JsonError(HttpStatusCode statusCode, string errorMessage)
        {
            this.Response.StatusCode = (int)statusCode;
            return new JsonResult
            {
                Data = new { errorMessage = errorMessage }
            };
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.ViewBag.Settings = new SystemSettingsManager(this.GetSettings);
            return base.BeginExecute(requestContext, callback, state);
        }

        protected IDictionary<string, string> GetSettings()
        {
            return this.settings.All().ToDictionary(x => x.Name, x => x.Value);
        }
    }
}