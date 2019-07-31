using System;

namespace Aafp.Also.Api.Dtos
{
    public class AlsoMessageDto
    {
        public string DiscountCode { get; set; }

        public DateTime? ActivityBeginDate { get; set; }

        public DateTime? ActivityEndDate { get; set; }

        public string ActivityLocation { get; set; }

        public string ActivitySponsorName { get; set; }

        public string CourseDirectorEmail { get; set; }

        public string CourseCoordinatorEmail { get; set; }

        public string ActivityCourseType { get; set; }

        public string ActivityDateDisplay
        {
            get
            {
                var display = string.Empty;

                if (ActivityBeginDate.HasValue)
                {
                    display = $"{ActivityBeginDate.Value.ToString("M/d/yyyy")}";

                    if (ActivityEndDate.HasValue & ActivityBeginDate != ActivityEndDate)
                        display += $" - {ActivityEndDate.Value.ToString("M/d/yyyy")}";
                }
                else
                {
                    display = "n/a";
                }

                return display;
            }
        }
    }
}