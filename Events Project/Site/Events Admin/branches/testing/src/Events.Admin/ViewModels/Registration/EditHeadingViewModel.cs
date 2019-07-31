using System;
using System.Collections.Generic;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class EditHeadingViewModel
    {
        public Guid Key { get; set; }

        public int HeadingSequence { get; set; }

        public string HeadingHeading { get; set; }

        public string HeadingDescription { get; set; }

        public bool HeadingRequiredFlag { get; set; }

        public List<EditSessionViewModel> Sessions { get; set; }
    }
}