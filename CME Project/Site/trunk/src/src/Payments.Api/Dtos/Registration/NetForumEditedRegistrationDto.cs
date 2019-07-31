using System.Collections.Generic;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class NetForumEditedRegistrationDto
    {
        public string reg_key { get; set; }

        public string bat_key { get; set; }

        public string ait_key { get; set; }

        public List<NetForumEditedRegistrationSessionDto> Sessions { get; set; }

        public NetForumEditedRegistrationPaymentDto Payment { get; set; }

    }
}