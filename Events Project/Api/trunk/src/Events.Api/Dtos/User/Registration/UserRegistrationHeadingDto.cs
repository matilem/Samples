using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class UserRegistrationHeadingDto
    {
        public Guid Key { get; set; }

        public int HeadingSequence { get; set; }

        public string HeadingHeading { get; set; }

        public string HeadingDescription { get; set; }

        public List<UserRegistrationSessionDto> Sessions { get; set; }

        public bool RequiredFlag { get; set; }

        public string RegistrationStatus { get; set; }
    }
}