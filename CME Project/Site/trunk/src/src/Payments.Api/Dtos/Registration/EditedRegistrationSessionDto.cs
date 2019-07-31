using System;
using System.Collections.Generic;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class EditedRegistrationSessionDto
    {
        public Guid? Key { get; set; }

        public Guid SessionKey { get; set; }

        public int SelectedQuantity { get; set; }

        public Guid PriceKey { get; set; }
    }
}