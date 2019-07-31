using System.Collections.Generic;
using Aafp.Cme.Api.Dtos;

namespace Aafp.Cme.Api.Helpers
{
    public class ReElectionTotalsHelper
    {
        public ReElectionTotalsHelper(IEnumerable<CreditTypeDto> creditTypes, string stateCode)
        {
            ChapterStateCode = stateCode;
            CreditTypeReElectionTotalsList = new List<CreditTypeReElectionTotalsHelper>();

            foreach (var creditType in creditTypes)
            {
                AddCreditTypeReElectionTotal(new CreditTypeReElectionTotalsHelper { MaximumCreditsPerCycle = creditType.MaximumCreditsPerCycle, CreditTypeKey = creditType.Key, CreditTypeTitle = creditType.Title, CreditTypeDesignation = creditType.Designation});
            }
        }

        public string ChapterStateCode { get; set; }

        public decimal PrescribedCredits { get; set; }

        public decimal PrescribedCreditsRequired => ApplicationConfig.PrescribedCreditsRequired;

        public decimal PrescribedCreditsNeeded
        {
            get
            {
                var prescribedCreditsNeeded = PrescribedCreditsRequired - PrescribedCreditsApplied;

                return prescribedCreditsNeeded < 0 ? 0 : prescribedCreditsNeeded;
            }
        }

        public decimal PrescribedCreditsApplied => PrescribedCredits - GetCreditsOverLimit("P");

        public decimal ElectiveCredits { get; set; }

        public decimal ElectiveCreditsApplied => ElectiveCredits - GetCreditsOverLimit("E");

        public decimal GroupCredits { get; set; }

        public decimal GroupCreditsRequired => ApplicationConfig.GroupCreditsRequired;

        public decimal GroupCreditsNeeded
        {
            get
            {
                var groupCreditsNeeded = GroupCreditsRequired - GroupCredits;

                return groupCreditsNeeded < 0 ? 0 : groupCreditsNeeded;
            }
        }

        public decimal ChapterCredits { get; set; }

        public decimal ChapterCreditsRequired
        {
            get
            {
                if (ChapterStateCode == "FL")
                {
                    return ApplicationConfig.FloridaChapterCreditsRequired;
                }

                if (ChapterStateCode == "MD")
                {
                    return ApplicationConfig.MarylandChapterCreditsRequired;
                }

                return 0;
            }
        }

        public decimal ChapterCreditsNeeded
        {
            get
            {
                var chapterCreditsNeeded = ChapterCreditsRequired - ChapterCredits;

                return chapterCreditsNeeded < 0 ? 0 : chapterCreditsNeeded;
            }
        }

        public decimal TotalCreditsReported => PrescribedCredits + ElectiveCredits;

        public decimal TotalCreditsRequired => ApplicationConfig.TotalCreditsRequired;

        public decimal TotalCreditsApplied => PrescribedCreditsApplied + ElectiveCreditsApplied;

        public decimal TotalCreditsNeeded
        {
            get
            {
                decimal totalNeeded = TotalCreditsRequired - TotalCreditsApplied;
                return totalNeeded < 0 ? 0 : totalNeeded;
            }
        }

        public bool RequirementsFulfilledAll => PrescribedCreditsNeeded == 0 && TotalCreditsNeeded == 0 && GroupCreditsNeeded == 0 && ChapterCreditsNeeded == 0;

        public bool RequirementsFulfilledReElection
        {
            get
            {
                if (ChapterStateCode == "MD")
                {
                    return PrescribedCreditsNeeded == 0 && TotalCreditsNeeded == 0 && GroupCreditsNeeded == 0 && ChapterCreditsNeeded == 0;
                }

                return PrescribedCreditsNeeded == 0 && TotalCreditsNeeded == 0 && GroupCreditsNeeded == 0;
            }
        }

        public List<CreditTypeReElectionTotalsHelper> CreditTypeReElectionTotalsList { get; set; }

        public void AddCreditTypeReElectionTotal(CreditTypeReElectionTotalsHelper totals)
        {
            CreditTypeReElectionTotalsList.Add(totals);
        }

        private decimal GetCreditsOverLimit(string creditDesignation)
        {
            decimal creditsOverLimit = 0;
            foreach (var creditTypeTotal in CreditTypeReElectionTotalsList)
            {
                if (creditTypeTotal.CreditTypeDesignation == creditDesignation)
                    creditsOverLimit += creditTypeTotal.GetCreditsOverLimit();
            }

            return creditsOverLimit;
        }
    }
}