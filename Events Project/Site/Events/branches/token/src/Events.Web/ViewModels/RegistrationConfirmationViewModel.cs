using System;
using System.Collections.Generic;
using System.Linq;
using Aafp.Events.Web.ApplicationConfig;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationConfirmationViewModel : ViewModelBase
    {
        public Guid Key { get; set; }

        public Guid InvoiceKey { get; set; }

        public string InvoiceCode { get; set; }

        public Guid? SelectedAddressKey { get; set; }

        public Guid? SelectedPhoneKey { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactPhone { get; set; }

        public string Comment { get; set; }

        public string DiscountName { get; set; }

        public decimal DiscountAmount { get; set; }

        public EventFeeViewModel Fee { get; set; }

        public EventViewModel Event { get; set; }

        public CustomerViewModel Customer { get; set; }

        public RegistrationBadgeViewModel Badge { get; set; }

        public List<RegistrationSessionViewModel> Sessions { get; set; }

        public List<RegistrationConfirmationViewModel> RelatedRegistrations { get; set; }

        public string SelectedAddressDisplay
        {
            get
            {
                CustomerAddressViewModel address = null;

                if (SelectedAddressKey.HasValue)
                    address = Customer.Addresses.FirstOrDefault(x => x.Key == SelectedAddressKey.Value);

                return address != null ? $"{address.Address1}<br />{address.City}, {address.State} {address.PostalCode} <br />{address.Country}" : string.Empty;
            }
        }

        public string SelectedPhoneDisplay
        {
            get
            {
                CustomerPhoneViewModel phone = null;

                if (SelectedPhoneKey.HasValue)
                    phone = Customer.Phones.FirstOrDefault(x => x.Key == SelectedPhoneKey.Value);

                return phone != null ? $"{phone.Number}  ({phone.PhoneType})" : string.Empty;
            }
        }

        public decimal SubTotal
        {
            get
            {
                var subtotal = 0m;
                subtotal += Fee.Price;

                subtotal += Sessions.Sum(session => (session.SelectedQuantity * session.Fee.Price));

                return subtotal;
            }
        }

        public decimal Total => SubTotal - DiscountAmount;

        public string InvoiceUrl => $"{ApplicationConfigManager.Settings.ApplicationUrl}registration/invoice/{InvoiceKey}/pdf";
    }
}