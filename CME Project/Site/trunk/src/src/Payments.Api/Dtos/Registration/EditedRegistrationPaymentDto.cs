using System;
using System.Collections.Generic;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class EditedRegistrationPaymentDto
    {
        public Guid Key { get; set; }

        public Guid? InvoiceTermsKey { get; set; }

        public List<EditedRegistrationPaymentSessionDto> EditedSessions { get; set; }

        public List<EditedRegistrationGuestBadgeDto> EditedGuestBadges { get; set; }

        public string Zip { get; set; }

        public decimal CurrentTotal { get; set; }

        public string WebLogin { get; set; }
    }
}