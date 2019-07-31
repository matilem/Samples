using System;
using System.Collections.Generic;
using System.Linq;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class EditedRegistrationViewModel : ViewModelBase
    {
        public Guid Key { get; set; }

        public bool PaymentRequired { get; set; }

    }
}