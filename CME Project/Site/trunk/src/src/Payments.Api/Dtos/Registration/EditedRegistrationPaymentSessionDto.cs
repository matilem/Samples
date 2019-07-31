using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class EditedRegistrationPaymentSessionDto
    {
        public Guid? Key { get; set; }

        public Guid SessionKey { get; set; }

        public int Quantity { get; set; }

        public RegistrationSessionFeeDto Fee { get; set; }

        public bool Removed { get; set; }

        public bool Selected { get; set; }
    }
}