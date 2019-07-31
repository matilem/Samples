using System;
using System.Collections.Generic;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationIntroViewModel : ViewModelBase
    {
        public Guid? SelectedPriceKey { get; set; }

        public bool UserIsOnWaitList { get; set; }

        public bool IsPending { get; set; }

        public bool IsRegistered { get; set; }

        public bool IsEligible { get; set; }

        public CustomerViewModel Customer { get; set; }

        public EventViewModel Event { get; set; }

        public List<RegistrationIntroViewModel> RelatedRegistrations { get; set; }

        public List<EventStepViewModel> PendingSteps { get; set; }

        public decimal CurrentSessionsCost { get; set; }

        public string Status { get; set; }
    }
}