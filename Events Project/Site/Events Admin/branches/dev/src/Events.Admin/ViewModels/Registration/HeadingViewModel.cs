using System;
using System.Collections.Generic;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class HeadingViewModel
    {
        public Guid Key { get; set; }

        public int HeadingSequence { get; set; }

        public string HeadingHeading { get; set; }

        public string HeadingDescription { get; set; }

        public List<SessionViewModel> Sessions { get; set; }

        public bool RequiredFlag { get; set; }
    }
}