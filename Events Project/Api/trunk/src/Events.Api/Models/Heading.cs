using System;

namespace Aafp.Events.Api.Models
{
    public class Heading
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool DeleteFlag { get; set; }

        public virtual int HeadingSequence { get; set; }

        public virtual string HeadingHeading { get; set; }

        public virtual string HeadingDescription { get; set; }

        public virtual bool RequiredFlag { get; set; }
    }
}