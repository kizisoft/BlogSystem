namespace Helpers.Client
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    using Helpers.Server;

    public class PagerBox : IHtmlString
    {
        private readonly IPagerTo previous;
        private readonly IPagerTo next;
        private readonly object htmlAttributes;

        public PagerBox(IPagerTo previous, IPagerTo next, object htmlAttributes)
        {
            if (previous == null || next == null)
            {
                throw new ArgumentNullException("The IPagerTo parameter of PagerHtmlHelper must not be null!");
            }

            this.previous = previous;
            this.next = next;
            this.htmlAttributes = htmlAttributes;
        }

        public string ToHtmlString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            return this.RenderPager();
        }

        private string RenderPager()
        {
            var wrapper = new TagBuilder("nav");
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(this.htmlAttributes) as IDictionary<string, object>;
            wrapper.MergeAttributes(attributes);
            wrapper.AddCssClass("pager-nav");

            var pager = new TagBuilder("ul");
            pager.AddCssClass("pager-custom clearfix");

            var liPrevious = new TagBuilder("li");
            liPrevious.AddCssClass("pager-previous text-left pull-left");

            TagBuilder aOrSpanPrevious;

            if (string.IsNullOrEmpty(this.previous.Url))
            {
                aOrSpanPrevious = this.GetAOrSpanHtml("span", "pull-left", "glyphicon-arrow-left", "pull-right", this.previous.Title);
            }
            else
            {
                aOrSpanPrevious = this.GetAOrSpanHtml("a", "pull-left", "glyphicon-arrow-left", "pull-right", this.previous.Title);
                aOrSpanPrevious.Attributes.Add("href", this.previous.Url);
            }

            var liNext = new TagBuilder("li");
            liNext.AddCssClass("pager-next text-right pull-right");

            TagBuilder aOrSpanNext;

            if (string.IsNullOrEmpty(this.next.Url))
            {
                aOrSpanNext = this.GetAOrSpanHtml("span", "pull-right", "glyphicon-arrow-right", "pull-left", this.next.Title);
            }
            else
            {
                aOrSpanNext = this.GetAOrSpanHtml("a", "pull-right", "glyphicon-arrow-right", "pull-left", this.next.Title);
                aOrSpanNext.Attributes.Add("href", this.next.Url);
            }

            liPrevious.InnerHtml += aOrSpanPrevious;
            liNext.InnerHtml += aOrSpanNext;
            pager.InnerHtml += liPrevious;
            pager.InnerHtml += liNext;
            wrapper.InnerHtml += pager;

            return wrapper.ToString();
        }

        private TagBuilder GetAOrSpanHtml(string aOrSpan, string spanPagerGlyphClass, string spanArrowClass, string spanPagerTextClass, string spanPagerTextTitle)
        {
            var element = new TagBuilder(aOrSpan);

            var spanPagerGlyph = new TagBuilder("span");
            spanPagerGlyph.AddCssClass("pager-glyph " + spanPagerGlyphClass);

            var spanArrow = new TagBuilder("span");
            spanArrow.AddCssClass("glyphicon " + spanArrowClass);
            spanArrow.Attributes.Add("aria-hidden=", "true");

            var spanPagerText = new TagBuilder("span");
            spanPagerText.AddCssClass("pager-text " + spanPagerTextClass);
            spanPagerText.SetInnerText(spanPagerTextTitle);

            spanPagerGlyph.InnerHtml += spanArrow;
            element.InnerHtml += spanPagerGlyph;
            element.InnerHtml += spanPagerText;
            return element;
        }
    }
}