using System;

namespace Aafp.Also.Api.Dtos
{
    public class LearnerSubmissionDto
    {
        public Guid LearnerKey { get; set; }

        public Guid CustomerKey { get; set; }

        public Guid OccupationKey { get; set; }

        public string Grade { get; set; }

        public bool Passed
        {
            get
            {
                var grade = false;

                if(Grade != string.Empty)
                {
                    grade = Grade == "Pass" ? true : false;
                }

                return grade;
            }
        }

        public bool Failed
        {
            get
            {
                var grade = false;

                if (Grade != string.Empty)
                {
                    grade = Grade == "Fail" ? true : false;
                }

                return grade;
            }
        }

        public bool NoShow
        {
            get
            {
                var grade = false;

                if (Grade != string.Empty)
                {
                    grade = Grade == "No-Show" ? true : false;
                }

                return grade;
            }
        }
    }
}