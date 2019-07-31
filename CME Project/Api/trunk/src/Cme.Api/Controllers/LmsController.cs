using Aafp.Cme.Api.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aafp.Cme.Api.Controllers
{
    [RoutePrefix("lms")]
    public class LmsController : ApiController
    {
        public ILmsTasks LmsTasks { get; set; }
        public ICmeStatsTasks CmeStatsTasks { get; set; }

        [Route("stats/{webLogin}/")]
        public async Task<IHttpActionResult> GetCmeStats(string webLogin)
        {
            var html = await CmeStatsTasks.GetCmeStatsHtml(webLogin);
            return Ok(html);
        }
    }
}
