using System;

namespace Aafp.Also.Api.Dtos
{
    public class ActivityPostCourseLearnerDto
    {
        public Guid LearnerKey { get; set; }

        public Guid CustomerKey { get; set; }

        public string CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid OccupationKey { get; set; }

        public bool PassedFlag { get; set; }

        public bool FailedFlag { get; set; }

        public bool NoShowFlag { get; set; }

        public bool Eligible { get; set; }
    }
}