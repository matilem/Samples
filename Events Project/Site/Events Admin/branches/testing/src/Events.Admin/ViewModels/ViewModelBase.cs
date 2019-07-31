using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Events.Admin.ViewModels
{
    public class ViewModelBase
    {
        public bool HasError { get; set; }

        public string ErrorMessage { get; set; }
    }
}