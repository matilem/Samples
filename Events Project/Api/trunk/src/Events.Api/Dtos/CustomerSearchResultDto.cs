using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos
{
    public class CustomerSearchResultDto
    {
        public CustomerSearchResultDto()
        {
            Events = new List<CustomerEventDto>();
        }

        public Guid CustomerKey { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

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

        public List<CustomerEventDto> Events { get; set; } 
    }
}