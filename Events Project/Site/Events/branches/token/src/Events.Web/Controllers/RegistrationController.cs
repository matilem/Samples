using Aafp.Events.Web.Filters;
using Aafp.Events.Web.Tasks.Interfaces;
using Aafp.Events.Web.ViewModels;
using ApiClientHelper.Components;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Aafp.Events.Web.Controllers
{
    [RoutePrefix("registration")]
    [SiteMinderAuthentication(Roles = new[] { "customer" })]
    public class RegistrationController : ControllerBase
    {
        public IRegistrationTasks RegistrationTasks { get; set; }

        [Route("")]
        public ActionResult Index()
        {
            var viewModel = RegistrationTasks.GetRegistrationHomeViewModel(User.Identity.Name);

            return View(viewModel);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [Route("intro/new/{eventCode}")]
        public ActionResult Intro(string eventCode)
        {
            var viewModel = RegistrationTasks.GetNewRegistrationIntroViewModel(eventCode, User.Identity.Name);

            if (viewModel.HasError)
                return View(viewModel);
            else if (viewModel.IsRegistered)
                return RedirectToAction("Confirmation", new { viewModel.RegistrationKey });
            else if (viewModel.IsPending)
                return View("Intro", viewModel);
            else if (viewModel.Event.IsSoldOut || !viewModel.Event.IsOpenForRegistration || !viewModel.IsEligible)
                return View("Warning", viewModel);
            else
                return View(viewModel);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [Route("intro/{registrationKey}")]
        public ActionResult Intro(Guid registrationKey)
        {
            var viewModel = RegistrationTasks.GetRegistrationIntroViewModel(registrationKey);

            return View(viewModel);
        }

        [HttpPost]
        [Route("intro")]
        public ActionResult SaveIntro(RegistrationIntroViewModel model)
        {
            var registrationKey = RegistrationTasks.SaveRegistrationIntro(model);

            return RedirectToAction("ContactInfo", new { registrationKey });
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [Route("contact-info/{registrationKey}")]
        public ActionResult ContactInfo(Guid registrationKey)
        {
            var viewModel = RegistrationTasks.GetRegistrationContactInfoViewModel(registrationKey);

            return View("ContactInfo", viewModel);
        }

        [HttpPost]
        [Route("contact-info")]
        public ActionResult SaveContactInfo(RegistrationContactInfoViewModel model)
        {
            var registrationKey = RegistrationTasks.SaveRegistrationContactInfo(model);
            var stepKey = model.Navigation.NavigationSteps[0].StepKey;

            return RedirectToAction("Sessions", new { registrationKey, stepKey });
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [Route("sessions/{registrationKey}")]
        public ActionResult Sessions(Guid registrationKey, Guid stepKey)
        {
            var viewModel = RegistrationTasks.GetRegistrationStepViewModel(registrationKey, stepKey);

            return View("Sessions", viewModel);
        }

        [HttpPost]
        [Route("sessions")]
        public ActionResult SaveSessions(RegistrationStepViewModel model)
        {
            model.StepKey = model.Navigation.NavigationSteps[model.Navigation.CurrentProgress - 3].StepKey;
            var registrationKey = RegistrationTasks.SaveRegistrationSessions(model);
            var nextStepIndex = model.Navigation.CurrentProgress - 2;
            var stepKey = Guid.Empty;            

            if (nextStepIndex >= model.Navigation.NavigationSteps.Count)
            {
                return RedirectToAction("CheckConflicts", new { registrationKey });
            }

            stepKey = model.Navigation.NavigationSteps[nextStepIndex].StepKey;

            return RedirectToAction("Sessions", null, new { registrationKey, stepKey });
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [Route("conflicts/{registrationKey}")]
        public ActionResult CheckConflicts(Guid registrationKey)
        {
            var viewModel = RegistrationTasks.GetRegistrationNotAllowedConflictViewModel(registrationKey);

            if (viewModel.ConflictGroups.Any())
            {
                return View("Conflicts", viewModel);
            }

            viewModel = RegistrationTasks.GetRegistrationAllowedConflictViewModel(registrationKey);

            if (viewModel.ConflictGroups.Any())
            {
                return View("Conflicts", viewModel);
            }

            return Redirect($"{ApplicationConfig.ApplicationConfigManager.Settings.PaymentsUrl}registration/{registrationKey}");

        }

        [HttpPost]
        [Route("resolve-conflicts")]
        public ActionResult ResolveConflicts(RegistrationConflictViewModel model)
        {
            var registrationKey = RegistrationTasks.SaveRegistrationSessionConflicts(model);

            if (model.AllowedConflicts)
            {
                return Redirect($"{ApplicationConfig.ApplicationConfigManager.Settings.PaymentsUrl}registration/{registrationKey}");
            }

            return RedirectToAction("CheckConflicts", new { registrationKey });
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [Route("confirmation/{registrationKey}")]
        public ActionResult Confirmation(Guid registrationKey)
        {
            var viewModel = RegistrationTasks.GetRegistrationConfirmationViewModel(registrationKey);

            return View("Confirmation", viewModel);
        }

        [Route("invoice/{invoiceKey}/pdf")]
        public ActionResult GetInvoicePdf(Guid invoiceKey)
        {
            var pdf = RegistrationTasks.GetInvoice(invoiceKey);

            if (pdf.Data != null)
                return File(pdf.Data, "application/pdf");
            else
                return RedirectToAction("InvoiceError", pdf);
        }

        [Route("warning")]
        public ActionResult Warning()
        {
            return View();
        }

        [Route("invoice-error")]
        public ActionResult InvoiceError(AafpServiceFileResult model)
        {
            return View("InvoiceError", model);
        }

        [Route("send-confirmation-email/{registrationKey}")]
        public JsonResult SendConfirmationEmail(Guid registrationKey)
        {
            var result = RegistrationTasks.SendConfirmationEmail(registrationKey);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("add-to-wait-list")]
        public JsonResult AddToWaitList(Guid eventKey, Guid customerKey)
        {
            var result = RegistrationTasks.AddToWaitList(eventKey, customerKey);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("save-comments")]
        public JsonResult SaveComments(Guid registrationKey, string comments)
        {
            var result = RegistrationTasks.SaveComments(registrationKey, comments);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}