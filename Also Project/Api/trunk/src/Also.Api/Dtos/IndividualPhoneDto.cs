using System;

namespace Aafp.Also.Api.Dtos
{
    public class IndividualPhoneDto
    {
        public Guid Key { get; set; }

        public Guid PhoneKey { get; set; }

        public string Number { get; set; }

        public string Extension { get; set; }

        public bool IsPrimary { get; set; }

        public string PhoneType { get; set; }

        public bool IsUnlisted { get; set; }
    }
}