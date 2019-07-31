using System;

namespace Aafp.Payments.Api.Models
{
    public class PaymentInfo
    {
        public enum PaymentType
        {
            Payment,
            Invoice
        }

        public Guid Key { get; set; }

        public string CardholderName { get; set; }

        public string CreditCardNumber { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        public string VerificationCode { get; set; }

        public CreditCard ToCreditCard()
        {
            var card = new CreditCard
            {
                CardholderName = CardholderName,
                Number = CreditCardNumber,
                ExpirationYear = ExpirationYear,
                ExpirationMonth = ExpirationMonth
            };

            // Verification Code.
            int code;
            var parsed = int.TryParse(VerificationCode, out code);
            card.VerificationCode = parsed ? VerificationCode : string.Empty;
            return card;
        }

        public PaymentType GetPaymentType()
        {
            var type = string.IsNullOrWhiteSpace(CreditCardNumber) ? PaymentType.Invoice : PaymentType.Payment;

            return type;
        }
    }
}