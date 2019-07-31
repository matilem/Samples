using System;

namespace Aafp.Events.Web.ViewModels
{
    public class EventStepViewModel
    {
        public Guid Key { get; set; }

        public int StepSequence { get; set; }

        public string StepHeading { get; set; }

        public string StepDescription { get; set; }
    }
}