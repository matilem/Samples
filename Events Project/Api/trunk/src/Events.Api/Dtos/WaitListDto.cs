using System;

namespace Aafp.Events.Api.Dtos
{
    public class WaitListDto
    {
        public Guid EventKey { get; set; }

        public Guid CustomerKey { get; set; }
    }
}