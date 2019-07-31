using System;

namespace Aafp.MyCme.Web.ViewModels
{
    public class CmeActivitySessionViewModel
    {
        public Guid SessionKey { get; set; }

        public string SessionTitle { get; set; }

        public DateTime SessionBeginDate { get; set; }

        public DateTime SessionEndDate { get; set; }

        public string SessionCity { get; set; }

        public string SessionState { get; set; }

        public decimal SessionPrescribedCredits { get; set; }

        public decimal SessionElectiveCredits { get; set; }

        public bool Reported { get; set; }

        public string SessionDateDisplay
        {
            get
            {
                string display = string.Empty;

                display = $"{SessionBeginDate.ToString("ddd, MMM d, yyyy")}";

                return display;
            }
        }

        public string SessionTimeDisplay
        {
            get
            {
                string display;

                if (SessionBeginDate.TimeOfDay != new TimeSpan(0, 0, 0))
                {
                    display = $"{SessionBeginDate:t}";
                }
                else
                    display = "n/a";

                return display;
            }
        }
    }
}