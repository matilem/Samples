using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aafp.Cme.Api.Controllers
{
    [RoutePrefix("credit")]
    public class CreditController : ApiController
    {
        public ICreditTasks CreditTasks { get; set; }

        [Route("reportcredits/{webLogin}/")]
        [HttpPost]
        public async Task<IHttpActionResult> ReportCredit(string webLogin, [FromBody] List<Guid> credits)
        {
            var dto = await CreditTasks.ReportCmeCredit(webLogin, credits);
            return Ok(dto);
        }

        [Route("live/{webLogin}/")]
        public async Task<IHttpActionResult> GetByCustomerForTranscript(string webLogin)
        {
            var dto = await CreditTasks.GetLiveCreditsForTranscript(webLogin);
            return Ok(dto);
        }

        [Route("reportcredits/by-session")]
        [HttpPost]
        public IHttpActionResult ReportSessionCredit(AlsoCreditDto dto)
        {
            var result = CreditTasks.ReportCmeCreditBySession(dto);
            return Ok(result);
        }

        [Route("reportcredits/teaching")]
        [HttpPost]
        public IHttpActionResult ReportTeachingCredit(TeachingCreditDto dto)
        {
            var result = CreditTasks.ReportTeachingCreditBySession(dto);
            return Ok(result);
        }
    }
}
