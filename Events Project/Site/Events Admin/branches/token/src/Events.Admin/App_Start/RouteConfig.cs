using System.Web.Mvc;
using System.Web.Routing;

namespace Aafp.Events.Admin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;
            routes.MapMvcAttributeRoutes();
        }
    }
}
