using System.Collections.Generic;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class RegistrationStepDto
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

        public IList<RegistrationHeadingDto> Headings { get; set; }

        public int StepSequence { get; set; }

        public string StepHeading { get; set; }

        public string StepDescription { get; set; }

        #endregion
    }
}