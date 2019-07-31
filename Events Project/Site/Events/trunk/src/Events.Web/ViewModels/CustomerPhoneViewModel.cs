using System;

namespace Aafp.Events.Web.ViewModels
{
    public class CustomerPhoneViewModel
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