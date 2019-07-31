using System;
using System.Collections.Generic;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class RegistrationSessionDto
    {
        public Guid Key { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public DateTime? Date { get; set; }

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

        public string LearningObjectives { get; set; }

        public int Capacity { get; set; }

        public int SelectedQuantity { get; set; }

        public bool Ticketed { get; set; }

        public int RegistrantCount { get; set; }

        public string SessionTypeCode { get; set; }

        public int MaxTicketQuantity { get; set; }

        public bool? PrintTicket { get; set; }

        public RegistrationSessionFeeDto Fee { get; set; }

        public bool Selected { get; set; }

        public List<RegistrationGuestBadgeDto> GuestBadges { get; set; } 
    }
}