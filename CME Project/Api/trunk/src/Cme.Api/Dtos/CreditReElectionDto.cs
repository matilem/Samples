using System;
using Aafp.Cme.Api.Helpers;

namespace Aafp.Cme.Api.Dtos
{
    public class CreditReElectionDto
    {
        public virtual decimal ElectiveCredits { get; set; }

        public virtual decimal PrescribedCredits { get; set; }

        public Guid? SessionKey { get; set; }

        public Guid? ActivityKey { get; set; }

        public string ActivitySubTypeTitle { get; set; }

        public bool FloridaChapterApproved { get; set; }

        public bool MarylandChapterApproved { get; set; }

        public string ActivityType { get; set; }

        public Guid? CreditTypeKey { get; set; }

        public string CreditTypeTitle { get; set; }

        public string CreditTypeGroupType { get; set; }

        public string CreditTypeDesignation { get; set; }

        public string CreditTypeLimitType { get; set; }
        
        public virtual bool IsWriteIn()
        {
            // ALSO courses should not be write in
            if (IsAlso())
                return false;

            return CreditTypeKey.HasValue;
        }

        public virtual bool IsGroup()
        {
            return (IsWriteIn() && CreditTypeGroupType == "G")
                || (!string.IsNullOrWhiteSpace(ActivityType)
                    && (ActivityType == ActivityTypeHelper.LiveActivity
                        || ActivityType == ActivityTypeHelper.SatelliteEvent
                        || ActivityType == ActivityTypeHelper.LiveActivity
                        || ActivityType == ActivityTypeHelper.KsaLiveActivity));
        }
        
        public virtual bool IsAlso()
        {
            return SessionKey.HasValue
                   && !string.IsNullOrWhiteSpace(ActivitySubTypeTitle)
                   && ActivitySubTypeTitle == ActivitySubTypeHelper.LifeSupportRefresher 
                   || ActivitySubTypeTitle == ActivitySubTypeHelper.LifeSupportProvider 
                   || ActivitySubTypeTitle == ActivitySubTypeHelper.LifeSupportInstructor 
                   || ActivitySubTypeTitle == ActivitySubTypeHelper.BasicLifeSupportProvider;
        }
    }
}