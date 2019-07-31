using System.Web.Mvc;

namespace Aafp.Events.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Name = !string.IsNullOrWhiteSpace(Request.Headers.Get("FIRSTNAME")) ? Request.Headers.Get("FIRSTNAME") : string.Empty;
        }
    }
}