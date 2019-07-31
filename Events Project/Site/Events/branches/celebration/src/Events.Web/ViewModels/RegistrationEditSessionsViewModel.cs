using System;
using System.Collections.Generic;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationEditSessionsViewModel : ViewModelBase
    {
        public Guid Key { get; set; }

        public Guid StepKey { get; set; }

        public int StepSequence { get; set; }

        public string StepHeading { get; set; }

        public string StepDescription { get; set; }

        public List<RegistrationHeadingViewModel> Headings { get; set; }

        public EventViewModel Event { get; set; }

        public string RegistrationStatus { get; set; }

        public string WebLogin { get; set; }
    }
}