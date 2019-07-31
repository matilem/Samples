using System;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationNavigationStepViewModel
    {
        public Guid StepKey { get; set; }

        public string StepLink { get; set; }

        public int StepSequence { get; set; }

        public string StepDescription { get; set; }

        public EventViewModel Event { get; set; }
    }
}