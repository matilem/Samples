using System;

namespace Aafp.Events.Api.Dtos
{
    public class EventListItemDto
    {
        public Guid Key { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? RemoveFromWebDate { get; set; }

        public DateTime? PostToWebDate { get; set; }

        public DateTime? CutOffDate { get; set; }

        public string LocationCity { get; set; }

        public string LocationState { get; set; }
    }
}