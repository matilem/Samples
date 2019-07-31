using System;

namespace Aafp.Events.Web.ViewModels
{
    public class EventFeeViewModel
    {
        public decimal Price { get; set; }

        public Guid PriceKey { get; set; }

        public string ProductName { get; set; }

        public Guid ProductKey { get; set; }

        public string FeeText => $"{ProductName}: {Price.ToString("C")}";
    }
}