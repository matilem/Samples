using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationBadgeViewModel
    {
        public string NickName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
       
        public string State { get; set; }
       
        public string City { get; set; }

        public string Country { get; set; }

        public string Company { get; set; }

        public string Position { get; set; }

        public List<StateViewModel> States { get; set; }

        public bool ShowState => States != null && States.Any();
    }
}