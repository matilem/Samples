using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos.Customer;

namespace Aafp.Events.Api.Dtos.Admin.Registration
{
    public class AdminEditedRegistrationDto
    {
        public Guid Key { get; set; }

        public Guid RegistrationKey { get; set; }

        public string RegistrationStatus { get; set; }

        public List<AdminRegistrationSessionDto> EditedSessions { get; set; }

        public List<AdminEditRegistrationGuestBadgeDto> EditedGuestBadges { get; set; }

        public bool PaymentRequired { get; set; }

        public EventDetailDto Event { get; set; }

        public CustomerDto Customer { get; set; } 
    }
}