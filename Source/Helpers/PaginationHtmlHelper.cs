namespace Helpers
{
    using System.Web.Mvc;

    using Helpers.Client;
    using Helpers.Server;

    public static class PaginationHtmlHelper
    {
        public static PaginationBox Pagination(this HtmlHelper html, string actionName, IPageable pageable, object htmlAttributes = null, PaginationSize size = PaginationSize.Normal)
        {
            var urlHelper = new UrlHelper(html.ViewContext.Controller.ControllerContext.HttpContext.Request.RequestContext);
            var url = urlHelper.Action(actionName);

            return new PaginationBox(url, pageable, htmlAttributes, size);
        }

        public static PaginationBox Pagination(this HtmlHelper html, string actionName, string controllerName, IPageable pageable, object htmlAttributes = null, PaginationSize size = PaginationSize.Normal)
        {
            var urlHelper = new UrlHelper(html.ViewContext.Controller.ControllerContext.HttpContext.Request.RequestContext);
            var url = urlHelper.Action(actionName, controllerName);

            return new PaginationBox(url, pageable, htmlAttributes, size);
        }
    }
}