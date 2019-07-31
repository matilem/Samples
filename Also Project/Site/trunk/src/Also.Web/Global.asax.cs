using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebSite.Components;

namespace Aafp.Also.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            StructureMapConfig.Initialize("Aafp.Also.Web");
            Log4NetConfig.ConfigureWithDb(ApplicationConfig.DatabaseConnectionString, true);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterControllerFactory();
        }

        public static void RegisterControllerFactory()
        {
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
        }
    }
}
