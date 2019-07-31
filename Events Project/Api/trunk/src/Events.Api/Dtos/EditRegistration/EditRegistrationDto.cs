using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos.User.Registration;

namespace Aafp.Events.Api.Dtos.EditRegistration
{
    public class EditRegistrationDto
    {
        public Guid Key { get; set; }

        public Guid EventKey { get; set; }

        public Guid CustomerKey { get; set; }

        public virtual Guid? CustomerAddressKey { get; set; }

        public virtual Guid? CustomerPhoneKey { get; set; }

        public virtual string EmergencyContactName { get; set; }

        public virtual string EmergencyContactPhone { get; set; }

        public virtual EditRegistrationBadgeDto Badge { get; set; }

        public virtual IList<EditRegistrationGuestBadgeDto> GuestBadges { get; set; }

        public List<EditUserRegistrationSessionsDto> SelectedSessions { get; set; }
    }
}