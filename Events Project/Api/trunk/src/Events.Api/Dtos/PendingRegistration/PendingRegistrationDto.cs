using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.PendingRegistration
{
    public class PendingRegistrationDto
    {
        public Guid Key { get; set; }

        public Guid EventKey { get; set; }

        public Guid CustomerKey { get; set; }

        public Guid? CusotmerAddressKey { get; set; }

        public Guid? CusotmerPhoneKey { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactPhone { get; set; }

        public DateTime RegistrationDate { get; set; }

        public Guid? PriceKey { get; set; }

        public PendingRegistrationBadgeDto Badge { get; set; }

        public List<PendingRegistrationSessionDto> SelectedSessions { get; set; } 
    }
}