using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class EditedRegistrationPaymentResultTransactionDataDto
    {
        public string ses_name { get; set; }

        public string inv_code_cp { get; set; }

        public string ivd_qty { get; set; }

        public string ivd_price { get; set; }

        public string ivd_amount_cp { get; set; }

        public string ret_qty { get; set; }

        public string ret_type { get; set; }

        public string rgs_qty { get; set; }

        public string rgs_cancel_qty { get; set; }

        public string rgs_balance_qty_cp { get; set; }

        public string rgs_cancel_date { get; set; }

        public string ivd_add_date { get; set; }

        public string ivd_change_date { get; set; }

        public string rgs_add_date { get; set; }

        public string rgs_change_date { get; set; }

        public string rxi_add_date { get; set; }
        
        public string rxi_change_date { get; set; }

        public string rxi_delete_flag { get; set; }

        public string rxi_key { get; set; }

        public string rgs_key { get; set; }

        public string ivd_key { get; set; }
        
        public string ses_key { get; set; }

        public string prc_key { get; set; }
    }
}