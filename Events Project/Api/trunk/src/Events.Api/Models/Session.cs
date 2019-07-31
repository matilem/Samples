using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Models
{
    public class Session
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool DeleteFlag { get; set; }

        public virtual string Code { get; set; }

        public virtual string Title { get; set; }

        public virtual DateTime? Date { get; set; }

        public virtual string Time
        {
            get
            {
                if (StartTime == null || EndTime == null)
                    return string.Empty;
                return StartTime + " - " + EndTime;
            }
        }

        public virtual string StartTime { get; set; }

        public virtual string EndTime { get; set; }

        public virtual string LearningObjectives { get; set; }

        public virtual int Capacity { get; set; }

        public virtual bool Ticketed { get; set; }

        public virtual int RegisteredTicketsTotal { get; set; }

        public virtual IList<SessionFaculty> SessionFaculties { get; set; }

        public virtual Event Event { get; set; }

        public virtual IList<SessionLocation> Locations { get; set; }

        public virtual IList<Registrant> Registrants { get; set; }

        public virtual string SessionTypeCode { get; set; }

        public virtual IList<SessionConflict> Conflicts { get; set; }

        public virtual IList<SessionIcon> Icon { get; set; }

        public virtual int MaxTicketQuantity { get; set; }

        public virtual bool? PrintTicket { get; set; }

        public virtual Guid HeadingKey { get; set; }

        public virtual int Sequence { get; set; }

        public virtual Guid RequiredSession { get; set; }

        public virtual decimal ElectiveCredits { get; set; }

        public virtual decimal PrescribedCredits { get; set; }
    }
}