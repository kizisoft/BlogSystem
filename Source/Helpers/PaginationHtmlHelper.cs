namespace Helpers
{
    using System.Web.Mvc;

    using Helpers.Client;
    using Helpers.Server;

    public static class PaginationHtmlHelper
    {
        public static PaginationBox Pagination(this HtmlHelper html, string url, IPageable pageable, object htmlAttributes = null, PaginationSize size = PaginationSize.Normal)
        {
            return new PaginationBox(url, pageable, htmlAttributes, size);
        }
    }
}