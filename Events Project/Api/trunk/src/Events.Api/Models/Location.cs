using System;

namespace Aafp.Events.Api.Models
{
    public class Location
    {
        public virtual Guid CustomerKey { get; set; }

        public virtual Guid Key { get; set; }

        public virtual Guid AddressKey { get; set; }

        public virtual string Address1 { get; set; }

        public virtual string Address2 { get; set; }

        public virtual string Address3 { get; set; }

        public virtual string City { get; set; }

        public virtual string County { get; set; }

        public virtual string State { get; set; }

        public virtual string PostalCode { get; set; }

        public virtual string Country { get; set; }

        public virtual string InternationalProvince { get; set; }

        public virtual string MetroStatArea { get; set; }

        public virtual string TimeZone { get; set; }

        public virtual string MailingLabelHtml { get; set; }

        public virtual double? AverageLongitude { get; set; }

        public virtual double? AverageLatitude { get; set; }

        public virtual bool IsBilling { get; set; }

        public virtual bool IsPrimary { get; set; }

        public virtual string AddressType { get; set; }

        public virtual string ToHtmlFormattedString()
        {
            if (string.IsNullOrEmpty(Address2))
                return Address1 + Address2 + "<br>" + City + ", " + State + " " + PostalCode + "<br>" + Country;

            return Address1 + "<br>" + Address2 + "<br>" + City + ", " + State + " " + PostalCode + "<br>" + Country;
        }
    }
}