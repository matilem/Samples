﻿using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos.Session;

namespace Aafp.Events.Api.Dtos
{
    public class EventDetailDto
    {
        public Guid Key { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public string LocationCity { get; set; }

        public string LocationState { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CutOffDate { get; set; }

        public bool DisplayBadgeCompany { get; set; }

        public string AlternativeCompanyBadgeLabel { get; set; }

        public bool DisplayBadgePosition { get; set; }

        public string AlternativePositionBadgeLabel { get; set; }

        public List<EventFeeDto> Fees { get; set; }

        public List<StepDto> Steps { get; set; }
    }
}