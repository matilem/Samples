using System.Web.Http;
using Aafp.Payments.Api.Filters;
using Aafp.WebApi.Components;

namespace Aafp.Payments.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new ServiceExceptionFilter());
            config.Filters.Add(new NHibernateActionFilter());

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
