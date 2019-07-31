using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Cme.Api.Dtos
{
    public class CmeActivitySessionDto
    {
        public Guid SessionKey { get; set; }

        public string SessionTitle { get; set; }

        public DateTime SessionBeginDate { get; set; }

        public DateTime SessionEndDate { get; set; }

        public string SessionCity { get; set; }

        public string SessionState { get; set; }

        public decimal SessionPrescribedCredits { get; set; }

        public decimal SessionElectiveCredits { get; set; }

        public bool Reported { get; set; }
    }
}