using System;
using System.Collections.Generic;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationContactInfoViewModel : ViewModelBase
    {
        public Guid? SelectedAddressKey { get; set; }

        public Guid? SelectedPhoneKey { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactPhone { get; set; }

        public CustomerViewModel Customer { get; set; }

        public EventViewModel Event { get; set; }

        public RegistrationBadgeViewModel Badge { get; set; }

        public string AlternativeCompanyBadgeLabel { get; set; }

        public string AlternativePositionBadgeLabel { get; set; }

        public bool DisplayBadgeCompany { get; set; }

        public bool DisplayBadgePosition { get; set; }

        public List<EventStepViewModel> PendingSteps { get; set; }

        public List<EventViewModel> RelatedRegistrationEvents { get; set; }
    }
}