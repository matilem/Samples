using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Events.Api.Dtos
{
    public class EventBaseDto
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

        public string LocationCountry { get; set; }

        public bool DisplayBadgeCompany { get; set; }

        public string AlternativeCompanyBadgeLabel { get; set; }

        public bool DisplayBadgePosition { get; set; }

        public string AlternativePositionBadgeLabel { get; set; }

        public bool AllowWaitList { get; set; }
    }
}