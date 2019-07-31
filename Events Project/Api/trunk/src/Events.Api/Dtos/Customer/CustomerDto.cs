using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.Customer
{
    public class CustomerDto
    {
        public Guid Key { get; set; }

        public string CustomerId { get; set; }

        public string WebLogin { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string OrganizationName { get; set; }

        public string Title { get; set; }

        public bool IsMember { get; set; }

        public bool IsAafpStaff { get; set; }

        public string FaafpFellowshipYear { get; set; }

        public string FullNameMinusPrefix { get; set; }

        public CustomerMembershipDto Membership { get; set; }

        public List<CustomerAddressDto> Addresses { get; set; }

        public List<CustomerPhoneDto> Phones { get; set; }
    }
}