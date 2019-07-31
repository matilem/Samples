using System;

namespace Aafp.Events.Web.ViewModels
{
    public class EventHeadingViewModel
    {
        public Guid Key { get; set; }

        public int HeadingSequence { get; set; }

        public string HeadingHeading { get; set; }

        public string HeadingDescription { get; set; }
    }
}