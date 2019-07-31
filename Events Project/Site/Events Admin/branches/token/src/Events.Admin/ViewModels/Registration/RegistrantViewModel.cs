using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class RegistrantViewModel : ViewModelBase
    {
        public Guid Key { get; set; }

        public Guid CustomerKey { get; set; }

        public Guid EventKey { get; set; }
    }
}