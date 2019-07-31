using System.Threading.Tasks;
using System.Web.Mvc;
using Aafp.MyCme.Web.Filters;
using Aafp.MyCme.Web.Tasks.Interfaces;

namespace Aafp.MyCme.Web.Controllers
{
    [RoutePrefix("")]
    [SiteMinderAuthenticationFilter(Roles = new[] { "customer" })]
    public class HomeController : Controller
    {
        public IHomeTasks HomeTasks { get; set; }

        [Route("")]
        public async Task<ActionResult> Index()
        {
            var viewModel = await HomeTasks.GetHomeViewModel(User.Identity.Name);

            return View(viewModel);
        }
    }
}