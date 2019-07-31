using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos.Customer;

namespace Aafp.Events.Api.Dtos.Admin.Registration
{
    public class AdminRegistrationDto
    {
        public Guid Key { get; set; }

        public string RegistrationStatus { get; set; }

        public string CurrentUser { get; set; }

        public Guid? CustomerAddressKey { get; set; }

        public Guid? CustomerPhoneKey { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactPhone { get; set; }

        public DateTime RegistrationDate { get; set; }

        public Guid? PriceKey { get; set; }

        public RegistrationBadgeDto Badge { get; set; }

        public CustomerDto Customer { get; set; }

        public AdminRegistrationEventDto Event { get; set; }

        public bool IsRegistered { get; set; }

        public bool IsSoldOut { get; set; }

        public bool AllowWaitList { get; set; }

        public bool UserIsOnWaitList { get; set; }

        public List<AdminRegistrationDto> RelatedRegistrations { get; set; }

        public List<AdminRegistrationSessionDto> UnavailableSessions { get; set; }

        public bool PaymentRequired { get; set; }

        public IList<AdminRegistrantInvoiceDetailsDto> InvoiceDetails { get; set; }
    }
}