using System;
using System.Collections.Generic;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class RegistrationEventDto
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

        public List<RegistrationFeeDto> Fees { get; set; } 

        public List<RegistrationStepDto> Steps { get; set; }
    }
}