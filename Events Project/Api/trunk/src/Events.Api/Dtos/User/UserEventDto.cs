using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.User
{
    public class UserEventDto
    {
        public Guid Key { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public string LocationCity { get; set; }

        public string LocationState { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? RemoveFromWebDate { get; set; }

        public string DescriptionHtml { get; set; }

        public bool AllowWaitList { get; set; }

        public int Capacity { get; set; }

        public bool IsSoldOut { get; set; }

        public int RegistrantsOnWaitCount { get; set; }

        public bool DisplayBadgeCompany { get; set; }

        public string AlternativeCompanyBadgeLabel { get; set; }

        public bool DisplayBadgePosition { get; set; }

        public string AlternativePositionBadgeLabel { get; set; }

        public string HousingUrl { get; set; }

        public string HousingDescription { get; set; }

        public string HousingDiscountCode { get; set; }

        public bool IsOpenForRegistration { get; set; }

        public string DenialMessage { get; set; }

        public bool TinyRegFlag { get; set; }

        public string TinyRegMessage { get; set; }

        public List<EventFeeDto> Fees { get; set; }

        public List<EventStepDto> Steps { get; set; }

        public string MarketingMessage { get; set; }
    }
}