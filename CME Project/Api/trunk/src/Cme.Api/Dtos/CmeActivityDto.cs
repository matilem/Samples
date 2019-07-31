using System;
using System.Collections.Generic;

namespace Aafp.Cme.Api.Dtos
{
    public class CmeActivityDto
    {
        public Guid ActivityKey { get; set; }

        public int ActivityNumber { get; set; }

        public string ActivityTitle { get; set; }

        public DateTime ActivityStartDate { get; set; }

        public DateTime ActivityEndDate { get; set; }

        public decimal ActivityPrescribedCredits { get; set; }

        public decimal ActivityElectiveCredits { get; set; }

        public List<CmeActivitySessionDto> Sessions { get; set; }

        public IndividualDto Customer { get; set; }
    }
}