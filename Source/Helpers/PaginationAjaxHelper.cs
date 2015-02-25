namespace Helpers
{
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;

    using Helpers.Client;
    using Helpers.Server;

    public static class PaginationAjaxHelper
    {
        public static AjaxPaginationBox Pagination(this AjaxHelper ajax, string actionName, IPageable pageable, AjaxOptions ajaxOptions, object htmlAttributes = null, PaginationSize size = PaginationSize.Normal)
        {
            var urlHelper = new UrlHelper(ajax.ViewContext.Controller.ControllerContext.HttpContext.Request.RequestContext);
            var url = urlHelper.Action(actionName);

            return new AjaxPaginationBox(url, pageable, ajaxOptions, htmlAttributes, size);
        }

        public static AjaxPaginationBox Pagination(this AjaxHelper ajax, string actionName, string controllerName, IPageable pageable, AjaxOptions ajaxOptions, object htmlAttributes = null, PaginationSize size = PaginationSize.Normal)
        {
            var urlHelper = new UrlHelper(ajax.ViewContext.Controller.ControllerContext.HttpContext.Request.RequestContext);
            var url = urlHelper.Action(actionName, controllerName);

            return new AjaxPaginationBox(url, pageable, ajaxOptions, htmlAttributes, size);
        }
    }
}