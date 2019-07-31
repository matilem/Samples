using System.Threading.Tasks;
using System.Web.Mvc;
using Aafp.MyCme.Web.Filters;
using Aafp.MyCme.Web.Tasks.Interfaces;

namespace Aafp.MyCme.Web.Controllers
{
    [RoutePrefix("cards")]
    [SiteMinderAuthenticationFilter(Roles = new[] { "customer" })]
    public class CmeCardController : Controller
    {
        public ICmeCardTasks CmeCardTasks { get; set; }

        [Route("totals")]
        public async Task<ActionResult> GetCmeCardTotals()
        {
            var viewModel = await CmeCardTasks.GetCardTotals(User.Identity.Name);
            
            return PartialView("_CmeStats", viewModel);
        }

        [Route("all")]
        public async Task<ActionResult> GetAllCmeCards()
        {
            var viewModel = await CmeCardTasks.GetAllItems(User.Identity.Name);

            return PartialView("_CmeCard", viewModel);
        }

        [Route("purchased")]
        public async Task<ActionResult> GetPurchasedCmeCards()
        {
            var viewModel = await CmeCardTasks.GetPurchasedItems(User.Identity.Name);

            return PartialView("_CmeCard", viewModel);
        }

        [Route("subscription")]
        public async Task<ActionResult> GetSubscriptionCmeCards()
        {
            var viewModel = await CmeCardTasks.GetSubscriptionItems(User.Identity.Name);

            return PartialView("_CmeCard", viewModel);
        }

        [Route("free")]
        public async Task<ActionResult> GetFreeCmeCards()
        {
            var viewModel = await CmeCardTasks.GetFreeItems();

            return PartialView("_CmeCard", viewModel);
        }

        [Route("completed")]
        public async Task<ActionResult> GetCompletedCmeCards()
        {
            var viewModel = await CmeCardTasks.GetCompletedItems(User.Identity.Name);

            return PartialView("_CmeCard", viewModel);
        }
    }
}