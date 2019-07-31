using System.Threading.Tasks;
using System.Web.Http;
using Aafp.Cme.Api.Tasks.Interfaces;

namespace Aafp.Cme.Api.Controllers
{
    [RoutePrefix("available-credits")]
    public class CreditAvailableController : ApiController
    {
        public ICreditAvailableTasks CreditAvailableTasks { get; set; }

        [Route("all/{webLogin}/")]
        public async Task<IHttpActionResult> GetAllByCustomer(string webLogin)
        {
            var dto = await CreditAvailableTasks.GetAllByCustomer(webLogin);
            return Ok(dto);
        }

        [Route("purchased/{webLogin}/")]
        public async Task<IHttpActionResult> GetPurchasedByCustomer(string webLogin)
        {
            var dto = await CreditAvailableTasks.GetPurchasedByCustomer(webLogin);
            return Ok(dto);
        }

        [Route("subscriptions/{webLogin}/")]
        public async Task<IHttpActionResult> GetSubscriptionsByCustomer(string webLogin)
        {
            var dto = await CreditAvailableTasks.GetSubscriptionsByCustomer(webLogin);
            return Ok(dto);
        }

        [Route("completed/{webLogin}/")]
        public async Task<IHttpActionResult> GetCompletedByCustomer(string webLogin)
        {
            var dto = await CreditAvailableTasks.GetCompletedByCustomer(webLogin);
            return Ok(dto);
        }

        [Route("free")]
        public async Task<IHttpActionResult> GetFreeItems()
        {
            var dto = await CreditAvailableTasks.GetFreeItems();
            return Ok(dto);
        }

        [Route("stats/{webLogin}/")]
        public async Task<IHttpActionResult> GetStatsByCustomer(string webLogin)
        {
            var dto = await CreditAvailableTasks.GetCreditTotals(webLogin);
            return Ok(dto);
        }
    }
}
