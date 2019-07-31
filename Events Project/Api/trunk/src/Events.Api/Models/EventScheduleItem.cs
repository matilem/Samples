using System;

namespace Aafp.Events.Api.Models
{
    public class EventScheduleItem
    {
        public virtual Guid Key { get; set; }

        public virtual string Code { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual decimal TotalCme { get; set; }

        public virtual decimal Fee { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual string StartTime { get; set; }

        public virtual string EndTime { get; set; }

        public virtual string Room { get; set; }

        public virtual string TrackCode { get; set; }

        public virtual string Faculty { get; set; }

        public virtual string Speakers { get; set; }

        public virtual string Topics { get; set; }

        public virtual string Track { get; set; }
    }
}