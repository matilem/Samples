using System;

namespace Aafp.Events.Web.ViewModels
{
    public class SessionFeeViewModel
    {
        public decimal Price { get; set; }

        public Guid PriceKey { get; set; }

        public Guid ProductKey { get; set; }

        public string PriceDisplay => Price > 0 ? Price.ToString("C") : "$0.00";
    }
}