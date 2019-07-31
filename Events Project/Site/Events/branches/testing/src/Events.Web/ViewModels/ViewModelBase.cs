using System;

namespace Aafp.Events.Web.ViewModels
{
    public class ViewModelBase
    {
        public bool HasError { get; set; }

        public string ErrorMessage { get; set; }

        public RegistrationNavigationViewModel Navigation { get; set; }

        public Guid? RegistrationKey { get; set; }

        public decimal CurrentTotal { get; set; }
    }
}