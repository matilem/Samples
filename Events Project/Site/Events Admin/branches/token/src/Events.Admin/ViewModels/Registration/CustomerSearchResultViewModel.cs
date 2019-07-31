using System;
using System.Collections.Generic;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class CustomerSearchResultViewModel
    {
        public Guid CustomerKey { get; set; }

        public string FullName { get; set; }

        public string SortName { get; set; }

        public string CstId { get; set; }

        public string Email { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public List<CustomerEventViewModel> RecentEvents { get; set; }

        public List<CustomerEventViewModel> PastEvents { get; set; }

        public List<CustomerEventViewModel> EventsToAdd { get; set; }

        public string AddressDisplay
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(AddressLine3))
                {
                    return $"{AddressLine1}</br>{AddressLine2}</br>{AddressLine3}</br>{City}, {State}  {Zip}";
                }
                else if (!string.IsNullOrWhiteSpace(AddressLine2))
                {
                    return $"{AddressLine1}</br>{AddressLine2}</br>{City}, {State}  {Zip}";
                }
                else if (!string.IsNullOrWhiteSpace(AddressLine1))
                {
                    return $"{AddressLine1}</br>{City}, {State}  {Zip}";
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}