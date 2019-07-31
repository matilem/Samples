using System.Web;
using System.Web.Http;
using Aafp.Events.Api.ApplicationConfig;
using Aafp.WebApi.Components;
using StructureMap;

namespace Aafp.Events.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            StructureMapConfig.Initialize("Aafp.Events.Api", ApplicationConfigManager.Settings.ConnectionString);
            AutomapperConfig.Configure();
            Log4NetConfig.ConfigureWithDb(ApplicationConfigManager.Settings.ConnectionString, true);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterControllerFactory();
        }

        public static void RegisterControllerFactory()
        {
            var container = ObjectFactory.Container;
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
        }
    }
}
