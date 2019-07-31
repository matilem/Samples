using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class EditedRegistrationGuestBadgeDto
    {
        public Guid Key { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string SessionCode { get; set; }

        public string SessionTitle { get; set; }
    }
}