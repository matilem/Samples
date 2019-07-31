using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos.Customer;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class UserEditedRegistrationDto
    {
        public Guid Key { get; set; }

        public string RegistrationStatus { get; set; }

        public List<UserRegistrationSessionDto> EditedSessions { get; set; }

        public List<EditedRegistrationGuestBadgeDto> EditedGuestBadges { get; set; }

        public bool PaymentRequired { get; set; }

        public EventDetailDto Event { get; set; }

        public CustomerDto Customer { get; set; }
    }
}