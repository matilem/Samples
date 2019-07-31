using System;

namespace Aafp.Events.Api.Dtos.Admin.Registration
{
    public class AdminRegistrantSessionDto
    {
        public Guid Key { get; set; }

        public string SessionCode { get; set; }

        public string SessionTitle { get; set; }

        public DateTime? SessionDate { get; set; }

        public string SessionTime { get; set; }

        public Guid SessionRequiredSession { get; set; }
    }
}