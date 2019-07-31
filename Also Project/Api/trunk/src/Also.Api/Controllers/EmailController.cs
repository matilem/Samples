using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using ApiClientHelper.Components;
using System.Web.Http;

namespace Aafp.Also.Api.Controllers
{
    [RoutePrefix("email")]
    public class EmailController : ApiController
    {
        public IEmailTasks EmailTasks { get; set; }

        [Route("welcome")]
        [HttpPost]
        public IHttpActionResult SendWelcomeEmail(AlsoMessageDto alsoMessage)
        {
            try
            {
                var result = EmailTasks.SendWelcomeEmail(alsoMessage);

                return Ok(result);
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
