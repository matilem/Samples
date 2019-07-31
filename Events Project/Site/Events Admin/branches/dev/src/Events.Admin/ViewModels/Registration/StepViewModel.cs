using System.Collections.Generic;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class StepViewModel
    {
        public int StepSequence { get; set; }

        public string StepHeading { get; set; }

        public string StepDescription { get; set; }

        public IList<HeadingViewModel> Headings { get; set; }
    }
}