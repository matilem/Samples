using System;

namespace Aafp.Also.Web.ViewModels
{
    public class ActivityViewModel
    {
        public Guid ActivityKey { get; set; }

        public int ActivityNumber { get; set; }

        public string ActivityTitle { get; set; }

        public DateTime? ActivityBeginDate { get; set; }

        public DateTime? ActivityEndDate { get; set; }

        public string ActivityCourseType { get; set; }

        public string ActivityCity { get; set; }

        public string ActivityState { get; set; }

        public string ActivitySponsorName { get; set; }

        public string ActivityDirectorName { get; set; }

        public string ActivityDirectorEmail { get; set; }

        public string ActivityCoordinatorName { get; set; }

        public string ActivityCoordinatorEmail { get; set; }


        public Guid ActivitySessionKey { get; set; }

        public string CMEApplicationStatus { get; set; }

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

        public string ActivityLocation => $"{ActivityCity}, {ActivityState}";

        public string CMEApplicationStatusDisplay
        {
            get
            {
                var display = string.Empty;

                if (CMEApplicationStatus != string.Empty)
                {
                    display = CMEApplicationStatus;
                }
                else
                {
                    display = "No Status Available";
                }

                return display;
            }
        }
    }
}