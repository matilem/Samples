using System;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class UserRegistrationNavigationStepDto
    {
        public Guid Key { get; set; }

        public int StepSequence { get; set; }

        public string StepHeading { get; set; }

        public string StepDescription { get; set; }
    }
}