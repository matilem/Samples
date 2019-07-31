using System;

namespace Aafp.Cme.Api.Dtos
{
    public class CreditAvailableDto
    {
        public string Title { get; set; }

        public Guid ProductKey { get; set; }

        public int ActivityNumber { get; set; }

        public DateTime? TransactionDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime? BeginDate { get; set; }

        public string AccessUrl { get; set; }

        public string ClaimCreditUrl { get; set; }

        public string ProductImage { get; set; }

        public decimal CreditsAvailable { get; set; }

        public decimal CreditsReported { get; set; }

        public Guid AssessmentGroupKey { get; set; }

        public bool IsMember { get; set; }
    }
}