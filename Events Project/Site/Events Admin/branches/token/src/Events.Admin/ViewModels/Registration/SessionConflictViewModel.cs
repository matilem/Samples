using System;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class SessionConflictViewModel
    {
        public Guid Key { get; set; }

        public Guid ConflictSessionKey { get; set; }

        public string ConflictSessionCode { get; set; }

        public int Type { get; set; }
    }
}