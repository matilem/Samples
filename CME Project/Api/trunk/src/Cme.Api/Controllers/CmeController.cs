using System.Threading.Tasks;
using System.Web.Http;
using Aafp.Cme.Api.Tasks.Interfaces;

namespace Aafp.Cme.Api.Controllers
{
    [RoutePrefix("cme")]
    public class CmeController : ApiController
    {
        public ICmeStatsTasks CmeStatsTasks { get; set; }

        [Route("stats/{webLogin}/")]
        public async Task<IHttpActionResult> GetCmeStats(string webLogin)
        {
            var html = await CmeStatsTasks.GetCmeStatsHtml(webLogin);
            return Ok(html);
        }

        [Route("stats/unauthenticated")]
        public IHttpActionResult GetCmeStatsForUnauthenticatedUser()
        {
            var html = CmeStatsTasks.GetCmeStatsHtmlForUnauthenticatedUser();
            return Ok(html);
        }
    }
}