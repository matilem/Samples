using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aafp.Events.Api.Tasks.Admin.Interfaces;

namespace Aafp.Events.Api.Controllers
{
    [RoutePrefix("admin/session")]
    public class AdminSessionController : ApiController
    {
        public IAdminSessionTasks AdminSessionTasks { get; set; }

        [HttpPost]
        [Route("{sessionKey}/increase-capacity")]
        public HttpResponseMessage IncreaseSessionCapacity(Guid sessionKey)
        {
            var updated = AdminSessionTasks.IncreaseSessionCapacity(sessionKey);

            if (updated)
            {
                var dto = AdminSessionTasks.GetSessionByKey(sessionKey);

                return Request.CreateResponse(HttpStatusCode.OK, dto);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unable to oversell session.");
            }
            
        }
    }
}
