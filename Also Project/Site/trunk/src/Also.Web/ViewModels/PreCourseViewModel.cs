using System;
using System.Collections.Generic;

namespace Aafp.Also.Web.ViewModels
{
    public class PreCourseViewModel
    {
        public Guid ApplicationKey { get; set; }

        public ActivityViewModel Activity { get; set; }

        public Guid CourseDirectorKey { get; set; }

        public string CourseDirectorId { get; set; }

        public string CourseDirectorName { get; set; }

        public string CourseDirectorEmail { get; set; }

        public string CourseDirectorPhone { get; set; }

        public Guid CourseCoordinatorKey { get; set; }

        public string CourseCoordinatorId { get; set; }

        public string CourseCoordinatorName { get; set; }

        public string CourseCoordinatorEmail { get; set; }

        public string CourseCoordinatorPhone { get; set; }

        public string ActivitySessionAgendaUrl { get; set; }

        public string ActivityCity { get; set; }

        public string ActivityState { get; set; }

        public string ActivitySponsorName { get; set; }

        public Guid MilitaryKey { get; set; }

        public List<MilitaryBranchViewModel> MilitaryBranches { get; set; }

        public IndividualViewModel Customer { get; set; }

        public AlsoCourseViewModel AlsoCourse { get; set; }

        public List<NoteViewModel> Notes { get; set; }

        public string CMEADashboardURL
        {
            get
            {
                return $"{ApplicationConfig.BaseUrl}/cme/accreditation";
            }
        }

        public string ActivitySessionAgendaUrlDisplay
        {
            get
            {
                return $"{ApplicationConfig.BaseUrl}/cme/accreditation/files/{ActivitySessionAgendaUrl}";
            }
        }

        public string AlsoHome
        {
            get
            {
                return $"{ApplicationConfig.BaseUrl}/also/home"; 
            }
        }

        public string ActivityLocation => $"{ActivityCity}, {ActivityState}";
    }
}