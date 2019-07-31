using AdminTool.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AdminTool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HydraUser()
        {
            var viewModel = new HydraUserViewModel();
            viewModel.Permissions = new List<HydraUserPermissionViewModel>();

            return View("HydraUser", viewModel);
        }

        public ActionResult ActiveDirectoryUser()
        {
            var viewModel = new ActiveDirectoryViewModel();

            return View("ActiveDirectoryUser");
        }
    }
}