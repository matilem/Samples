using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class UserRegistrationSessionDto
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

        public IList<UserRegistrationSessionConflictDto> Conflicts { get; set; }

        public int MaxTicketQuantity { get; set; }

        public bool? PrintTicket { get; set; }

        public SessionFeeDto Fee { get; set; }

        public int SelectedQuantity { get; set; }

        public bool Selected { get; set; }

        public bool Removed { get; set; }

        public bool IsRegistered { get; set; }

        public string RegistrationStatus { get; set; }

        public Guid RequiredSession { get; set; }

        public List<RegistrationGuestBadgeDto> GuestBadges { get; set; }

        public decimal ElectiveCredits { get; set; }

        public decimal PrescribedCredits { get; set; }

        public string EventCode { get; set; }
    }
}