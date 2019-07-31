using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Events.Admin.Common.ViewModels
{
    public class AddressViewModel
    {
        public Guid Key { get; set; }

        public Guid AddressKey { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string InternationalProvince { get; set; }

        public string CongressDistrictNumber { get; set; }

        public string StateHouse { get; set; }

        public string StateSenate { get; set; }

        public string MetroStatArea { get; set; }

        public string CarrierRoute { get; set; }

        public string DeliveryPoint { get; set; }

        public string TimeZone { get; set; }

        public string Urbanization { get; set; }

        public string BarCode { get; set; }

        public string Fips { get; set; }

        public string MailingLabelHtml { get; set; }

        public double? AverageLongitude { get; set; }

        public double? AverageLatitude { get; set; }

        public bool NoValidationRequired { get; set; }

        public bool IsBilling { get; set; }

        public bool IsPrimary { get; set; }

        public string AddressType { get; set; }
    }
}