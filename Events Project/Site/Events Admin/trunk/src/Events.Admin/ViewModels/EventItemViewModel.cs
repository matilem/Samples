using System;

namespace Aafp.Events.Admin.ViewModels
{
    public class EventListItemViewModel
    {
        public Guid Key { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public string LocationCity { get; set; }

        public string LocationState { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CutOffDate { get; set; }

        public string TitleDisplay => $"{Title} - {Code}";

        public string LocationDisplay => $"{LocationCity}, {LocationState}";

        public string StartDateDisplay => StartDate?.ToShortDateString();

        public string CutOffDateDisplay => CutOffDate?.ToShortDateString();
    }
}