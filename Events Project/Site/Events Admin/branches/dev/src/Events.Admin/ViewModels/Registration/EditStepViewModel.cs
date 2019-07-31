using System.Collections.Generic;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class EditStepViewModel
    {
        public int StepSequence { get; set; }

        public string StepHeading { get; set; }

        public string StepDescription { get; set; }

        public IList<EditHeadingViewModel> Headings { get; set; }
    }
}