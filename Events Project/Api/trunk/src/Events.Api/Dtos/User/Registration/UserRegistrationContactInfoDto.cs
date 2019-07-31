using Aafp.Events.Api.Dtos.Customer;
using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class UserRegistrationContactInfoDto
    {
        public Guid RegistrationKey { get; set; }

        public Guid? SelectedAddressKey { get; set; }

        public Guid? SelectedPhoneKey { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactPhone { get; set; }

        public decimal CurrentTotal { get; set; }

        public RegistrationBadgeDto Badge { get; set; }

        public CustomerDto Customer { get; set; }

        public UserEventDto Event { get; set; }

        public string AlternativeCompanyBadgeLabel { get; set; }

        public string AlternativePositionBadgeLabel { get; set; }

        public bool DisplayBadgeCompany { get; set; }

        public bool DisplayBadgePosition { get; set; }

        public List<EventStepDto> PendingSteps { get; set; }

        public List<UserEventDto> RelatedRegistrationEvents { get; set; }
    }
}