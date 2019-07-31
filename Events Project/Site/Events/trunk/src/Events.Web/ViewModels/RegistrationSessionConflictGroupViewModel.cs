using System.Collections.Generic;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationSessionConflictGroupViewModel
    {
        public bool DoNotAllow { get; set; }

        public List<RegistrationSessionViewModel> ConflictedSessions { get; set; }
    }
}