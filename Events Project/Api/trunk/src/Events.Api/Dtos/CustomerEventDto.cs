using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos
{
    public class CustomerEventDto
    {
        public CustomerEventDto()
        {
            RelatedRegistrationKeys = new List<Guid>();
        }

        public Guid EventKey { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public DateTime StartDate { get; set; }

        public Guid RegistrationKey { get; set; }

        public Guid CustomerKey { get; set; }

        public bool IsPending { get; set; }

        public List<Guid> RelatedRegistrationKeys { get; set; }
    }
}