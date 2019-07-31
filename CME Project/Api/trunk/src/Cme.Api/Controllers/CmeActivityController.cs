using System.Web.Http;
using Aafp.Cme.Api.Tasks.Interfaces;
using System.Threading.Tasks;

namespace Aafp.Cme.Api.Controllers
{
    [RoutePrefix("activity")]
    public class CmeActivityController : ApiController
    {
        public ICmeActivityTasks CmeActivityTasks { get; set; }

        [Route("{activityNumber}/{webLogin}/")]
        public async Task<IHttpActionResult> GetSessionsByCustomerAndActivity(string activityNumber, string webLogin)
        {
            //No CME Validation required - Checking if Optional CME has been purchase
            var parsedActivityNumber = 0;
            var parseResult = int.TryParse(activityNumber, out parsedActivityNumber);

            if (!parseResult)
                return BadRequest("Activity number must be an integer.");

            var dto = await CmeActivityTasks.GetCmeSessionsByActivity(parsedActivityNumber, webLogin);

            if (dto == null)
                return BadRequest("The provided activity number is not valid.");

            return Ok(dto);
        }
    }
}
