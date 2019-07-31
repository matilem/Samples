using System;

namespace Aafp.EmailSender.Api.Models
{
    public class CustomerCorrespondence
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool DeleteFlag { get; set; }

        public virtual Guid? EntityKey { get; set; }

        public virtual Guid CustomerCorrespondenceKey { get; set; }

        public virtual Guid? CustomerKey { get; set; }

        public virtual string CorrespondenceType { get; set; }

        public virtual DateTime CorrespondenceDate { get; set; }

        public virtual string CommunicationMethod { get; set; }

        public virtual string CommunicationValue { get; set; }

        public virtual string CommunicationDescription { get; set; }

        public virtual string CorrespondenceTypeValue { get; set; }

        public virtual Guid? CorrespondenceTypeKey { get; set; }

        public virtual string CorrespondenceContent { get; set; }

        public virtual Guid? CallToActionKey { get; set; }

        public virtual Guid? CallToActionObjectKey { get; set; }

        public virtual Guid? EmailErrorKey { get; set; }

        public virtual Guid? CorrespondenceTemplateKey { get; set; }

        public virtual Guid? MessageQueueJobKey { get; set; }

        public virtual string CorrespondenceSubject { get; set; }

        public virtual bool CorrespondenceOpenFlag { get; set; }
    }
}