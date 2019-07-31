using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.Admin.Registration
{
    public class AdminRegistrationHeadingDto
    {
        public Guid Key { get; set; }

        public int HeadingSequence { get; set; }

        public string HeadingHeading { get; set; }

        public string HeadingDescription { get; set; }

        public List<AdminRegistrationSessionDto> Sessions { get; set; }

        public bool RequiredFlag { get; set; }
    }
}