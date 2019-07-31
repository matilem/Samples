using System;
using System.Collections.Generic;
using System.Web.Http;
using Aafp.Events.Api.Dtos;
using Aafp.Events.Api.Dtos.Admin.Registration;
using Aafp.Events.Api.Tasks.Admin.Interfaces;

namespace Aafp.Events.Api.Controllers
{
    [RoutePrefix("admin/registration")]
    public class AdminRegistrationController : ApiController
    {
        public IAdminRegistrationTasks AdminRegistrationTasks { get; set; }

        [Route("customer-search")]
        public IHttpActionResult GetSearchResults(string searchTerm)
        {
            var results = AdminRegistrationTasks.GetAdminCustomerSearchResults(searchTerm);

            return Ok(results);
        }

        [Route("events")]
        public IHttpActionResult GetAdminRegistrationEvents()
        {
            var events = AdminRegistrationTasks.GetAdminRegistrationEvents();

            return Ok(events);
        }

        [Route("registration-types/{eventKey}/customer/{customerKey}")]
        public IHttpActionResult GetEventRegistrationTypeInfoByCustomer(Guid eventKey, Guid customerKey, [FromUri] DateTime registrationDate)
        {
            var eventDetail = AdminRegistrationTasks.GetEventRegistrationTypesByCustomerKey(eventKey, customerKey, registrationDate);

            return Ok(eventDetail);
        }

        [Route("registration-types/{eventKey}/customer/by-weblogin/{webLogin}")]
        public IHttpActionResult GetEventRegistrationTypeInfoByWebLogin(Guid eventKey, string webLogin, [FromUri] DateTime registrationDate)
        {
            var eventDetail = AdminRegistrationTasks.GetEventRegistrationTypesByWebLogin(eventKey, webLogin, registrationDate);

            return Ok(eventDetail);
        }

        [Route("pending/{registrationKey}")]
        public IHttpActionResult GetPendingRegistration(Guid registrationKey)
        {
            var registration = AdminRegistrationTasks.GetRegistrationFromPendingRegistration(registrationKey);

            if (registration != null)
                return Ok(registration);
            else
                return
                    BadRequest(
                        "The requested pending registration could not be found. It may have already been processed.");
        }

        [Route("edit/{registrationKey}")]
        public IHttpActionResult GetEditRegistration(Guid registrationKey)
        {
            var registration = AdminRegistrationTasks.GetRegistrationFromEditedRegistration(registrationKey);

            if (registration != null)
                return Ok(registration);
            else
                return
                    BadRequest(
                        "The requested edited registration could not be found. It may have already been processed.");
        }

        [Route("edited-sessions/{registrationKey}")]
        public IHttpActionResult GetEditedSessions(Guid registrationKey)
        {
            var registration = AdminRegistrationTasks.GetEditedSessions(registrationKey);

            return Ok(registration);
        }

        [Route("new")]
        public IHttpActionResult GetNewRegistration([FromUri] Guid eventKey, [FromUri] Guid customerKey, [FromUri] Guid registrationTypeKey, [FromUri] DateTime registrationDate)
        {
            var pendingRegistration = AdminRegistrationTasks.GetPendingRegistrationByEvent(eventKey, customerKey);

            if (pendingRegistration != null)
            {
                var registration = AdminRegistrationTasks.GetRegistrationFromPendingRegistration(pendingRegistration.Key);

                return Ok(registration);
            }

            var newRegistration = AdminRegistrationTasks.GetNewRegistration(eventKey, customerKey, registrationTypeKey, registrationDate);

            return Ok(newRegistration);
        }

        [HttpPost]
        [Route("save-registration")]
        public IHttpActionResult SaveRegistration(AdminRegistrationDto registration)
        {
            var savedRegistration = AdminRegistrationTasks.SaveRegistration(registration);

            return Ok(savedRegistration);
        }

        [Route("{registrationKey}")]
        public IHttpActionResult GetRegistration(Guid registrationKey)
        {
            var registration = AdminRegistrationTasks.GetRegistration(registrationKey);

            return Ok(registration);
        }

        [Route("print/{registrantKey}")]
        public IHttpActionResult GetRegistrantForPrinting(Guid registrantKey)
        {
            var registrant = AdminRegistrationTasks.GetRegistrantForPrinting(registrantKey);

            return Ok(registrant);
        }

