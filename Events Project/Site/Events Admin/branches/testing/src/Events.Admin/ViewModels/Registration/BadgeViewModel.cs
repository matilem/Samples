using System.Collections.Generic;
using Aafp.Events.Admin.Common.ViewModels;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class BadgeViewModel
    {
        public string NickName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Notes { get; set; }

        public string Company { get; set; }

        public string Position { get; set; }
        
        public List<StateViewModel> States { get; set; }
    }
}