using System;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class RegistrationDiscountDto
    {
        public Guid PriceKey { get; set; }

        public string DiscountCode { get; set; }

        public string Name { get; set; }

        public string DiscountDisplay
        {
            get
            {
                var discount = string.Empty;

                if (DiscountCode != string.Empty || DiscountCode != null)
                {
                    discount = $"{DiscountCode} - {Name}";
                }
                else
                {
                    discount = Name;
                }

                return discount;
            }
        }
    }
}