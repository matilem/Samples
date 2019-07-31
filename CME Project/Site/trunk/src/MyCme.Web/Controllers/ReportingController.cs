using System.Threading.Tasks;
using System.Web.Mvc;
using Aafp.MyCme.Web.Filters;
using Aafp.MyCme.Web.Tasks.Interfaces;

namespace Aafp.MyCme.Web.Controllers
{
    [RoutePrefix("report")]
    [SiteMinderAuthenticationFilter(Roles = new[] {"customer"})]
    public class ReportingController : Controller
    {
        public ICmeActivityTasks CmeActivityTasks { get; set; }

        public ICreditTasks CreditTasks { get; set; }

        [HttpGet]
        [Route("live/{activityNumber}")]
        public async Task<ActionResult> GetCmeActivitiesForCustomer(string activityNumber)
        {
            var viewModel = await CmeActivityTasks.GetCmeActivies(activityNumber, User.Identity.Name);

            ViewBag.Name = !viewModel.HasError && !string.IsNullOrWhiteSpace(viewModel.Customer.FirstName)
                ? viewModel.Customer.FirstName
                : string.Empty;

            return View("LiveActivity", viewModel);
        }

        [HttpPost]
        [Route("submit")]
        public async Task<JsonResult> PostSubmissionData(string activityNumber, string[] sessionKeys)
        {
            var result = await CreditTasks.SubmitCredits(sessionKeys, User.Identity.Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}