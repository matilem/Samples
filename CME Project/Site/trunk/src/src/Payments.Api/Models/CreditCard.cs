using System.Text.RegularExpressions;
using Avectra.netForum.xWeb.xWebSecure;

namespace Aafp.Payments.Api.Models
{
    public class CreditCard
    {
        #region Fields

        private readonly Crypto webCrypto = new Crypto();

        private string number;

        private const string MasterCardRegEx = "^(51|52|53|54|55|22|23|24|25|26|27)";

        private const string VisaRegEx = "^(4)";

        private const string AmexRegEx = "^(34|37)";

        private const string DiscoverRegEx = "^(6011)";

        #endregion

        #region constructor

        public CreditCard()
        {
            Encryption = true;
        }

        #endregion

        #region Properties

        public string Number
        {
            get
            {
                return Encryption && !string.IsNullOrWhiteSpace(number) ?
                    webCrypto.TripleDesCBCSymmetricEncrypt(number) : number;
            }

            set
            {
                // Remove anything that isn't a number.
                number = Regex.Replace(value, @"\D", string.Empty);
            }
        }

        public string VerificationCode { get; set; }

        public string CardholderName { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        public string ExpirationDate => $"{ExpirationYear}/{ExpirationMonth:00}";

        public bool Encryption { get; set; }

        #endregion

        #region Public Methods

        public string GetCardType()
        {
            var type = string.Empty;
            var decryptedNumber = GetDecryptedCardNumber();

            if (Regex.IsMatch(decryptedNumber, MasterCardRegEx))
                type = "MasterCard";
            else if (Regex.IsMatch(decryptedNumber, VisaRegEx))
                type = "VISA";
            else if (Regex.IsMatch(decryptedNumber, AmexRegEx))
                type = "American Express";
            else if (Regex.IsMatch(decryptedNumber, DiscoverRegEx))
                type = "Discover";

            return type;
        }

        public string GetDecryptedCardNumber()
        {
            return string.IsNullOrWhiteSpace(Number) ? string.Empty : webCrypto.TripleDesCBCSymmetricDecrypt(Number);
        }

        public bool IsIncomplete()
        {
            int parsed;
            return string.IsNullOrWhiteSpace(CardholderName) || string.IsNullOrWhiteSpace(number)
                   || string.IsNullOrWhiteSpace(VerificationCode) || !int.TryParse(VerificationCode, out parsed) || ExpirationDate.Length != 7;
        }

        #endregion
    }
}