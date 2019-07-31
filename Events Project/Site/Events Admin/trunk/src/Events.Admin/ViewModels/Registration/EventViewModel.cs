using System;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class EventViewModel
    {
        public Guid Key { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? RemoveFromWebDate { get; set; }

        public DateTime? PostToWebDate { get; set; }

        public DateTime? EarlyRegistrationDate { get; set; }
    }
}