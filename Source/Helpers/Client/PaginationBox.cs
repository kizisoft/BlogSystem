namespace Helpers.Client
{
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    using Helpers.Server;

    public class PaginationBox : IHtmlString
    {
        private readonly string url;
        private readonly IPageable pageable;
        private readonly object htmlAttributes;
        private readonly string size;

        public PaginationBox(string url, IPageable pageable, object htmlAttributes, PaginationSize size)
        {
            this.url = url + "?page=";
            this.pageable = pageable;
            this.htmlAttributes = htmlAttributes;
            this.size = this.SizeToBootstrapClass(size);
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
            if (this.pageable.PagesCount < 1 || this.pageable.MaxVisiblePages < 1 || this.pageable.PagesCount < 1 || this.pageable.PreviousPage < 1 || this.pageable.NextPage < 1)
            {
                return string.Empty;
            }

            var wrapper = new TagBuilder("nav");
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(this.htmlAttributes) as IDictionary<string, object>;
            wrapper.MergeAttributes(attributes);

            var pagination = new TagBuilder("ul");
            pagination.AddCssClass(this.size);

            var liPrevious = new TagBuilder("li");

            var aPrevious = new TagBuilder("a");
            var urlPrevious = this.url + this.pageable.PreviousPage;
            aPrevious.Attributes["href"] = urlPrevious;
            aPrevious.Attributes["aria-label"] = "Previous";

            var spanPrevious = new TagBuilder("span");
            spanPrevious.Attributes.Add("aria-hidden", "true");
            spanPrevious.SetInnerText("\u00AB");

            aPrevious.InnerHtml += spanPrevious;
            liPrevious.InnerHtml += aPrevious;
            pagination.InnerHtml += liPrevious;

            int count = this.pageable.PagesCount > this.pageable.MaxVisiblePages ? this.pageable.MaxVisiblePages : this.pageable.PagesCount;
            for (int i = 1; i <= count; i++)
            {
                var li = new TagBuilder("li");
                if (i == this.pageable.CurrentPage)
                {
                    var span = new TagBuilder("span");
                    span.SetInnerText(i.ToString());
                    li.InnerHtml += span;
                    li.AddCssClass("active");
                }
                else if (count < this.pageable.PagesCount && i == count - 1)
                {
                    var span = new TagBuilder("span");
                    span.SetInnerText("...");
                    li.InnerHtml += span;
                    li.AddCssClass("disabled");
                }
                else
                {
                    var a = new TagBuilder("a");
                    var url = this.url + i;
                    a.Attributes["href"] = url;
                    a.SetInnerText(i.ToString());
                    li.InnerHtml += a;
                }

                pagination.InnerHtml += li;
            }

            var liNext = new TagBuilder("li");

            var aNext = new TagBuilder("a");
            var urlNext = this.url + this.pageable.NextPage;
            aNext.Attributes["href"] = urlNext;
            aNext.Attributes["aria-label"] = "Next";

            var spanNext = new TagBuilder("span");
            spanNext.Attributes.Add("aria-hidden", "true");
            spanNext.SetInnerText("\u00BB");

            aNext.InnerHtml += spanNext;
            liNext.InnerHtml += aNext;
            pagination.InnerHtml += liNext;

            wrapper.InnerHtml += pagination;

            return wrapper.ToString();
        }
    }
}