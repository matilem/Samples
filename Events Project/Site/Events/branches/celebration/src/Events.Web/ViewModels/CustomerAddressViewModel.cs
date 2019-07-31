using System;
namespace Aafp.Events.Web.ViewModels
{
    public class CustomerAddressViewModel
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

        public bool IsBilling { get; set; }

        public bool IsPrimary { get; set; }

        public string AddressType { get; set; }
    }
}