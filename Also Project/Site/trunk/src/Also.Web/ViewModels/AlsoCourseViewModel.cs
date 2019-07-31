using System;

namespace Aafp.Also.Web.ViewModels
{
    public class AlsoCourseViewModel
    {
        public Guid AlsoCourseKey { get; set; }

        public string AddUser { get; set; }

        public DateTime AddDate { get; set; }

        public string ChangeUser { get; set; }

        public DateTime? ChangeDate { get; set; }

        public bool IsDeleted { get; set; }

        public Guid EntityKey { get; set; }

        public Guid ActivityKey { get; set; }

        public Guid PriceKey { get; set; }

        public string DiscountCode { get; set; }

        public Guid MilitaryBranchKey { get; set; }

        public string MilitaryBranch { get; set; }

        public bool PreCourseSubmittedFlag { get; set; }

        public bool PreCourseApprovedFlag { get; set; }

        public bool PostCourseSubmittedFlag { get; set; }

        public bool PostCourseCompletedFlag { get; set; }
    }
}