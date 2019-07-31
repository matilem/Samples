using System;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class EventFeeViewModel
    {
        public decimal Price { get; set; }

        public Guid PriceKey { get; set; }

        public string ProductName { get; set; }

        public string FeeText => $"{ProductName}: {Price.ToString("C")}";
    }
}