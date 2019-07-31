using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class SessionViewModel
    {
        public SessionViewModel() 
        {
            SessionCapacity = new SessionCapacityViewModel
            {
                SessionKey = Key,
                SessionCode = Code,
                SessionTitle = Title,
                SessionCapacity = Capacity,
                SessionRegistrantCount = RegisteredTicketsTotal,
                SessionTicketed = Ticketed
            };
        }

        public Guid Key { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public DateTime? Date { get; set; }

        public string Time { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string LearningObjectives { get; set; }

        public int Capacity { get; set; }

        public bool Ticketed { get; set; }

        public int SelectedQuantity { get; set; }

        public bool Selected { get; set; }

        public int RegisteredTicketsTotal { get; set; }

        public string SessionTypeCode { get; set; }

        public List<SessionConflictViewModel> Conflicts { get; set; }

        public int MaxTicketQuantity { get; set; }

        public SessionFeeViewModel Fee { get; set; }

        public SessionCapacityViewModel SessionCapacity { get; set; }

        public List<GuestBadgeViewModel> GuestBadges { get; set; } 

        public string DateDisplay
        {
            get
            {
                var display = string.Empty;

                if (Date.HasValue)
                {
                    display = $"{Date.Value.ToString("ddd, MMM dd, yyyy")} </br> {Time}";
                }
                else
                {
                    display = "n/a";
                }

                return display;
            }
        }

        public string FullInfoDisplay => $"{Code}  {Title}  {DateDisplay}";

        public string SessionConflictKeys
        {
            get
            {
                var conflictKeys = string.Empty;

                if (Conflicts != null)
                {
                    foreach (var conflict in Conflicts)
                    {
                        conflictKeys += $"{conflict.ConflictSessionKey},";
                    }

                    if (!string.IsNullOrWhiteSpace(conflictKeys))
                        conflictKeys = conflictKeys.Remove(conflictKeys.Length - 1, 1);
                }

                return conflictKeys;
            }
        }

        public List<SelectListItem> TicketQuantities
        {
            get
            {
                var list = new List<SelectListItem>();
                var ticketsLeft = Capacity - RegisteredTicketsTotal;

                if (ticketsLeft > MaxTicketQuantity)
                {
                    for (var index = 1; index <= MaxTicketQuantity; index++)
                    {
                        list.Add(new SelectListItem { Text = index.ToString(), Value = index.ToString() });
                    }
                }
                else
                {
                    for (var index = 1; index <= ticketsLeft; index++)
                    {
                        list.Add(new SelectListItem { Text = index.ToString(), Value = index.ToString() });
                    }
                }

                return list;
            }
        } 
    }
}