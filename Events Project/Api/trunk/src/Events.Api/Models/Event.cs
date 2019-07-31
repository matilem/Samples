using System;

namespace Aafp.Events.Api.Models
{
    using System.Collections.Generic;

    public class Event
    {
        #region Fields
        
        private string alternativeCompanyBadgeLabel;
        private string alternativePositionBadgeLabel;

        #endregion

        #region Properties

        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool DeleteFlag { get; set; }

        public virtual string Title { get; set; }

        public virtual IList<Step> Steps { get; set; }

        public virtual IList<Session> Sessions { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual DateTime? PreRegistrationDate { get; set; }

        public virtual DateTime? CutOffDate { get; set; }

        public virtual DateTime? RemoveFromWebDate { get; set; }

        public virtual DateTime? PostToWebDate { get; set; }

        public virtual int Capacity { get; set; }

        public virtual string Code { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual string HousingUrl { get; set; }

        public virtual string HousingDescription { get; set; }

        public virtual string HousingDiscountCode { get; set; }

        public virtual bool DisplayEducationQuestion { get; set; }

        public virtual bool DisplayBadgeCompany { get; set; }

        public virtual bool DisplayBadgePosition { get; set; }

        public virtual string ConfirmationInstructions { get; set; }

        public virtual string DenialMessage { get; set; }

        public virtual string CancellationPolicy { get; set; }

        public virtual bool TinyRegFlag { get; set; }

        public virtual string TinyRegMessage { get; set; }

        public virtual string AlternativeCompanyBadgeLabel
        {
            get
            {
                if (string.IsNullOrEmpty(alternativeCompanyBadgeLabel))
                    return "Company";
                return alternativeCompanyBadgeLabel;
            }

            set
            {
                alternativeCompanyBadgeLabel = value;
            }
        }

        public virtual string AlternativePositionBadgeLabel
        {
            get
            {
                if (string.IsNullOrEmpty(alternativePositionBadgeLabel))
                    return "Position";
                return alternativePositionBadgeLabel;
            }

            set
            {
                alternativePositionBadgeLabel = value;
            }
        }

        public virtual string DescriptionHtml { get; set; }

        public virtual bool IsOrganizationalApplicationFlag { get; set; }

        public virtual string OtherInformation { get; set; }

        public virtual Location Location { get; set; }

        public virtual bool AllowWaitList { get; set; }

        public virtual IList<RegistrantOnWait> RegistrantsOnWait { get; set; }
        
        public virtual IList<Registrant> Registrants { get; set; }

        public virtual IList<Event> RelatedEvents { get; set; }

        public virtual string MarketingMessage { get; set; }

        #endregion

        public virtual bool IsOpenForRegistration(DateTime registrationDate)
        {
            if (PostToWebDate.HasValue && RemoveFromWebDate.HasValue)
                if (registrationDate > PostToWebDate.Value.Date && registrationDate < RemoveFromWebDate.Value.Date)
                    return true;

            return false;
        }
    }
}