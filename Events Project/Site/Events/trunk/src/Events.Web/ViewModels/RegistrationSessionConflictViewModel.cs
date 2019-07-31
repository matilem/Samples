using System;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationSessionConflictViewModel
    {
        public Guid Key { get; set; }

        public Guid ConflictSessionKey { get; set; }

        public string ConflictSessionCode { get; set; }

        public int Type { get; set; }
    }
}