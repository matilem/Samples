using Aafp.Also.Api.Tasks.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aafp.Also.Api.Controllers
{
    [RoutePrefix("individual")]

    public class IndividualController : ApiController
    {
        public IIndividualTasks IndividualTasks { get; set; }

        [Route("verify/{cstId}")]
        public async Task<IHttpActionResult> GetCstId(string cstId)
        {
            var dto = await IndividualTasks.GetIndividualByCustomerId(cstId);

            return Ok(dto);
        }
    }
}
