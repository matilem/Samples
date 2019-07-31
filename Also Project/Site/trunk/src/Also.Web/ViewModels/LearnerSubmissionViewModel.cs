using System;

namespace Aafp.Also.Web.ViewModels
{
    public class LearnerSubmissionViewModel
    {
        public Guid LearnerKey { get; set; }

        public Guid CustomerKey { get; set; }

        public Guid OccupationKey { get; set; }

        public string Grade { get; set; }
    }
}