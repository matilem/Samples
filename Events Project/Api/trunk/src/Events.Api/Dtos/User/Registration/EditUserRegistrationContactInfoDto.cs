using System;
using Aafp.Events.Api.Dtos.Customer;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class EditUserRegistrationContactInfoDto
    {
        public Guid RegistrationKey { get; set; }

        public Guid? SelectedAddressKey { get; set; }

        public Guid? SelectedPhoneKey { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactPhone { get; set; }

        public RegistrationBadgeDto Badge { get; set; }

        public CustomerDto Customer { get; set; }

        public EventBaseDto Event { get; set; }

        public string AlternativeCompanyBadgeLabel { get; set; }

        public string AlternativePositionBadgeLabel { get; set; }

        public bool DisplayBadgeCompany { get; set; }

        public bool DisplayBadgePosition { get; set; }
    }
}