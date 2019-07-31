using System;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationViewModel
    {
        public Guid Key { get; set; }

        public Guid EventKey { get; set; }

        public string EventTitle { get; set; }

        public string EventCode { get; set; }

        public string EventCity { get; set; }

        public string EventState { get; set; }

        public DateTime? EventStartDate { get; set; }

        public DateTime? EventEndDate { get; set; }

        public DateTime? PostToWebDate { get; set; }

        public DateTime? RemoveFromWebDate { get; set; }

        public string EventDescriptionHtml { get; set; }

        public int Capacity { get; set; }

        public bool IsRegistered { get; set; }

        public bool IsSoldOut { get; set; }

        public bool AllowWaitList { get; set; }

        public bool UserIsOnWaitList { get; set; }

        public string Type { get; set; }

        public string LocationDisplay => $"{EventCity}, {EventState}";

        public string TitleDisplay => $"{EventTitle} - {EventCode}";

        public string DateDisplay
        {
            get
            {
                var display = string.Empty;

                if (EventStartDate.HasValue)
                {
                    display = $"{EventStartDate.Value.ToString("ddd, MMM dd, yyyy")}";

                    if (EventEndDate.HasValue)
                        display += $" - {EventEndDate.Value.ToString("ddd, MMM dd, yyyy")}";
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