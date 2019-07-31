using System;
using System.Collections.Generic;
using Aafp.Payments.Api.Models;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class RegistrationDto
    {
        public Guid Key { get; set; }

        public string RegistrationStatus { get; set; }

        public string CurrentUser { get; set; }

        public Guid? CustomerAddressKey { get; set; }

        public Guid? CustomerPhoneKey { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactPhone { get; set; }

        public DateTime RegistrationDate { get; set; }

        public Guid? PriceKey { get; set; }

        public bool SendConfirmationEmail { get; set; } = true;

        public bool IsSoldOut { get; set; }

        public bool IsAdmin { get; set; }

        public RegistrationFeeDto SelectedFee { get; set; }

        public RegistrationBadgeDto Badge { get; set; }

        public List<RegistrationBadgeDto> GuestBadges { get; set; } 

        public CustomerDto Customer { get; set; }

        public RegistrationEventDto Event { get; set; }

        public List<RegistrationDiscountDto> Discounts { get; set; } 

        public Guid? SelectedDiscountPriceKey { get; set; }

        public PaymentInfo Payment { get; set; }

        public List<RegistrationDto> RelatedRegistrations { get; set; }

        public List<RegistrationSessionDto> UnavailableSessions { get; set; }

        public bool HasError { get; set; }

        public string ErrorMessage { get; set; }
    }
}