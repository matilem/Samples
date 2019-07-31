using System;
using System.Collections.Generic;
using Aafp.Payments.Api.Models;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class EditedRegistrationDto
    {
        public Guid RegistrationKey { get; set; }

        public Guid? BatchKey { get; set; }

        public Guid? InvoiceTermsKey { get; set; }

        public List<EditedRegistrationSessionDto> Sessions { get; set; }

        public List<EditedRegistrationGuestBadgeDto> EditedGuestBadges { get; set; }

        public EditedPaymentInfoDto Payment { get; set; }

        public string CurrentUser { get; set; }
    }
}