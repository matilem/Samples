using System;

namespace Aafp.Cme.Api.Models
{
    public class Credit
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual Guid EntityKey { get; set; }

        public virtual Guid CustomerKey { get; set; }

        public virtual string ProviderName { get; set; }

        public virtual string ProviderCity { get; set; }

        public virtual string ProviderStateCode { get; set; }

        public virtual string ProviderCountryCode { get; set; }

        public virtual string ProviderPostalCode { get; set; }

        public virtual DateTime ParticipationBeginDate { get; set; }

        public virtual DateTime ParticipationEndDate { get; set; }

        public virtual decimal ElectiveCredits { get; set; }

        public virtual decimal PrescribedCredits { get; set; }

        public virtual Guid SessionKey { get; set; }

        public virtual string SessionEntityName { get; set; }

        public virtual string SessionEntityTopic { get; set; }

        public virtual string SessionTitle { get; set; }

        public virtual string SessionCity { get; set; }

        public virtual string SessionStateCode { get; set; }

        public virtual string SessionCountryCode { get; set; }

        public virtual string SessionPostalCode { get; set; }

        public virtual bool DesignatedForAma { get; set; }

        public virtual bool DesignatedForAoa { get; set; }

        public virtual bool DesignatedForCfpc { get; set; }

        public virtual bool DesignatedForQchp { get; set; }

        public virtual Guid CreditSourceKey { get; set; }

        public virtual Guid CreditTypeKey { get; set; }
    }
}