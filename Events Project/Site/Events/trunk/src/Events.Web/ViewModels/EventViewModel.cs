using System;
using System.Collections.Generic;

namespace Aafp.Events.Web.ViewModels
{
    public class EventViewModel
    {
        public Guid Key { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public string LocationCity { get; set; }

        public string LocationState { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? RemoveFromWebDate { get; set; }

        public string DescriptionHtml { get; set; }

        public bool DisplayBadgeCompany { get; set; }

        public string AlternativeCompanyBadgeLabel { get; set; }

        public bool DisplayBadgePosition { get; set; }

        public string AlternativePositionBadgeLabel { get; set; }

        public bool AllowWaitList { get; set; }

        public bool IsSoldOut { get; set; }

        public bool IsOpenForRegistration { get; set; }

        public string HousingUrl { get; set; }

        public string HousingDescription { get; set; }

        public string HousingDiscountCode { get; set; }

        public string DenialMessage { get; set; }

        public bool TinyRegFlag { get; set; }

        public string TinyRegMessage { get; set; }

        public List<EventFeeViewModel> Fees { get; set; }

        public List<EventStepViewModel> Steps { get; set; }

        public string MarketingMessage { get; set; }

        public string LocationDisplay => $"{LocationCity}, {LocationState}";

        public string DateDisplay
        {
            get
            {
                var display = string.Empty;

                if (StartDate.HasValue)
                {
                    display = $"{StartDate.Value.ToString("ddd, MMM dd, yyyy")}";

                    if (EndDate.HasValue)
                        display += $" - {EndDate.Value.ToString("ddd, MMM dd, yyyy")}";
                }
                else
                {
                    display = "n/a";
                }

                return display;
            }
        }

        public string RemoveFromWebDateDisplay
        {
            get
            {
                var display = string.Empty;

                if (RemoveFromWebDate.HasValue)
                {
                    display = $"{RemoveFromWebDate.Value.ToString("ddd, MMM dd, yyyy")}";
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