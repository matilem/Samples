using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.EditedRegistration
{
    public class EditedRegistrationDto
    {
        public Guid Key { get; set; }

        public Guid EventKey { get; set; }

        public Guid CustomerKey { get; set; }

        public Guid? CustomerAddressKey { get; set; }

        public Guid? CustomerPhoneKey { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactPhone { get; set; }

        public DateTime RegistrationDate { get; set; }

        public Guid? PriceKey { get; set; }

        public EditedRegistrationBadgeDto Badge { get; set; }

        public List<EditedRegistrationSessionDto> SelectedSessions { get; set; }
    }
}