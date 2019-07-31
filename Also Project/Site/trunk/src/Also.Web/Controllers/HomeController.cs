using Aafp.Also.Web.Filters;
using Aafp.Also.Web.Tasks.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aafp.Also.Web.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : Controller
    {
        public IHomeTasks HomeTasks { get; set; }

        [Route("")]
        public async Task<ActionResult> GetCourseList()
        {
            var webLogin = GetUserName();
            var viewModel = await HomeTasks.GetCourseListByWebLogin(webLogin);

            return View("Index", viewModel);
        }

        private string GetUserName()
        {
            return SiteMinderAuthenticationFilter.GetUserName(System.Web.HttpContext.Current.Request);
        }
    }
}