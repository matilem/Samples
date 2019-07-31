using System.Collections.Generic;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationNavigationViewModel
    {
        public int CurrentProgress { get; set; }

        public string IntroUrl { get; set; }

        public string ContactInfoUrl { get; set; }

        public string ConflictUrl { get; set; }

        public string PaymentUrl { get; set; }

        public List<RegistrationNavigationStepViewModel> NavigationSteps { get; set; }
        
        public int StepCount
        {
            get
            {
                return NavigationSteps.Count + 4;
            }
        }
        
        public string RegistrationStatus {get; set; }
    }
}