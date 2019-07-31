namespace Aafp.Payments.Api.Dtos.Registration
{
    public class NetForumEditedRegistrationPaymentDto
    {
        public string pin_cst_key { get; set; }

        public string pin_apm_key { get; set; }

        public string pin_cc_cardholder_name { get; set; }

        public string pin_cc_number { get; set; }

        public string pin_cc_expire { get; set; }

        public string pin_cc_security_code { get; set; }

        public string pin_zip { get; set; }

        public string pin_check_amount { get; set; }
    }
}