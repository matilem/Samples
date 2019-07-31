using System;
using Aafp.Payments.Api.Dtos.Registration;
using Aafp.Payments.Api.Models;

namespace Aafp.Payments.Api.Tasks.Interfaces
{
    public interface IRegistrationPaymentTasks
    {
        RegistrationDto GetPendingRegistration(Guid key);

        RegistrationDto GetEditedRegistration(Guid key);

        Discount GetDiscount(Guid discountPriceKey);

        Discount GetDiscount(Guid priceKey, string discountCode);

        Guid ProcessPayment(RegistrationDto dto);

        Guid ProcessEditedPayment(RegistrationDto dto);

        Guid Register(RegistrationDto pendingRegistration, bool sendConfirmationEmail);

        Guid ProcessPaymentForBatch(Guid eventKey, Guid customerKey, Guid registrationTypeKey, DateTime registrationDate);
    }
}
