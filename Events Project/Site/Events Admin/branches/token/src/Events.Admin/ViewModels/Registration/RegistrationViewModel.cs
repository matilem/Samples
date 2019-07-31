using System;
using System.Linq;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class RegistrationViewModel : ViewModelBase
    {
        public UserSearchViewModel UserSearch { get; set; }

        public Guid Key { get; set; }

        public string CurrentUser { get; set; }

        public Guid? CustomerAddressKey { get; set; }

        public Guid? CustomerPhoneKey { get; set; }

        public string EmergencyContactName { get; set; }
        
        public string EmergencyContactPhone { get; set; }

        public DateTime RegistrationDate { get; set; }
        
        public Guid? PriceKey { get; set; }

        public BadgeViewModel Badge { get; set; }

        public CustomerViewModel Customer { get; set; }

        public EventDetailViewModel Event { get; set; }

        public bool IsRegistered { get; set; }

        public bool IsSoldOut { get; set; }

        public bool AllowWaitList { get; set; }

        public bool UserIsOnWaitList { get; set; }

        public string RegistrationDateDisplay => RegistrationDate.ToString("MM/dd/yyyy");

        public decimal SelectedRegistrationFeePrice
        {
            get
            {
                var price = 0m;

                if (PriceKey.HasValue && Event.Fees != null && Event.Fees.Count > 0)
                {
                    var selectedFee = Event.Fees.FirstOrDefault(x => x.PriceKey == PriceKey.Value);
                    price = selectedFee?.Price ?? price;
                }

                return price;
            }
        }

        public string SelectedRegistrationFeeDisplay
        {
            get
            {
                var display = string.Empty;

                if (PriceKey.HasValue && Event?.Fees != null && Event.Fees.Count > 0)
                {
                    var selectedFee = Event.Fees.FirstOrDefault(x => x.PriceKey == PriceKey.Value);
                    display = selectedFee != null ? $"{selectedFee?.FeeText}" : display;
                }

                return display;
            }
        }
    }
}