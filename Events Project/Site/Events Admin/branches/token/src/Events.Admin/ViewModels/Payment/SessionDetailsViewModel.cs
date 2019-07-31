using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Events.Admin.ViewModels.Payment
{
    public class SessionDetailsViewModel
    {
        public Guid SessionKey { get; set; }

        public string SessionCode { get; set; }

        public string SessionTitle { get; set; }

        public DateTime? SessionDate { get; set; }

        public string SessionTime { get; set; }
    }
}