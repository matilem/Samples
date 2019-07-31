using System;
using System.Collections.Generic;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class RegistrationHeadingDto
    {
        public Guid Key { get; set; }

        public int HeadingSequence { get; set; }

        public string HeadingHeading { get; set; }

        public string HeadingDescription { get; set; }

        public List<RegistrationSessionDto> Sessions { get; set; } 
    }
}