using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Aafp.Events.Api.Models.Badges;
using Aafp.Events.Api.Tasks.Admin.Interfaces;
using Aafp.Events.Api.Tasks.Interfaces;

namespace Aafp.Events.Api.Controllers
{
    [RoutePrefix("admin/badge")]
    public class AdminBadgeController : ApiController
    {
        public IAdminBadgeTasks AdminBadgeTasks { get; set; }

        public IPdfTasks PdfTasks { get; set; }

        [Route("registrant/{registrantkey}/pdf")]
        public HttpResponseMessage GetRegistrantPdf(Guid registrantkey)
        {
            var badge = AdminBadgeTasks.GetRegistrantBadge(registrantkey);
            var pdf = PdfTasks.GetPdf(new List<BadgeBase> { badge });
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(pdf);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }

        [Route("registrant/{registrantkey}/pdf/all")]
        public HttpResponseMessage GetRegistrantPdfAll(Guid registrantkey)
        {
            var badges = AdminBadgeTasks.GetAllRegistrantBadges(registrantkey);
            var pdf = PdfTasks.GetPdf(badges);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(pdf);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }

        [Route("session/unsold/{sessionKey}/pdf")]
        public HttpResponseMessage GetUnsoldSessionBadgePdf(Guid sessionkey)
        {
            var badge = AdminBadgeTasks.GetUnsoldSessionBadge(sessionkey);
            var pdf = PdfTasks.GetPdf(new List<BadgeBase> { badge });
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(pdf);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }

        [Route("session/{sessionKey}/pdf")]
        public HttpResponseMessage GetRegistrantSessionBadgePdf(Guid sessionkey)
        {
            var badges = AdminBadgeTasks.GetRegistrantSessionBadge(sessionkey);
            var pdf = PdfTasks.GetPdf(badges);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(pdf);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }

        [Route("registrant/{registrantKey}/sessions/pdf")]
        public HttpResponseMessage GetRegistrantSessionBadgePdfs(Guid registrantKey)
        {
            var badges = AdminBadgeTasks.GetRegistrantSessionBadges(registrantKey);
            var pdf = PdfTasks.GetPdf(badges);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(pdf);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }

        [Route("event/{eventkey}/pdf")]
        public HttpResponseMessage GetEventPdfs(Guid eventkey, [FromUri] DateTime? startDate, [FromUri] DateTime? endDate)
        {
            var badges = AdminBadgeTasks.GetEventBadges(eventkey, startDate, endDate);
            var pdf = PdfTasks.GetPdf(badges);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(pdf);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }
    }
}
