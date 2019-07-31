using System.Web;
using System.Web.Http;
using Aafp.EmailSender.Api.Dtos;
using Aafp.EmailSender.Api.Tasks.Interfaces;

namespace Aafp.EmailSender.Api.Controllers
{
    [RoutePrefix("also")]
    public class AlsoController : ApiController
    {
        public IAlsoTasks AlsoTasks { get; set; }

        [Route("testing")]
        [HttpGet]
        public IHttpActionResult SendTestEmail(AlsoMessageDto dto)
        {
            var result = AlsoTasks.SendTestEmail(dto, HttpContext.Current.Request.RequestContext);

            return Ok(result);
        }

        [Route("welcome")]
        [HttpPost]
        public IHttpActionResult SendWelcomeEmail(AlsoMessageDto dto)
        {
            var result = AlsoTasks.SendWelcomeEmail(dto, HttpContext.Current.Request.RequestContext);

            return Ok(result);
        }

        [Route("instructions/learner")]
        [HttpPost]
        public IHttpActionResult SendLearnerinstuctionsEmail(AlsoLearnerMessageDto dto)
        {
            var result = AlsoTasks.SendLearnerInstructionsEmail(dto, HttpContext.Current.Request.RequestContext);

            return Ok(result);
        }

        [Route("status-change")]
        [HttpPost]
        public IHttpActionResult SendStatusChangeEmail(AlsoStatusChangeMessageDto dto)
        {
            var result = AlsoTasks.SendStatusChangeEmail(dto, HttpContext.Current.Request.RequestContext);

            return Ok(result);
        }
    }
}
