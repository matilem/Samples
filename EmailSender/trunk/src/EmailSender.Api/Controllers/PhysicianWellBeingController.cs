using System.Web;
using System.Web.Http;
using Aafp.EmailSender.Api.Dtos;
using Aafp.EmailSender.Api.Tasks.Interfaces;

namespace Aafp.EmailSender.Api.Controllers
{
    [RoutePrefix("pwb")]
    public class PhysicianWellBeingController : ApiController
    {
        public IPhysicianWellBeingTasks PhysicianWellBeingTasks { get; set; }

        [Route("testing")]
        [HttpPost]
        public IHttpActionResult SendTestEmail(PhysicianWellBeingMessageDto dto)
        {
            var result = PhysicianWellBeingTasks.SendTestEmail(dto, HttpContext.Current.Request.RequestContext);

            return Ok(result);
        }

        [Route("feedback")]
        [HttpPost]
        public IHttpActionResult SendFeedbackEmail(PhysicianWellBeingMessageDto dto)
        {
            var result = PhysicianWellBeingTasks.SendFeedbackEmail(dto, HttpContext.Current.Request.RequestContext);

            return Ok(result);
        }
    }
}
