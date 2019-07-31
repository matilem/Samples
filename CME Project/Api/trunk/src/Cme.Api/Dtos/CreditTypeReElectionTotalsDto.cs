using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Cme.Api.Dtos
{
    public class CreditTypeReElectionTotalsDto
    {
        public Guid CreditTypeKey { get; set; }

        public string CreditTypeTitle { get; set; }

        public string CreditTypeDesignation { get; set; }

        public decimal? MaximumCreditsPerCycle { get; set; }

        public decimal CreditsReported { get; set; }

        public decimal CreditsApplied { get; set; }
    }
}