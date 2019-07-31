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

            if (viewModel.Event.IsSoldOut)
            {
                return View("Warning", viewModel);
            }

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

            if (model.PayNow)
            {
                return RedirectToAction("CheckConflicts", new { registrationKey });
            }

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

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [Route("editsessions/{registrationKey}")]
        public ActionResult EditSessions(Guid registrationKey, Guid stepKey)
        {
            var viewModel = RegistrationTasks.GetEditRegistrationStepViewModel(registrationKey, stepKey);

            return View("EditedSessions", viewModel);
        }

        [HttpPost]
        [Route("sessions")]
        public ActionResult SaveSessions(RegistrationStepViewModel model)
        {
            var registrationKey = RegistrationTasks.SaveRegistrationSessions(model);

            if (model.PayNow)
            {
                return RedirectToAction("CheckConflicts", new { registrationKey });
            }

            var stepKey = Guid.Empty;
            model.StepKey = model.Navigation.NavigationSteps[model.Navigation.CurrentProgress - 3].StepKey;
            var nextStepIndex = model.Navigation.CurrentProgress - 2;

            if (nextStepIndex >= model.Navigation.NavigationSteps.Count && model.RegistrationStatus == "Edit")
            {
                return RedirectToAction("EditCheckConflicts", new { registrationKey });
            }

            if (nextStepIndex >= model.Navigation.NavigationSteps.Count)
            {
                return RedirectToAction("CheckConflicts", new { registrationKey });
            }

            stepKey = model.Navigation.NavigationSteps[nextStepIndex].StepKey;

            if (model.RegistrationStatus == "Edit")
            {
                return RedirectToAction("EditSessions", null, new { registrationKey, stepKey });
            }

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

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [Route("editconflicts/{registrationKey}")]
        public ActionResult EditCheckConflicts(Guid registrationKey)
        {
            var viewModel = RegistrationTasks.GetEditRegistrationNotAllowedConflictViewModel(registrationKey);

            if (viewModel.ConflictGroups.Any())
            {
                return View("Conflicts", viewModel);
            }

            viewModel = RegistrationTasks.GetEditRegistrationAllowedConflictViewModel(registrationKey);

            if (viewModel.ConflictGroups.Any())
            {
                return View("Conflicts", viewModel);
            }

            return Redirect($"{ApplicationConfig.ApplicationConfigManager.Settings.PaymentsUrl}registration/edit/user/{registrationKey}");

        }

        [HttpPost]
        [Route("resolve-conflicts")]
        public ActionResult ResolveConflicts(RegistrationConflictViewModel model)
        {
            var registrationKey = RegistrationTasks.SaveRegistrationSessionConflicts(model);

            if (model.RegistrationStatus == "Edit")
            {
                if (model.AllowedConflicts)
                {
                    return Redirect($"{ApplicationConfig.ApplicationConfigManager.Settings.PaymentsUrl}registration/edit/user/{registrationKey}");
                }

                return RedirectToAction("EditCheckConflicts", new { registrationKey });
            }

            if (model.AllowedConflicts)
            {
                return Redirect($"{ApplicationConfig.ApplicationConfigManager.Settings.PaymentsUrl}registration/{registrationKey}");
            }

            return RedirectToAction("CheckConflicts", new { registrationKey });
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [Route("confirmation/{registrationKey}")]
        public ActionResult Confirmation(Guid registrationKey, string status = "view")
        {
            var viewModel = RegistrationTasks.GetRegistrationConfirmationViewModel(registrationKey, status);

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

        [Route("edit/{registrationKey}/contact-info", Name = "EditContactInformation")]
        public ActionResult EditContactInformation(Guid registrationKey)
        {
            var viewModel = RegistrationTasks.GetRegistrationContactInfoViewModelForEdit(registrationKey);

            return PartialView("_EditContactInformation", viewModel);
        }

        [Route("edit/{registrationKey}/get-contact-info", Name = "GetContactInformation")]
        public ActionResult GetContactInformation(Guid registrationKey)
        {
            var viewModel = RegistrationTasks.GetRegistrationConfirmationViewModel(registrationKey, "View");

            return PartialView("_ContactInformation", viewModel);
        }

        [HttpPost]
        [Route("update/{registrationKey}/contact-info", Name = "UpdateContactInformation")]
        public ActionResult UpdateContactInformation(RegistrationContactInfoViewModel model)
        {
            var status = RegistrationTasks.UpdateContactInformation(model);
            var viewModel = RegistrationTasks.GetRegistrationConfirmationViewModel(model.RegistrationKey.Value, "View");
            viewModel.UpdateSuccessful = status;

            return PartialView("_ContactInformation", viewModel);
        }

        [Route("edit/{registrationKey}/sessions")]
        public ActionResult GetSessionsForEdit(Guid registrationKey)
        {
            var editSessions = RegistrationTasks.GetRegistrationSessionsForEdit(registrationKey);

            return View("EditedSessions", editSessions);
        }
    }
}