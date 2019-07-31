using System.Threading.Tasks;
using System.Web.Http;
using Aafp.Cme.Api.Tasks.Interfaces;

namespace Aafp.Cme.Api.Controllers
{
    [RoutePrefix("re-election")]
    public class ReElectionController : ApiController
    {
        public IReElectionTasks ReElectionTasks { get; set; }

        [Route("status/{webLogin}/")]
        public async Task<IHttpActionResult> GetReElectionByWebLogin(string webLogin)
        {
            var result = await ReElectionTasks.GetReElectionByWebLogin(webLogin);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
