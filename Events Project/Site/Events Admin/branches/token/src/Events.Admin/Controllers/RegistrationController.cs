﻿using System;
using System.Web.Mvc;
using Aafp.Events.Admin.Filters;
using Aafp.Events.Admin.Tasks.Interfaces;
using Aafp.Events.Admin.ViewModels.Registration;

namespace Aafp.Events.Admin.Controllers
{
    [RoutePrefix("registration")]
    [SiteMinderAuthentication(Roles = new[] { "aafpstaff" })]
    public class RegistrationController : Controller
    {
        public IRegistrationTasks RegistrationTasks { get; set; }

        [Route("")]
        public ActionResult Index()
        {
            return RedirectToAction("Home");
        }

        [Route("home")]
        public ActionResult Home(string customerId)
        {
            var viewModel = RegistrationTasks.GetHomeViewModel();

            if (string.IsNullOrWhiteSpace(customerId))
            {
                return View(viewModel);
            }

            viewModel.SearchTerm = customerId;
            var results = RegistrationTasks.Search(viewModel);
            return View("SearchResults", results);
        }

        [HttpPost]
        [Route("search")]
        public ActionResult Search(UserSearchViewModel model)
        {
            var results = RegistrationTasks.Search(model);

            return View("SearchResults", results);
        }

        [Route("event/{eventKey}/customer/{customerKey}")]
        public ActionResult GetEventRegistrationTypeInfoByCustomer(Guid eventKey, Guid customerKey, DateTime registrationDate)
        {
            var viewModel = RegistrationTasks.GetEventRegistrationTypeInfoByCustomer(eventKey, customerKey, registrationDate);

            return PartialView("_EventDetails", viewModel);
        }

        [Route("pending/{registrationKey}")]
        public ActionResult PendingRegistration(Guid registrationKey)
        {
            var viewModel = RegistrationTasks.GetPendingRegistration(registrationKey);
            viewModel.CurrentUser = System.Web.HttpContext.Current.User.Identity.Name;

            return View("Registration", viewModel);
        }

        [Route("new")]
        public ActionResult NewRegistration(Guid eventKey, Guid customerKey, Guid registrationTypeKey, DateTime registrationDate)
        {
            var viewModel = RegistrationTasks.GetNewRegistration(eventKey, customerKey, registrationTypeKey, registrationDate);
            viewModel.CurrentUser = System.Web.HttpContext.Current.User.Identity.Name;

            if (viewModel.IsRegistered)
                return RedirectToAction("PaymentConfirmation", new { registrationKey = viewModel.Key });

            if (viewModel.IsSoldOut)
            {
                return View("Warning", viewModel);
            }

            return View("Registration", viewModel);
        }

        [Route("get-registrant-details/event/{eventKey}/customer/{customerKey}")]
        public ActionResult GetCustomerEventRegistration(Guid eventKey, Guid customerKey)
        {
            var registrantionInformation = RegistrationTasks.GetCustomerEventRegistration(eventKey, customerKey);

            return Json(registrantionInformation, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("update-new-registration/event/{eventKey}/customer/{customerKey}/type/{registrationTypeKey}")]
        public JsonResult UpdatePendingRegistration(Guid eventKey, Guid customerKey, Guid registrationTypeKey, DateTime newRegistrationDate, RegistrationViewModel registration)
        {
            var registrationKey = RegistrationTasks.UpdatePendingRegistration(eventKey, customerKey, registrationTypeKey, newRegistrationDate, registration);

            return Json(registrationKey, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("save-pending-registration")]
        public JsonResult SavePendingRegistration(RegistrationViewModel model)
        {
            var registrationKey = RegistrationTasks.SavePendingRegistration(model);

            return Json(registrationKey, JsonRequestBehavior.AllowGet);
        }

        [Route("edit/{registrationKey}")]
        public ActionResult EditRegistration(Guid registrationKey)
        {
            var viewModel = RegistrationTasks.GetEditRegistrationViewModel(registrationKey);

            return View(viewModel);
        }

        [Route("edit/save-registration")]
        public JsonResult SaveEditRegistration(EditRegistrationViewModel model)
        {
            var registrationKey = RegistrationTasks.SaveEditRegistration(model);

            return Json(registrationKey, JsonRequestBehavior.AllowGet);
        }

        [Route("print/{registrantKey}")]
        public PartialViewResult GetPrintBadgeSelectorViewModel(Guid registrantKey)
        {
            var viewModel = RegistrationTasks.GetPrintBadgeSelectorViewModel(registrantKey);

            return PartialView("_PrintBadgeSelector", viewModel);
        }

        [Route("email/{registrantKey}")]
        public PartialViewResult GetRegistrantEmail(Guid registrantKey)
        {
            var viewModel = RegistrationTasks.GetRegistrantEmailViewModel(registrantKey);

            return PartialView("_EmailConfirmation", viewModel);
        }

        [HttpPost]
        [Route("{registrantKey}/send-email")]
        public JsonResult SendConfirmationEmail(Guid registrantKey, string email)
        {
            var result = RegistrationTasks.SendConfirmationEmail(registrantKey, email);

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
        [Route("session/{sessionKey}/increase-capacity")]
        public PartialViewResult IncreaseSessionCapacity(Guid sessionKey)
        {
            var viewModel = RegistrationTasks.IncreaseSessionCapacity(sessionKey);

            return PartialView("_SessionCapacity", viewModel);
        }

        [Route("confirmation/{registrationKey}")]
        public ActionResult PaymentConfirmation(Guid registrationKey)
        {
            var viewModel = RegistrationTasks.GetPaymentConfirmationViewModel(registrationKey);

            return View("PaymentConfirmation", viewModel);
        }

        [Route("warning/{registrationKey}")]
        public ActionResult Warning(Guid registrationKey)
        {
            var viewModel = RegistrationTasks.GetPendingRegistration(registrationKey);
            viewModel.CurrentUser = System.Web.HttpContext.Current.User.Identity.Name;

            return View(viewModel);
        }

        [Route("batch/")]
        public ActionResult GetEventBatchRegistration()
        {
            var viewModel = RegistrationTasks.GetEventBatchRegistration();
            viewModel.CurrentUser = System.Web.HttpContext.Current.User.Identity.Name;

            return View("Batch", viewModel);
        }

        [Route("batch/registrationinfo/{eventKey}")]
        public ActionResult GetBatchEventRegistrationInfo(Guid eventKey)
        {
            var viewModel = RegistrationTasks.GetBatchEventRegistrationInfo(eventKey);

            return PartialView("_BatchEventDetails", viewModel);
        }

        [HttpPost]
        [Route("batch/registrationupload/{eventKey}/type/{registrationTypeKey}")]
        public PartialViewResult BatchEventRegistrationUpload(Guid eventKey, Guid registrationTypeKey, DateTime registrationDate)
        {
            var results = RegistrationTasks.BatchEventRegistrationUpload(Request, eventKey, registrationTypeKey, registrationDate);

            var viewModel = new BatchViewModel();
            viewModel.Registrants = results;

            return PartialView("_BatchUploadDetails", viewModel);
        }
    }
}