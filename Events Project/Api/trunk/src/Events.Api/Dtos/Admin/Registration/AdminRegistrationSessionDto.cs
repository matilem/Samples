using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.Admin.Registration
{
    public class AdminRegistrationSessionDto
    {
        public Guid Key { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public DateTime? Date { get; set; }

        public string LearningObjectives { get; set; }

        public string Time
        {
            get
            {
                if (StartTime == null || EndTime == null)
                    return string.Empty;
                return StartTime + " - " + EndTime;
            }
        }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int Capacity { get; set; }

        public bool Ticketed { get; set; }
        
        public int RegisteredTicketsTotal { get; set; }

        public string SessionTypeCode { get; set; }

        public IList<AdminRegistrationSessionConflictDto> Conflicts { get; set; }

        public int MaxTicketQuantity { get; set; }

        public bool? PrintTicket { get; set; }

        public SessionFeeDto Fee { get; set; }

        public int SelectedQuantity { get; set; }

        public bool Selected { get; set; }

        public bool Removed { get; set; }

        public List<RegistrationGuestBadgeDto> GuestBadges { get; set; } 
    }
}