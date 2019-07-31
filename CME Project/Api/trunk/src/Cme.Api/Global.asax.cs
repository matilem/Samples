using System.Web;
using System.Web.Http;
using Aafp.WebApi.Components;
using StructureMap;

namespace Aafp.Cme.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            StructureMapConfig.Initialize("Aafp.Cme.Api", ApplicationConfig.DatabaseConnectionString);
            Log4NetConfig.ConfigureWithDb(ApplicationConfig.DatabaseConnectionString, true);
            AutomapperConfig.Configure();
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
