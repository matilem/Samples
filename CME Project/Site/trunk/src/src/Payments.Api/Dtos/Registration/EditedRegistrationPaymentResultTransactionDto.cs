using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Avectra.netForum.Extension.OE;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class EditedRegistrationPaymentResultTransactionDto
    {
        public string message { get; set; }

        public string transactionAmount { get; set; }

        public string creditAmount { get; set; }

        public string creditUsed { get; set; }

        public string paymentApplied { get; set; }

        public string remainingBalance { get; set; }

        public EditedRegistrationPaymentResultTransactionWarningDto warnings { get; set; }

    }
}