using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Events.Admin.ViewModels
{
    public class FaxViewModel
    {
        public Guid Key { get; set; }

        public Guid FaxKey { get; set; }

        public string Number { get; set; }

        public string Extension { get; set; }

        public bool IsPrimary { get; set; }

        public string FaxType { get; set; }

        public Guid? CountryKey { get; set; }
    }
}