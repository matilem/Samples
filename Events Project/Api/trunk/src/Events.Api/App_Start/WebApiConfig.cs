using System.Web.Http;
using Aafp.Events.Api.Filters;
using Aafp.WebApi.Components;

namespace Aafp.Events.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //Remove the XM Formatter from the web api
            config.Filters.Add(new ServiceExceptionFilter());
            config.Filters.Add(new NHibernateActionFilter());

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
