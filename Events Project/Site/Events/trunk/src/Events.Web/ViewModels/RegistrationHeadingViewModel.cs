using System;
using System.Collections.Generic;
using System.Linq;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationHeadingViewModel
    {
        public Guid Key { get; set; }

        public int HeadingSequence { get; set; }

        public string HeadingHeading { get; set; }

        public string HeadingDescription { get; set; }

        public List<RegistrationSessionViewModel> Sessions { get; set; }

        public bool RequiredFlag { get; set; }

        public string RegistrationStatus { get; set; }

        public bool ShowCost
        {
            get
            {
                if (Sessions != null)
                {
                    if (Sessions.Any(x => x.Fee != null && x.Fee.Price > 0m))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool ShowAvailableTickets
        {
            get
            {
                if (Sessions != null)
                {
                    if (Sessions.Any(x => x.SessionTypeCode != "Demographic" && x.SessionTypeCode != "Other"))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool ShowNumber
        {
            get
            {
                if (Sessions != null)
                {
                    if (Sessions.Any(x => x.SessionTypeCode != "Demographic" && x.SessionTypeCode != "Other"))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool ShowTime => ShowAvailableTickets || ShowCost;
    }
}