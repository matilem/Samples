using System.Collections.Generic;

namespace Aafp.Cme.Api.Dtos
{
    public class ReElectionTotalsDto
    {
        public string ChapterStateCode { get; set; }

        public int ReElectionEndYear { get; set; }

        public int ReElectionStartYear { get; set; }

        public decimal PrescribedCredits { get; set; }

        public decimal PrescribedCreditsRequired { get; set; }

        public decimal PrescribedCreditsNeeded { get; set; }

        public decimal PrescribedCreditsApplied { get; set; }

        public decimal ElectiveCredits { get; set; }

        public decimal ElectiveCreditsApplied { get; set; }

        public decimal GroupCredits { get; set; }

        public decimal GroupCreditsRequired { get; set; }

        public decimal GroupCreditsNeeded { get; set; }

        public decimal ChapterCredits { get; set; }

        public decimal ChapterCreditsRequired { get; set; }

        public decimal ChapterCreditsNeeded { get; set; }

        public decimal TotalCreditsReported { get; set; }

        public decimal TotalCreditsRequired { get; set; }

        public decimal TotalCreditsApplied { get; set; }

        public decimal TotalCreditsNeeded { get; set; }

        public bool RequirementsFulfilledAll { get; set; }

        public bool RequirementsFulfilledReElection { get; set; }

        public List<CreditTypeReElectionTotalsDto> CreditTypeReElectionTotalsList { get; set; }
    }
}