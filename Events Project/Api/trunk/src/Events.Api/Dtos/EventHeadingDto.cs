using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Events.Api.Dtos
{
    public class EventHeadingDto
    {
        public Guid Key { get; set; }

        public int HeadingSequence { get; set; }

        public string HeadingHeading { get; set; }

        public string HeadingDescription { get; set; }

    }
}