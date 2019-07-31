using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Cme.Api.Dtos
{
    public class AlsoCreditDto
    {
        public Guid SessionKey { get; set; }

        public string WebLogin { get; set; }

        public List<Guid> CustomerKeys { get; set; }
    }
}