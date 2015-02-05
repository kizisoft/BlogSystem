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
                name: "Blog post", url: "Blog/{year}/{month}/{day}/{title}/{id}",
                defaults: new { controller = "Blog", action = "Index" },
                namespaces: new[] { "BlogSystem.Web.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "BlogSystem.Web.Controllers" });
        }
    }
}