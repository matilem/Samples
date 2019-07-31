using System;
using System.Collections.Generic;

namespace Aafp.MyCme.Web.ViewModels
{
    public class SessionsByDateViewModel
    {
        public DateTime ActivityDate { get; set; }

        public List<CmeActivitySessionViewModel> CmeSessions { get; set; }

        public string ActivityDateDisplay
        {
            get
            {
                string display = string.Empty;

                display = $"{ActivityDate.ToString("ddd, MMM d, yyyy")}";

                return display;
            }
        }
    }
}