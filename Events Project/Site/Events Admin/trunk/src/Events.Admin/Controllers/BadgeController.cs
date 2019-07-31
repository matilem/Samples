using System;
using System.Web.Mvc;
using Aafp.Events.Admin.Filters;
using Aafp.Events.Admin.Tasks.Interfaces;
using Aafp.Events.Admin.ViewModels.Registration;

namespace Aafp.Events.Admin.Controllers
{
    [RoutePrefix("badge")]
    [SiteMinderAuthentication(Roles = new[] { "aafpstaff" })]
    public class BadgeController : Controller
    {
        public IBadgeTasks BadgeTasks { get; set; }
        
        [Route("registrant/{registrantkey}/pdf")]
        public ActionResult GetRegistrantPdf(Guid registrantkey)
        {
            var pdf = BadgeTasks.GetRegistrantBadgePdf(registrantkey);

            if (pdf.Data != null)
                return File(pdf.Data, "application/pdf");
            else
                return View("BadgeError", pdf);
        }

        [Route("registrant/{registrantkey}/pdf/all")]
        public ActionResult GetRegistrantPdfAll(Guid registrantkey)
        {
            var pdf = BadgeTasks.GetRegistrantBadgePdfAll(registrantkey);

            if (pdf.Data != null)
                return File(pdf.Data, "application/pdf");
            else
                return View("BadgeError", pdf);
        }

        [Route("session/{sessionkey}/pdf")]
        public ActionResult GetRegistrantSessionBadgePdf(Guid sessionkey)
        {
            var pdf = BadgeTasks.GetRegistrantSessionBadgePdf(sessionkey);

            if (pdf.Data != null)
                return File(pdf.Data, "application/pdf");
            else
                return View("BadgeError", pdf);
        }

        [Route("registrant/{registrantKey}/sessions/pdf")]
        public ActionResult GetRegistrantSessionBadgePdfs(Guid registrantKey)
        {
            var pdf = BadgeTasks.GetRegistrantSessionBadgePdfs(registrantKey);

            if (pdf.Data != null)
                return File(pdf.Data, "application/pdf");
            else
                return View("BadgeError", pdf);
        }

        [Route("event/{eventKey}/pdf")]
        public ActionResult PrintEventPdfs(Guid eventKey)
        {
            var viewModel = BadgeTasks.GetPrintEventBadgeViewModel(eventKey);

            return View(viewModel);
        }

        [HttpPost]
        [Route("event/pdf")]
        public ActionResult GetEventPdfs(PrintEventBadgeViewModel model)
        {
            var pdf = BadgeTasks.GetEventPdfs(model);

            if (pdf.Data != null)
                return File(pdf.Data, "application/pdf");
            else
                return View("BadgeError", pdf);
        }

        [Route("invoice/{invoiceKey}/pdf")]
        public ActionResult GetInvoicePdf(Guid invoiceKey)
        {
            var pdf = BadgeTasks.GetInvoice(invoiceKey);

            if (pdf.Data != null)
                return File(pdf.Data, "application/pdf");
            else
                return View("BadgeError", pdf);
        }
    }
}