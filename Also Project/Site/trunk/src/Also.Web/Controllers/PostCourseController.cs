using Aafp.Also.Web.Filters;
using Aafp.Also.Web.Tasks.Interfaces;
using Aafp.Also.Web.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aafp.Also.Web.Controllers
{
    [RoutePrefix("postcourse")]
    public class PostCourseController : Controller
    {
        public IPostCourseTasks PostCourseTasks { get; set; }

        [Route("activity/{activityNumber}")]
        public async Task<ActionResult> GetPostCourse(int activityNumber)
        {
            var webLogin = GetUserName();
            var viewModel = await PostCourseTasks.GetActivityPostCourse(activityNumber, webLogin);

            return View("PostCourse", viewModel);
        }

        [Route("instructor")]
        public ActionResult AddInstructor(InstructorViewModel instructor)
        {
            var viewModel = instructor;

            return PartialView("_Instructor", viewModel);
        }

        [HttpPost]
        [Route("save")]
        public async Task<ActionResult> SavePostCourse(PostCourseSubmissionViewModel model)
        {
            var result = await PostCourseTasks.SavePostCourse(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private string GetUserName()
        {
            return SiteMinderAuthenticationFilter.GetUserName(System.Web.HttpContext.Current.Request);
        }
    }
}
