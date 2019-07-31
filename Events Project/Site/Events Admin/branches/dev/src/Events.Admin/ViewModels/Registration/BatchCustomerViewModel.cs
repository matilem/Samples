using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class BatchCustomerViewModel : ViewModelBase
    {
        public Guid CustomerKey { get; set; }

        public string MemberId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RegistrationStatus { get; set; }
    }
}