using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos.Customer;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class UserRegistrationStepDto
    {
        public Guid Key { get; set; }

        public Guid RegistrationKey { get; set; }

        public int StepSequence { get; set; }

        public string StepHeading { get; set; }

        public string StepDescription { get; set; }

        public Guid StepKey { get; set; }

        public decimal CurrentTotal { get; set; }

        public List<UserRegistrationHeadingDto> Headings { get; set; }

        public CustomerDto Customer { get; set; }

        public UserEventDto Event { get; set; }

        public List<EventStepDto> PendingSteps { get; set; }

        public Dictionary<Guid, UserEventDto> PendingEvents { get; set; }

        public string RegistrationStatus { get; set; }
    }
}