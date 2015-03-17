namespace Helpers.Client
{
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;

    using Helpers.Server;

    public class PaginationBox : IHtmlString
    {
        private readonly string url;
        private readonly IPageable pageable;
        private readonly AjaxOptions ajaxOptions;
        private readonly object htmlAttributes;
        private readonly string size;

        private string urlAttribute;

        public PaginationBox(string url, IPageable pageable, object htmlAttributes, PaginationSize size)
        {
            this.url = url + "?page=";
            this.urlAttribute = "href";
            this.pageable = pageable;
            this.htmlAttributes = htmlAttributes;
            this.size = this.SizeToBootstrapClass(size);
        }

        public PaginationBox(string url, IPageable pageable, AjaxOptions ajaxOptions, object htmlAttributes, PaginationSize size)
            : this(url, pageable, htmlAttributes, size)
        {
            this.urlAttribute = "data-ajax-url";
            this.ajaxOptions = ajaxOptions;
        }

        public string ToHtmlString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            return this.RenderPagination();
        }

        private string SizeToBootstrapClass(PaginationSize size)
        {
            switch (size)
            {
                case PaginationSize.Small: return "pagination pagination-sm";
                case PaginationSize.Large: return "pagination pagination-lg";
                default: return "pagination";
            }
        }

        private string RenderPagination()
        {
            if (this.pageable.CurrentPage < 1 || this.pageable.TotalPages < 1)
            {
                return string.Empty;
            }

            var wrapper = new TagBuilder("nav");
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(this.htmlAttributes) as IDictionary<string, object>;
            wrapper.MergeAttributes(attributes);

            var pagination = new TagBuilder("ul");
            pagination.AddCssClass(this.size);

            if (this.pageable.TotalPages == 1)
            {
                return string.Empty;
            }

            if (this.pageable.CurrentPage > 1)
            {
                this.AddNavLi(pagination, this.pageable.CurrentPage - 1, "\u00AB");
            }

            this.CreatePages(pagination, this.pageable.CurrentPage, this.pageable.TotalPages);

            if (this.pageable.CurrentPage < this.pageable.TotalPages)
            {
                this.AddNavLi(pagination, this.pageable.CurrentPage + 1, "\u00BB");
            }

            wrapper.InnerHtml += pagination;
            return wrapper.ToString();
        }

        private void CreatePages(TagBuilder pagination, int currentPage, int pagesCount)
        {
            bool hasPoints = false;
            int start = 1;
            int end = pagesCount - 1;

            if (pagesCount > 5)
            {
                this.AddNavLi(pagination, 1, "1");

                if (currentPage < 5)
                {
                    start = 2;
                }
                else
                {
                    this.AddLi(pagination, "...", "disabled");
                    start = currentPage < pagesCount ? currentPage - 1 : pagesCount - 2;
                }

                if (currentPage < pagesCount - 2 && currentPage != pagesCount - 3)
                {
                    end = currentPage > 1 ? currentPage + 1 : 3;
                    hasPoints = true;
                }
            }

            for (int i = start; i <= end; i++)
            {
                this.AddNavLi(pagination, i, i.ToString());
            }

            if (hasPoints)
            {
                this.AddLi(pagination, "...", "disabled");
            }

            this.AddNavLi(pagination, pagesCount, pagesCount.ToString());
        }

        private void AddNavLi(TagBuilder ul, int page, string value)
        {
            if (page == this.pageable.CurrentPage)
            {
                this.AddLi(ul, value, "active");
                return;
            }

            var li = new TagBuilder("li");
            var a = new TagBuilder("a");
            var ajaxAttributes = this.ajaxOptions != null ? this.ajaxOptions.ToUnobtrusiveHtmlAttributes() : null;
            a.MergeAttributes(ajaxAttributes);
            var url = this.url + page;
            a.Attributes.Add(this.urlAttribute, url);
            a.SetInnerText(value);
            li.InnerHtml += a;
            ul.InnerHtml += li;
        }

        private void AddLi(TagBuilder ul, string value, string cssClass)
        {
            var li = new TagBuilder("li");
            var span = new TagBuilder("span");
            span.SetInnerText(value);
            li.InnerHtml += span;
            li.AddCssClass(cssClass);
            ul.InnerHtml += li;
        }
    }
}