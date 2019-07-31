using System;
using Aafp.Cme.Api.Helpers;

namespace Aafp.Cme.Api.Dtos
{
    public class CreditTranscriptDto
    {
        public virtual Guid Key { get; set; }

        public virtual Guid CustomerKey { get; set; }

        public virtual string ProviderName { get; set; }

        public virtual string ProviderCity { get; set; }

        public virtual string ProviderStateCode { get; set; }

        public virtual string ProviderCountryCode { get; set; }

        public virtual string ProviderPostalCode { get; set; }

        public virtual DateTime ParticipationBeginDate { get; set; }

        public virtual DateTime? ParticipationEndDate { get; set; }

        public virtual decimal ElectiveCredits { get; set; }

        public virtual decimal PrescribedCredits { get; set; }

        public virtual string SessionEntityName { get; set; }

        public virtual string SessionEntityTopic { get; set; }

        public virtual string Title { get; set; }

        public virtual string SessionCity { get; set; }

        public virtual string SessionStateCode { get; set; }

        public virtual string SessionCountryCode { get; set; }

        public virtual string SessionPostalCode { get; set; }

        public virtual bool? DesignatedForAma { get; set; }

        public virtual bool? DesignatedForAoa { get; set; }

        public virtual bool? DesignatedForCfpc { get; set; }

        public virtual bool? DesignatedForQchp { get; set; }

        public virtual Guid? SessionKey { get; set; }

        public virtual string SessionTitle { get; set; }

        public virtual string ActivityTitle { get; set; }

        public virtual bool AllowCertificate { get; set; }

        public virtual string CreditTypeName { get; set; }

        public virtual string ActivityTypeName { get; set; }
       
        public virtual string CreditLimitType { get; set; }

        public virtual string CreditGroupType { get; set; }

        public virtual string ActivitySubTypeName { get; set; }

        public virtual string ActivityCategoryName { get; set; }

        public virtual string ActivityProviderName { get; set; }    
               
        public virtual string ProducerTypeCode { get; set; }

        public virtual string ProviderDisplayName
        {
            get
            {
                if (!IsWriteIn())
                {
                    return ActivityProviderName;
                }
                else
                {
                    return ProviderName;
                }
            }
        }

        public virtual string TitleDisplay()
        {
            string title = String.Empty;

            if (IsWriteIn() || (IsAlso() && ActivitySubTypeName != ActivitySubTypeHelper.BasicLifeSupportProvider))
            {
                if (!string.IsNullOrWhiteSpace(CreditTypeName))
                {
                    if (CreditTypeName == CreditTypeHelper.Teaching && IsAlso())
                        title = CreditTypeHelper.Teaching + ": " + Title;
                    else
                        title = (CreditLimitType == "I") ? CreditTypeName : CreditTypeName + ": " + Title;
                }
            }
            else if (!string.IsNullOrEmpty(Title))
            {
                title = SessionTitle;
            }
            else if (!string.IsNullOrEmpty(ActivityTitle))
            {
                if (ActivityTitle != SessionTitle)
                {
                    title = string.Format("{0} : {1}", ActivityTitle, SessionTitle);
                }
                else
                {
                    title = ActivityTitle;
                }
            }

            return title;
        }

        public virtual bool IsWriteIn()
        {
            // ALSO courses should not be write in
            if (IsAlso())
                return false;

            return !string.IsNullOrWhiteSpace(CreditTypeName);
        }

        public virtual bool IsAlso()
        {
            return ActivitySubTypeName == ActivitySubTypeHelper.LifeSupportRefresher ||
                    ActivitySubTypeName == ActivitySubTypeHelper.LifeSupportProvider ||
                    ActivitySubTypeName == ActivitySubTypeHelper.LifeSupportInstructor ||
                    ActivitySubTypeName == ActivitySubTypeHelper.BasicLifeSupportProvider;
        }

        public virtual bool IsGroup()
        {
            return (IsWriteIn() && CreditGroupType == "G")
                || (!string.IsNullOrWhiteSpace(ActivityTypeName)
                    && ((ActivityTypeName == ActivityTypeHelper.LiveActivity)
                        || (ActivityTypeName == ActivityTypeHelper.SatelliteEvent)));
        }

        public virtual bool IsPt()
        {
            return false; // ActivityCategoryName == ActivityCategoryHelper.Pt;
        }

        public virtual bool AllowEditingOnTranscript()
        {
            if (!string.IsNullOrWhiteSpace(CreditTypeName))
                return true;

            if (CreditTypeName == CreditTypeHelper.TranslationToPractice)
                return false;

            return true;
        }

        public virtual bool ShowCertificateOnTranscript()
        {
            return SessionKey.HasValue
                   && ProducerTypeCode == "AAFP"
                   && AllowCertificate;
        }
    }
}