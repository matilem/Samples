using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Models
{
    public class Step
    {
        #region Constants

        public const string RegistrationType = "Registration Type";
        public const string ContactInformation = "Contact Information";
        public const string Badges = "Badges";
        public const string Payment = "Payment";
        public const string Undefined = "Undefined";
        public const string UserDefinedStep = "UserDefinedStep";

        #endregion

        #region Properties

        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool DeleteFlag { get; set; }

        public virtual IList<Heading> Headings { get; set; }

        public virtual int StepSequence { get; set; }

        public virtual string StepHeading { get; set; }

        public virtual string StepDescription { get; set; }

        public virtual Guid EventKey { get; set; }

        #endregion
    }
}