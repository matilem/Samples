using System;

namespace Aafp.Events.Api.Dtos.Customer
{
    public class CustomerMembershipDto
    {
        public DateTime? ExpireDate { get; set; }

        public DateTime? TerminateDate { get; set; }

        public string MemberStatusCode { get; set; }

        public string MemberTypeCode { get; set; }

        public bool MemberTypeIsChapter { get; set; }
    }
}