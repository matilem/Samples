using System;
using System.Collections.Generic;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class CustomerEventViewModel
    {
        public CustomerEventViewModel()
        {
            RelatedRegistrations = new List<CustomerEventViewModel>();
        }

        public Guid EventKey { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public string LocationCity { get; set; }

        public string LocationState { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CutOffDate { get; set; }

        public Guid RegistrationKey { get; set; }

        public Guid CustomerKey { get; set; }

        public bool IsPending { get; set; }

        public List<Guid> RelatedRegistrationKeys { get; set; }

        public List<CustomerEventViewModel> RelatedRegistrations { get; set; }

        public string TitleDisplay => $"{Title} - {Code}";

        public string LocationDisplay => $"{LocationCity}, {LocationState}";

        public string StartDateDisplay => StartDate?.ToShortDateString();

        public string EditLink
        {
            get
            {
                if (IsPending)
                {
                    return
                        $"{ApplicationConfig.ApplicationConfigManager.Settings.ApplicationUrl}registration/pending/{RegistrationKey}";
                }
                else
                {
                    return
                        $"{ApplicationConfig.ApplicationConfigManager.Settings.ApplicationUrl}registration/edit/{RegistrationKey}";
                }
            }
        }

        public string RemoveLink => $"{ApplicationConfig.ApplicationConfigManager.Settings.ApplicationUrl}registration/pending/remove/{RegistrationKey}/{SearchTerm}";

        public string SearchTerm { get; set; }
    }
}