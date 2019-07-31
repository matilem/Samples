using System;
using System.Globalization;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class EditedPaymentInfoDto
    {
        public Guid CustomerKey { get; set; }

        public Guid Key { get; set; }

        public string CardholderName { get; set; }

        public string CreditCardNumber { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        public string VerificationCode { get; set; }

        public string BillingZip { get; set; }

        public decimal TotalDue { get; set; }

        public virtual string ExpirationDateDisplay { get; set; }

    }
}