using Aafp.Also.Web.Filters;
using Aafp.Also.Web.Tasks.Interfaces;
using Aafp.Also.Web.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aafp.Also.Web.Controllers
{
    [RoutePrefix("precourse")]
    public class PreCourseController : Controller
    {
        public IPreCourseTasks PreCourseTasks { get; set; }

        [Route("activity/{activityNumber}")]
        public async Task<ActionResult> GetPreCourse(int activityNumber)
        {
            var webLogin = GetUserName();
            var viewModel = await PreCourseTasks.GetActivityPrecourse(activityNumber, webLogin);

            return View("PreCourse", viewModel);
        }

        [Route("verify/{cstid}")]
        public async Task<ActionResult> VerifyCstId(string cstId)
        {
            var viewModel = await PreCourseTasks.VerifyCstId(cstId);

            return View("PreCourse", viewModel);
        }

        [HttpPost]
        [Route("save")]
        public async Task<bool> SavePreCourse(PreCourseSubmissionViewModel model)
        {
            var result = await PreCourseTasks.SavePreCourse(model);

            return result;
        }

        private string GetUserName()
        {
            return SiteMinderAuthenticationFilter.GetUserName(System.Web.HttpContext.Current.Request);
        }
    }
}
