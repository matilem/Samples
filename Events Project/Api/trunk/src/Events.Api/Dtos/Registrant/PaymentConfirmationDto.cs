using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos.Admin.Registration;
using Aafp.Events.Api.Dtos.Customer;

namespace Aafp.Events.Api.Dtos.Registrant
{
    public class PaymentConfirmationDto
    {
        public Guid RegistrationKey { get; set; }

        public CustomerDto Customer { get; set; }

        public AdminRegistrationEventDto Event { get; set; }
        
        public List<AdminRegistrantInvoiceDetailsDto> InvoiceDetails { get; set; }

        public List<AdminRegistrantSessionDto> Sessions { get; set; }

        public List<PaymentConfirmationDto> RelatedRegistrations { get; set; }
    }
}