namespace BlogSystem.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Blog post",
                url: "Blog/{year}/{month}/{day}/{title}/{id}",
                defaults: new { controller = "Blog", action = "Index" },
                namespaces: new[] { "BlogSystem.Web.Controllers" });

            routes.MapRoute(
                name: "Tags Find",
                url: "Tags/Find/{key}",
                defaults: new { controller = "Tags", action = "Find", key = UrlParameter.Optional },
                namespaces: new[] { "BlogSystem.Web.Controllers" });

            routes.MapRoute(
                name: "Tags",
                url: "Tags/{tagName}",
                defaults: new { controller = "Home", action = "Tags", tagName = UrlParameter.Optional },
                namespaces: new[] { "BlogSystem.Web.Controllers" });

            routes.MapRoute(
                name: "Users info",
                url: "Users/{userName}",
                defaults: new { controller = "Users", action = "Info" },
                namespaces: new[] { "BlogSystem.Web.Controllers" });

            routes.MapRoute(
                name: "Users upload",
                url: "Users/Upload/{userName}",

                defaults: new { controller = "Users", action = "Upload" },
                namespaces: new[] { "BlogSystem.Web.Controllers" });

            routes.MapRoute(
                name: "Users edit",
                url: "Users/{action}/{userName}",
                defaults: new { controller = "Users", action = "Edit" },
                namespaces: new[] { "BlogSystem.Web.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "BlogSystem.Web.Controllers" });
        }
    }
}