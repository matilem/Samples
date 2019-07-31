using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class EditedRegistrationPaymentResultDto
    {
        public string reg_key { get; set; }

        public string bat_key { get; set; }

        public string inv_key { get; set; }
    
        public string inv_code_cp { get; set; }

        public string inv_amount { get; set; }

        public string inv_balance { get; set; }

        public string inv_netpayment { get; set; }

        public string inv_netcredit { get; set; }

        public List<NetForumEditedRegistrationSessionDto> sessions { get; set; }

        public NetForumEditedRegistrationPaymentDto payment { get; set; }

        public EditedRegistrationPaymentResultTransactionDto transactionResults { get; set; }

        public EditedRegistrationPaymentResultTransactionDataDto data { get; set; }




    }
}