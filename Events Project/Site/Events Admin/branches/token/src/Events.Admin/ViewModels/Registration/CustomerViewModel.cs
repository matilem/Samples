using System;
using System.Collections.Generic;
using Aafp.Events.Admin.Common.ViewModels;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class CustomerViewModel
    {
        public Guid Key { get; set; }

        public string CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public CustomerMembershipViewModel Membership { get; set; }

        public List<AddressViewModel> Addresses { get; set; }

        public List<PhoneViewModel> Phones { get; set; }
    }
}