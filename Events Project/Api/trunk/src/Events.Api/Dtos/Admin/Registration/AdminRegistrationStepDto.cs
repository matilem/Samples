using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.Admin.Registration
{
    public class AdminRegistrationStepDto
    {
        public IList<AdminRegistrationHeadingDto> Headings { get; set; }

        public int StepSequence { get; set; }

        public string StepHeading { get; set; }

        public string StepDescription { get; set; }
    }
}