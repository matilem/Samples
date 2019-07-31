using System;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class UserRegistrationDto
    {
        public Guid Key { get; set; }

        public Guid EventKey { get; set; }

        public string EventTitle { get; set; }

        public string EventCode { get; set; }

        public string EventCity { get; set; }

        public string EventState { get; set; }

        public DateTime? EventStartDate { get; set; }

        public DateTime? EventEndDate { get; set; }

        public DateTime? PostToWebDate { get; set; }

        public DateTime? RemoveFromWebDate { get; set; }

        public string EventDescriptionHtml { get; set; }

        public int Capacity { get; set; }
        
        public bool IsRegistered { get; set; }

        public bool IsSoldOut { get; set; }

        public bool AllowWaitList { get; set; }

        public bool UserIsOnWaitList { get; set; }

        public string Type { get; set; }
    }
}