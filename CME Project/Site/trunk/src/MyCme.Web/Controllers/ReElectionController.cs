using System.Threading.Tasks;
using System.Web.Mvc;
using Aafp.MyCme.Web.Filters;
using Aafp.MyCme.Web.Tasks.Interfaces;

namespace Aafp.MyCme.Web.Controllers
{
    [RoutePrefix("re-election")]
    [SiteMinderAuthenticationFilter(Roles = new[] { "customer" })]
    public class ReElectionController : Controller
    {
        public IReElectionTasks ReElectionTasks { get; set; }

        [Route("status")]
        public async Task<ActionResult> GetReElectionStatus()
        {
            var viewModel = await ReElectionTasks.GetReElectionStatusViewModel(User.Identity.Name);

            return PartialView("_ReElectionStatus", viewModel);
        }
    }
}