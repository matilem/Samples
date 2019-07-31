using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos
{
    public class EventStepDto
    {
        public Guid Key { get; set; }

        public int StepSequence { get; set; }

        public string StepHeading { get; set; }

        public string StepDescription { get; set; }

        public List<EventHeadingDto> Headings { get; set; }
    }
}