        [Route("payment-confirmation/{registrationKey}")]
        public IHttpActionResult GetPaymentConfirmation(Guid registrationKey)
        {
            var registrant = AdminRegistrationTasks.GetPaymentConfirmation(registrationKey);

            return Ok(registrant);
        }

        [HttpPost]
        [Route("{registrationKey}/update-contact-info")]
        public IHttpActionResult UpdateContactInfo(Guid registrationKey, [FromBody] RegistrationContactDto contactInfo)
        {
            var status = AdminRegistrationTasks.SaveEmergencyContactInformation(registrationKey, contactInfo.ContactName, contactInfo.ContactPhone);

            if (status)
                return Ok();
            else
                return InternalServerError();
        }

        [HttpPost]
        [Route("{registrationKey}/update-badge-notes")]
        public IHttpActionResult UpdateBadgeNotes(Guid registrationKey, [FromBody] RegistrationBadgeDto badgeInfo)
        {
            var status = AdminRegistrationTasks.SaveBadgeNotes(registrationKey, badgeInfo.Notes);

            if (status)
                return Ok();
            else
                return InternalServerError();
        }

        [HttpPost]
        [Route("{registrationKey}/badges/guest")]
        public IHttpActionResult SaveGuestBadges(Guid registrationKey, [FromBody] List<AdminRegistrationGuestBadgeDto> guestBadges)
        {
            var status = AdminRegistrationTasks.SaveGuestBadges(registrationKey, guestBadges);

            if (status)
                return Ok(status);
            else
                return InternalServerError();
        }

        [HttpPost]
        [Route("edit/{registrationKey}/badges/guest")]
        public IHttpActionResult SaveEditedGuestBadges(Guid registrationKey, [FromBody] List<AdminRegistrationGuestBadgeDto> guestBadges)
        {
            var status = AdminRegistrationTasks.SaveEditedGuestBadges(registrationKey, guestBadges);

            if (status)
                return Ok(status);
            else
                return InternalServerError();
        }

        [HttpPost]
        [Route("confirmation-email/{registrationKey}")]
        public IHttpActionResult SendConfirmationEmail(Guid registrationKey, [FromBody] string email)
        {
            var status = AdminRegistrationTasks.SendConfirmationEmail(registrationKey, email);

            if (status)
                return Ok(status);
            else
                return InternalServerError();
        }

        [HttpPost]
        [Route("add-to-wait-list")]
        public IHttpActionResult AddToWaitList([FromBody] WaitListDto dto)
        {
            var status = AdminRegistrationTasks.AddToWaitList(dto);

            if (status)
                return Ok(status);
            else
                return InternalServerError();
        }

        [Route("get-registrant/{eventKey}/{customerKey}")]
        public IHttpActionResult GetCustomerEventRegistration(Guid eventKey, Guid customerKey)
        {
            var registrant = AdminRegistrationTasks.GetCustomerEventRegistration(eventKey, customerKey);

            return Ok(registrant);
        }

        [Route("batch/get-eventinfo/{eventKey}")]
        public IHttpActionResult GetBatchEventRegistrationInfo(Guid eventKey)
        {
            var eventDetail = AdminRegistrationTasks.GetBatchEventRegistrationInfo(eventKey);

            return Ok(eventDetail);
        }

        [HttpGet]
        [Route("batch/save-registration/event/{eventKey}/customer/{memberId}/type/{registrationTypeKey}")]

        public IHttpActionResult SaveBatchEventRegistration(string memberId, Guid eventKey, Guid registrationTypeKey, [FromUri] DateTime registrationDate)
        {
            var batchCustomers = AdminRegistrationTasks.SaveBatchEventRegistration(memberId, eventKey, registrationTypeKey, registrationDate);

            return Ok(batchCustomers);
        }

        [Route("get-registrant/edit/{eventKey}/{customerKey}")]
        public IHttpActionResult GetCustomerEventRegistrationForEdit(Guid eventKey, Guid customerKey)
        {
            var registrant = AdminRegistrationTasks.GetCustomerEventRegistrationForEdit(eventKey, customerKey);

            return Ok(registrant);
        }

        [HttpPost]
        [Route("pending/remove/{pendingRegistrationKey}")]
        public IHttpActionResult RemovePendingRegistration(Guid pendingRegistrationKey)
        {
            var status = AdminRegistrationTasks.RemovePendingRegistration(pendingRegistrationKey);

            if (status)
                return Ok(status);
            else
                return InternalServerError();
        }
    }
}
