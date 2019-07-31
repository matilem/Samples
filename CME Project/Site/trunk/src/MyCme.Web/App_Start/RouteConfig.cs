using Antlr.Runtime.Misc;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aafp.MyCme.Web
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
