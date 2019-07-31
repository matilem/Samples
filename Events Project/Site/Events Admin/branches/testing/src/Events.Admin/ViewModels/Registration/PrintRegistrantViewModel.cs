using System;
using System.Collections.Generic;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class PrintRegistrantViewModel : ViewModelBase
    {
        public string FullName { get; set; }

        public string CstId { get; set; }

        public Guid EventKey { get; set; }

        public string EventTitle { get; set; }

        public string LocationCity { get; set; }

        public string LocationState { get; set; }

        public Guid RegistrantKey { get; set; }

        public Guid InvoiceKey { get; set; }

        public List<RegistrantSessionViewModel> Sessions { get; set; }

        public string LocationDisplay => $"{LocationCity}, {LocationState}";
    }
}