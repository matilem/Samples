using System;
using System.Collections.Generic;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationStepViewModel : ViewModelBase
    {
        public Guid Key { get; set; }

        public int StepSequence { get; set; }

        public string StepHeading { get; set; }

        public string StepDescription { get; set; }

        public Guid StepKey { get; set; }

        public List<RegistrationHeadingViewModel> Headings { get; set; }

        public CustomerViewModel Customer { get; set; }

        public EventViewModel Event { get; set; }

        public List<EventStepViewModel> PendingSteps { get; set; }

        public Dictionary<Guid, EventViewModel> PendingEvents { get; set; }

        public string RegistrationStatus { get; set; }

        public bool PayNow { get; set; }
    }
}