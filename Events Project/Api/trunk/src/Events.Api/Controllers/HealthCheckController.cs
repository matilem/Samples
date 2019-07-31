using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aafp.Events.Api.Tasks.Interfaces;

namespace Aafp.Events.Api.Controllers
{
    [RoutePrefix("health-check")]
    public class HealthCheckController : ApiController
    {
        public IHealthCheckTasks HealthCheckTasks { get; set; }

        [Route("")]
        public HttpResponseMessage Get()
        {
            var results = HealthCheckTasks.CheckHealth();
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }
    }
}
