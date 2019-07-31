using System;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class SessionFeeViewModel
    {
        public decimal Price { get; set; }

        public Guid SessionKey { get; set; }

        public Guid PriceKey { get; set; }

        public Guid ProductKey { get; set; }

        public string PriceDisplay
        {
            get { return Price > 0 ? Price.ToString("F") : string.Empty; }
        }
    }
}