using System;
using System.Web.Http;
using Aafp.Payments.Api.Dtos.Registration;
using Aafp.Payments.Api.Filters;
using Aafp.Payments.Api.Tasks.Interfaces;
using ApiClientHelper.Components;

namespace Aafp.Payments.Api.Controllers
{
    [RoutePrefix("registration")]
    public class RegistrationController : ApiController
    {
        public IRegistrationPaymentTasks EventPaymentTasks { get; set; }

        [Route("{pendingRegistrationKey}")]
        public IHttpActionResult GetPendingRegistrationForPayment(Guid pendingRegistrationKey)
        {
            var pendingRegistration = EventPaymentTasks.GetPendingRegistration(pendingRegistrationKey);

            if (pendingRegistration != null)
                return Ok(pendingRegistration);
            else
                return BadRequest("The requested pending registration could not be found. It may have already been processed.");
        }

        [Route("edited/{editedRegistrationKey}")]
        public IHttpActionResult GetEditedRegistrationForPayment(Guid editedRegistrationKey)
        {
            var editedRegistration = EventPaymentTasks.GetEditedRegistration(editedRegistrationKey);

            return Ok(editedRegistration);
        }

        [Route("discount/{discountPriceKey}")]
        public IHttpActionResult GetDiscountAmount(Guid discountPriceKey)
        {
            var discount = EventPaymentTasks.GetDiscount(discountPriceKey);

            return Ok(discount);
        }

        [Route("{priceKey}/discount/{discountCode}")]
        public IHttpActionResult GetDiscountAmount(Guid priceKey, string discountCode)
        {
            var discount = EventPaymentTasks.GetDiscount(priceKey, discountCode);

            if (discount != null)
                return Ok(discount);
            else
                return BadRequest("The provided discount is not available for this registration.");
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult ProcessPayment(RegistrationDto registration)
        {
            try
            {
                var result = EventPaymentTasks.ProcessPayment(registration);

                return Ok(result);
            }
            catch (RegistrationProcessingException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("edit")]
        public IHttpActionResult ProcessEditedPayment(RegistrationDto registration)
        {
            try
            {
                var result = EventPaymentTasks.ProcessEditedPayment(registration);

                return Ok(result);
            }
            catch (RegistrationProcessingException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("batch/event/{eventKey}/customer/{customerKey}/type/{registrationTypeKey}")]
        public IHttpActionResult ProcessPaymentForBatch(Guid eventKey, Guid customerKey, Guid registrationTypeKey, [FromUri] DateTime registrationDate)
        {
            try
            {
                var result = EventPaymentTasks.ProcessPaymentForBatch(eventKey, customerKey, registrationTypeKey, registrationDate);

                return Ok(result);
            }
            catch (RegistrationProcessingException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
