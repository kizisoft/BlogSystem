namespace Helpers
{
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;

    using Helpers.Client;
    using Helpers.Server;

    public static class PaginationAjaxHelper
    {
        public static PaginationBox Pagination(this AjaxHelper ajax, string url, IPageable pageable, AjaxOptions ajaxOptions, object htmlAttributes = null, PaginationSize size = PaginationSize.Normal)
        {
            return new PaginationBox(url, pageable, ajaxOptions, htmlAttributes, size);
        }
    }
}