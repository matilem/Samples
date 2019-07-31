using System;
using System.Collections.Generic;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dtos.Session
{
    public class SessionDto
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

        public bool Ticketed { get; set; }

        public IList<SessionFaculty> SessionFaculties { get; set; }

        public IList<SessionLocation> Locations { get; set; }

        public int RegisteredTicketsTotal { get; set; }

        public string SessionTypeCode { get; set; }

        public IList<SessionConflictDto> Conflicts { get; set; }

        public int MaxTicketQuantity { get; set; }

        public bool? PrintTicket { get; set; }

        public SessionFeeDto Fee { get; set; }

        public bool Selected { get; set; }

        public Guid RequiredSession { get; set; }
    }
}