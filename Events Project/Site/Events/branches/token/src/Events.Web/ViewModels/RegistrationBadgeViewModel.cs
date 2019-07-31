using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebGrease.Css.Ast.Selectors;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationBadgeViewModel
    {
        [Required(ErrorMessage = "Nickname is required.")]
        public string NickName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        public string Country { get; set; }

        public string Company { get; set; }

        public string Position { get; set; }

        public List<StateViewModel> States { get; set; }

        public bool ShowState => States != null && States.Any();
    }
}