using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos
{
    public class EventRegistrationTypeInfoDto
    {
        public Guid Key { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public DateTime CutOffDate { get; set; }

        public List<EventFeeDto> Fees { get; set; }
    }
}