using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos.Customer;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class UserRegistrationConfirmationDto
    {
        public Guid Key { get; set; }

        public Guid InvoiceKey { get; set; }

        public string InvoiceCode { get; set; }

        public Guid? SelectedAddressKey { get; set; }

        public Guid? SelectedPhoneKey { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactPhone { get; set; }

        public string Comment { get; set; }

        public string DiscountName { get; set; }

        public decimal DiscountAmount { get; set; }

        public EventFeeDto Fee { get; set; }

        public UserEventDto Event { get; set; }

        public CustomerDto Customer { get; set; }

        public RegistrationBadgeDto Badge { get; set; }

        public List<UserRegistrationSessionDto> Sessions { get; set; }

        public List<UserRegistrationConfirmationDto> RelatedRegistrations { get; set; }
    }
}