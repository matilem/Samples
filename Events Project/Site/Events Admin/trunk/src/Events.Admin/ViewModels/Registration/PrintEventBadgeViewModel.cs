using System;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class PrintEventBadgeViewModel : ViewModelBase
    {
        public Guid EventKey { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string EventTitle { get; set; }

        public string EventLocation { get; set; }
    }
}