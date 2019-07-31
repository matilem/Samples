using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationSessionViewModel
    {
        public Guid Key { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public DateTime? Date { get; set; }

        public string LearningObjectives { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int Capacity { get; set; }

        public bool Ticketed { get; set; }

        public int RegisteredTicketsTotal { get; set; }

        public string SessionTypeCode { get; set; }

        public List<RegistrationSessionConflictViewModel> Conflicts { get; set; }

        public int MaxTicketQuantity { get; set; }

        public bool? PrintTicket { get; set; }

        public SessionFeeViewModel Fee { get; set; }

        public int SelectedQuantity { get; set; }

        public bool Selected { get; set; }

        public bool Removed { get; set; }

        public bool IsRegistered { get; set; }

        public bool ShowCost { get; set; }

        public bool ShowAvailableTickets { get; set; }

        public bool ShowTime { get; set; }

        public bool ShowNumber { get; set; }

        public List<RegistrationGuestBadgeViewModel> GuestBadges { get; set; }

        public Guid RequiredSession { get; set; }

        public string RegistrationStatus { get; set; }

        public string Time
        {
            get
            {
                if (StartTime != null && EndTime != null)
                    return StartTime + " - " + EndTime;
                else if (StartTime != null)
                    return StartTime;
                else
                    return string.Empty;
            }
        }

        public string DateTimeDisplay
        {
            get
            {
                var display = string.Empty;

                if (Date.HasValue)
                {
                    display = $"{Date.Value.ToString("ddd, MMM dd, yyyy")} <br/> {Time}";
                }
                else
                {
                    display = "n/a";
                }

                return display;
            }
        }

        public string DateDisplay
        {
            get
            {
                var display = string.Empty;

                if (Date.HasValue)
                {
                    display = $"{Date.Value.ToString("ddd, MMM dd, yyyy")}";
                }
                else
                {
                    display = "n/a";
                }

                return display;
            }
        }

        public string StartDateTimeValue
        {
            get { return EndpointDateTimeValue(StartTime); }
        }

        public string EndDateTimeValue
        {
            get { return EndpointDateTimeValue(EndTime); }
        }

        private string EndpointDateTimeValue(string endtime)
        {
            var value = string.Empty;

            if (Date.HasValue)
            {
                value = $"{Date.Value.ToString("MMM dd, yyyy")} {endtime.Trim().Insert(endtime.Length - 3, " ")}";
            }
            else
            {
                value = "n/a";
            }

            return value;
        }

        public int AvailableTickets => Capacity - RegisteredTicketsTotal > 0 ? Capacity - RegisteredTicketsTotal : 0;

        public bool ShowSoldOut => !Selected && RegisteredTicketsTotal >= Capacity;

        public List<SelectListItem> TicketQuantities
        {
            get
            {
                var list = new List<SelectListItem>();
                var ticketsLeft = Capacity - RegisteredTicketsTotal;

                if (ticketsLeft > MaxTicketQuantity)
                {
                    for (var index = 0; index <= MaxTicketQuantity; index++)
                    {
                        list.Add(new SelectListItem { Text = index.ToString(), Value = index.ToString() });
                    }
                }
                else
                {
                    for (var index = 0; index <= ticketsLeft; index++)
                    {
                        list.Add(new SelectListItem { Text = index.ToString(), Value = index.ToString() });
                    }
                }

                return list;
            }
        }

        public decimal ElectiveCredits { get; set; }

        public decimal PrescribedCredits { get; set; }

        public string EventCode { get; set; }
    }
}