using System;

namespace Aafp.Events.Api.Dtos
{
    public class EventScheduleItemDto
    {
        public Guid Key { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal TotalCme { get; set; }

        public decimal Fee { get; set; }

        public DateTime StartDate { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Room { get; set; }

        public string TrackCode { get; set; }

        public string Faculty { get; set; }

        public string Speakers { get; set; }

        public string Topics { get; set; }

        public string Track { get; set; }
    }
}