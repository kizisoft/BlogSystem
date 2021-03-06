namespace BlogSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using BlogSystem.Common.Constants;
    using BlogSystem.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            // TODO: Remove it in production
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.SeedRoles(context);
            this.SeedSystemSettings(context);
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.AdministratorRoleName));
        }

        private void SeedSystemSettings(ApplicationDbContext context)
        {
            if (context.SystemSettings.Any())
            {
                return;
            }

            context.SystemSettings.Add(new SystemSetting { Name = "Logo URL", Value = "/img/header-logo.png" });
            context.SystemSettings.Add(new SystemSetting { Name = "Home Title", Value = "Home Title" });
            context.SystemSettings.Add(new SystemSetting { Name = "Blog Name", Value = "Blog Name" });
            context.SystemSettings.Add(new SystemSetting { Name = "Blog Url", Value = "Blog Url" });
            context.SystemSettings.Add(new SystemSetting { Name = "Author", Value = "Author" });
            context.SystemSettings.Add(new SystemSetting { Name = "GitHub Profile", Value = "GitHub Profile" });
            context.SystemSettings.Add(new SystemSetting { Name = "GitHub Badge HTML Code", Value = "GitHub Badge HTML Code" });
            context.SystemSettings.Add(new SystemSetting { Name = "Stack Overflow Profile", Value = "Stack Overflow Profile" });
            context.SystemSettings.Add(new SystemSetting { Name = "Linked In Profile", Value = "Linked In Profile" });
            context.SystemSettings.Add(new SystemSetting { Name = "Contact Email", Value = "Contact Email" });
            context.SystemSettings.Add(new SystemSetting { Name = "Facebook Profile", Value = "Facebook Profile" });
            context.SystemSettings.Add(new SystemSetting { Name = "Foursquare Profile", Value = "Foursquare Profile" });
            context.SystemSettings.Add(new SystemSetting { Name = "Google Profile", Value = "Google+ Profile" });
            context.SystemSettings.Add(new SystemSetting { Name = "RSS Url", Value = "RSS Url" });
            context.SystemSettings.Add(new SystemSetting { Name = "Comments Per Page", Value = "10" });
            context.SystemSettings.Add(new SystemSetting { Name = "ReCaptcha key", Value = "6Ld1eP4SAAAAAIz25clEdLemL8wMPa75LdiIKOPc" });
            context.SystemSettings.Add(new SystemSetting { Name = "ReCaptcha secret", Value = "6Ld1eP4SAAAAALRG5kUYhhyl2UmyWTw6l6z2r7gi" });
            context.SystemSettings.Add(new SystemSetting { Name = "Imgur Client ID", Value = "189a3981f79cc30" });
            context.SystemSettings.Add(new SystemSetting { Name = "Imgur Client Secret", Value = "b74594b806c4e8b8e8a93f4be9da84cf2f7a0923" });
        }
    }
}