using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class EditUserRegistrationSessionsDto
    {
        public Guid Key { get; set; }

        public int StepSequence { get; set; }

        public string StepHeading { get; set; }

        public string StepDescription { get; set; }

        public Guid StepKey { get; set; }

        public List<UserRegistrationHeadingDto> Headings { get; set; }

        public  UserRegistrationEventDto Event { get; set; }

        public string RegistrationStatus { get; set; }

        public string WebLogin { get; set; }
    }
}