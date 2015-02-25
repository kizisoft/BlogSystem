namespace Helpers
{
    using System.Web.Mvc;

    using Helpers.Client;
    using Helpers.Server;

    public static class PagerHtmlHelper
    {
        public static PagerBox Pager(this HtmlHelper html, IPagerTo previous, IPagerTo next, object htmlAttributes = null)
        {
            return new PagerBox(previous, next, htmlAttributes);
        }
    }
}