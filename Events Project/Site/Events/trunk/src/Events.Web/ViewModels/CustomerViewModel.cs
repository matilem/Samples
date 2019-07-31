using System;
using System.Collections.Generic;

namespace Aafp.Events.Web.ViewModels
{
    public class CustomerViewModel
    {
        public Guid Key { get; set; }

        public string CustomerId { get; set; }

        public string WebLogin { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool IsMember { get; set; }

        public string FaafpFellowshipYear { get; set; }

        public string FullNameMinusPrefix { get; set; }

        public List<CustomerAddressViewModel> Addresses { get; set; }

        public List<CustomerPhoneViewModel> Phones { get; set; }
    }
}