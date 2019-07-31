using System;

namespace Aafp.Cme.Api.Dtos
{
    public class CreditDto
    {
        public decimal PrescribedCredits { get; set; }

        public decimal ElectiveCredits { get; set; }

        public Guid SessionKey { get; set; }

        public bool HasError { get; set; }

        public string ErrorMessage { get; set; }
    }
}