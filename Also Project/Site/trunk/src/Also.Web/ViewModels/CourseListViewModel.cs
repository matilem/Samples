using System;

namespace Aafp.Also.Web.ViewModels
{
    public class CourseListViewModel
    {
        public Guid Key { get; set; }

        public Guid ActivityKey { get; set; }

        public int ActivityNumber { get; set; }

        public string ActivityTitle { get; set; }

        public DateTime? ActivityBeginDate { get; set; }

        public DateTime? ActivityEndDate { get; set; }

        public string ActivityCourseType { get; set; }

        public string ActivityCity { get; set; }

        public string ActivityState { get; set; }

        public string ActivitySponsorName { get; set; }

        public bool ActivityPreCourseSubmitted { get; set; }

        public bool ActivityPreCourseApproved { get; set; }

        public bool ActivityPostCourseSubmitted { get; set; }

        public bool ActivityPostCourseCompleted { get; set; }

        public string Status
        {
            get
            {
                var status = "Pre-Course View";

                if (ActivityPreCourseSubmitted)
                {
                    status = "Approval Pending";
                }

                if (ActivityPreCourseApproved)
                {
                    status = "Approved";
                }

                if (ActivityPostCourseSubmitted)
                {
                    status = "Completion Pending";
                }

                if (ActivityPostCourseCompleted)
                {
                    status = "Completed";
                }

                return status;
            }
        }

        public bool HasError { get; set; }

        public string ErrorMessage { get; set; }

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

        public string ActivityDetailsUrl
        {
            get
            {
                if (ActivityPreCourseApproved)
                {
                    return $"{ApplicationConfig.BaseUrl}/also/postcourse/activity/" + ActivityNumber;
                }
                else
                {

                    return $"{ApplicationConfig.BaseUrl}/also/precourse/activity/" + ActivityNumber;
                }
            }
        }
    }
}