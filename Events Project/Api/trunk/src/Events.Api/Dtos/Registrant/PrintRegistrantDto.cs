using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos.Admin.Registration;

namespace Aafp.Events.Api.Dtos.Registrant
{
    public class PrintRegistrantDto
    {
        public Guid EventKey { get; set; }

        public string EventTitle { get; set; }

        public string LocationCity { get; set; }

        public string LocationState { get; set; }

        public string FullName { get; set; }

        public string CstId { get; set; }

        public Guid RegistrantKey { get; set; }

        public Guid InvoiceKey { get; set; }

        public List<AdminRegistrantSessionDto> Sessions { get; set; } 
    }
}