using System;
using System.Web.Http;
using Aafp.Events.Api.Dtos.User.Registration;
using Aafp.Events.Api.Tasks.User.Interfaces;

namespace Aafp.Events.Api.Controllers
{
    [RoutePrefix("registration")]
    public class UserRegistrationController : ApiController
    {
        public IUserRegistrationTasks UserRegistrationTasks { get; set; }

        [Route("{webLogin}")]
        public IHttpActionResult GetRegistrationsForUserProfile(string webLogin)
        {
            var registrations = UserRegistrationTasks.GetRegistrationsForUserProfile(webLogin);

            return Ok(registrations);
        }

        [Route("home/{webLogin}")]
        public IHttpActionResult GetRegistrationHome(string webLogin)
        {
            var home = UserRegistrationTasks.GetRegistrationHome(webLogin);

            return Ok(home);
        }

        [Route("new/intro/{eventCode}/user/{webLogin}")]
        public IHttpActionResult GetNewRegistrationIntro(string eventCode, string webLogin)
        {
            var intro = UserRegistrationTasks.GetNewRegistrationIntro(eventCode, webLogin);

            if (intro == null)
                return BadRequest($"Event {eventCode} could not be found in the system.");

            return Ok(intro);
        }

        [Route("intro/{registrationKey}")]
        public IHttpActionResult GetRegistrationIntro(Guid registrationKey)
        {
            var intro = UserRegistrationTasks.GetRegistrationIntro(registrationKey);

            if (intro == null)
                return BadRequest($"The system could not find a pending registration with the provided key {registrationKey}.");

            return Ok(intro);
        }

        [HttpPost]
        [Route("save/intro")]
        public IHttpActionResult SaveRegistrationIntro([FromBody] UserRegistrationIntroDto dto)
        {
            var registrationKey = UserRegistrationTasks.SaveRegistrationIntro(dto);

            return Ok(registrationKey);
        }

        [Route("{registrationKey}/contact-info")]
        public IHttpActionResult GetRegistrationContactInfo(Guid registrationKey)
        {
            var contactInfo = UserRegistrationTasks.GetRegistrationContactInfo(registrationKey);

            return Ok(contactInfo);
        }

        [HttpPost]
        [Route("save/contact-info")]
        public IHttpActionResult SaveRegistrationContactInfo([FromBody] UserRegistrationContactInfoDto dto)
        {
            var registrationKey = UserRegistrationTasks.SaveRegistrationContactInfo(dto);

            return Ok(registrationKey);
        }

        [Route("{registrationKey}/step/{stepKey}")]
        public IHttpActionResult GetRegistrationStep(Guid registrationKey, Guid stepKey)
        {
            var step = UserRegistrationTasks.GetRegistrationStep(registrationKey, stepKey);

            return Ok(step);
        }

        [Route("edit/{registrationKey}/step/{stepKey}")]
        public IHttpActionResult GetEditRegistrationStep(Guid registrationKey, Guid stepKey)
        {
            var step = UserRegistrationTasks.GetEditRegistrationStep(registrationKey, stepKey);

            return Ok(step);
        }

        [HttpPost]
        [Route("save/sessions")]
        public IHttpActionResult SaveRegistrationSessions([FromBody] UserRegistrationStepDto dto)
        {
            var registration = UserRegistrationTasks.SaveRegistrationSessions(dto);

            return Ok(registration);
        }

        [Route("{registrationKey}/session-allowed-conflicts")]
        public IHttpActionResult GetRegistrationSessionAllowedConflicts(Guid registrationKey)
        {
            var conflicts = UserRegistrationTasks.GetRegistrationSessionConflicts(registrationKey, 0);

            return Ok(conflicts);
        }

        [Route("{registrationKey}/session-not-allowed-conflicts")]
        public IHttpActionResult GetRegistrationSessionNotAllowedConflicts(Guid registrationKey)
        {
            var conflicts = UserRegistrationTasks.GetRegistrationSessionConflicts(registrationKey, 1);

            return Ok(conflicts);
        }

        [Route("edit/{registrationKey}/session-allowed-conflicts")]
        public IHttpActionResult GetEditRegistrationSessionAllowedConflicts(Guid registrationKey)
        {
            var conflicts = UserRegistrationTasks.GetEditRegistrationSessionConflicts(registrationKey, 0);

            return Ok(conflicts);
        }

        [Route("edit/{registrationKey}/session-not-allowed-conflicts")]
        public IHttpActionResult GetEditRegistrationSessionNotAllowedConflicts(Guid registrationKey)
        {
            var conflicts = UserRegistrationTasks.GetEditRegistrationSessionConflicts(registrationKey, 1);

            return Ok(conflicts);
        }


        [HttpPost]
        [Route("save/session-conflicts")]
        public IHttpActionResult SaveRegistrationSessionConflicts([FromBody] UserRegistrationConflictDto dto)
        {
            var registration = UserRegistrationTasks.ResolveSessionConflicts(dto);

            return Ok(registration);
        }

        [Route("{registrationKey}/confirmation/{status}")]
        public IHttpActionResult GetRegistrationConfirmation(Guid registrationKey, string status)
        {
            var confirmation = UserRegistrationTasks.GetRegistrationConfirmation(registrationKey, status);

            return Ok(confirmation);
        }

        [HttpPost]
        [Route("confirmation-email/{registrationKey}")]
        public IHttpActionResult SendConfirmationEmail(Guid registrationKey)
        {
            var status = UserRegistrationTasks.SendConfirmationEmail(registrationKey);

            if (status)
                return Ok(status);
            else
                return InternalServerError();
        }

        [HttpPost]
        [Route("comments/{registrationKey}")]
        public IHttpActionResult SaveUserComments(Guid registrationKey, [FromBody] string comments)
        {
            var status = UserRegistrationTasks.SaveUserComments(registrationKey, comments);

            if (status)
                return Ok(status);
            else
                return InternalServerError();
        }

        [Route("edit/{registrationKey}/contact-info")]
        public IHttpActionResult GetRegistrationContactInfoForEdit(Guid registrationKey)
        {
            var contactInfo = UserRegistrationTasks.GetRegistrationContactInfoForEdit(registrationKey);

            return Ok(contactInfo);
        }

        [HttpPost]
        [Route("update/{registrationKey}/contact-info")]
        public IHttpActionResult UpdateRegistrationContactInfo([FromBody] UserRegistrationContactInfoDto dto)
        {
            var status = UserRegistrationTasks.UpdateRegistrationContactInfo(dto);

            if (status)
                return Ok(status);
            else
                return InternalServerError();
        }


        [Route("edit/{registrationKey}/sessions")]
        public IHttpActionResult GetRegistrationSessionsForEdit(Guid registrationKey)
        {
            var editSessionsInfo = UserRegistrationTasks.GetRegistrationSessionsForEdit(registrationKey);

            return Ok(editSessionsInfo);
        }

        [Route("edited-sessions/{registrationKey}")]
        public IHttpActionResult GetEditedSessions(Guid registrationKey)
        {
            var registration = UserRegistrationTasks.GetEditedSessions(registrationKey);

            return Ok(registration);
        }

        [Route("get-registrant/edit/{eventKey}/{customerKey}")]
        public IHttpActionResult GetCustomerEventRegistrationForEdit(Guid eventKey, Guid customerKey)
        {
            var registrant = UserRegistrationTasks.GetCustomerEventRegistrationForEdit(eventKey, customerKey);

            return Ok(registrant);
        }
    }
}
