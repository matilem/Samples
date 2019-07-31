using System;
using System.Collections.Generic;

namespace Aafp.Also.Api.Dtos
{
    public class AlsoCreditDto
    {
        public Guid SessionKey { get; set; }

        public string WebLogin { get; set; }

        public List<Guid> CustomerKeys { get; set; }
    }
}