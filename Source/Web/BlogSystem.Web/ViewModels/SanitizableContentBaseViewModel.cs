namespace BlogSystem.Web.ViewModels
{
    using System.Web.Mvc;

    using BlogSystem.Web.Infrastructure.Sanitizer;

    public abstract class SanitizableContentBaseViewModel
    {
        private readonly ISanitizer sanitizer;

        private string sanitizedContent;

        public SanitizableContentBaseViewModel()
        {
            this.sanitizer = (ISanitizer)DependencyResolver.Current.GetService(typeof(ISanitizer));
        }

        public SanitizableContentBaseViewModel(ISanitizer sanitizer)
        {
            this.sanitizer = sanitizer;
        }

        protected string SanitizedContent
        {
            get { return this.sanitizer.Sanitize(this.sanitizedContent); }
            set { this.sanitizedContent = value; }
        }
    }
}