using System;
using System.Collections.Generic;
using Aafp.Events.Admin.ViewModels.Registration;

namespace Aafp.Events.Admin.ViewModels
{
    public class EventDetailViewModel
    {
        public Guid Key { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public string LocationCity { get; set; }

        public string LocationState { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CutOffDate { get; set; }

        public bool DisplayBadgeCompany { get; set; }

        public string AlternativeCompanyBadgeLabel { get; set; }

        public bool DisplayBadgePosition { get; set; }

        public string AlternativePositionBadgeLabel { get; set; }

        public List<EventFeeViewModel> Fees { get; set; }

        public List<StepViewModel> Steps { get; set; }

        public string Time { get; set; }

        public string TitleDisplay => $"{Title} - {Code}";

        public string LocationDisplay => $"{LocationCity}, {LocationState}";

        public string StartDateDisplay => StartDate?.ToShortDateString();

        public string CutOffDateDisplay => CutOffDate?.ToShortDateString();

        public string DateDisplay
        {
            get
            {
                var display = string.Empty;

                if (StartDate.HasValue)
                {
                    display = $"{StartDate.Value.ToString("ddd, MMM dd, yyyy")} </br> {Time}";
                }
                else
                {
                    display = "n/a";
                }

                return display;
            }
        }
    }
}