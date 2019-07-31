using System;

namespace Aafp.Also.Api.Dtos
{
    public class DiscountDto
    {
        public Guid PriceKey { get; set; }

        public string ActivityNumber { get; set; }

        public string WebLogin { get; set; }

        public DateTime ActivityStartDate { get; set; }

        public DateTime ActivityEndDate { get; set; }
    }
}