using System;

namespace Aafp.Cme.Api.Helpers
{
    public class CreditTypeReElectionTotalsHelper
    {
        public Guid CreditTypeKey { get; set; }

        public string CreditTypeTitle { get; set; }

        public string CreditTypeDesignation { get; set; }

        public decimal? MaximumCreditsPerCycle { get; set; }

        public decimal CreditsReported { get; set; }

        public decimal CreditsApplied
        {
            get
            {
                if (MaximumCreditsPerCycle == 0.0m)
                    return CreditsReported;

                var creditsApplied = CreditsReported;
                var maxCredits = MaximumCreditsPerCycle ?? 0.0m;

                if (CreditsReported > maxCredits)
                    creditsApplied = maxCredits;

                return creditsApplied;
            }
        }

        public decimal GetCreditsOverLimit()
        {
            var maxCredits = MaximumCreditsPerCycle ?? 0.0m;
            var creditsOver = CreditsReported - maxCredits;

            return creditsOver > 0 ? creditsOver : 0;
        }
    }
}