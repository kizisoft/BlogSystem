namespace BlogSystem.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;

    public class SystemSettingsManager
    {
        private readonly Lazy<IDictionary<string, string>> settings;

        public SystemSettingsManager(Func<IDictionary<string, string>> initializer)
        {
            this.settings = new Lazy<IDictionary<string, string>>(initializer);
        }

        public IDictionary<string, string> Get
        {
            get
            {
                return this.settings.Value;
            }
        }
    }
}