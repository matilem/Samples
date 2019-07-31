using System.Collections.Generic;

namespace Aafp.Also.Web.ViewModels
{
    public class PostCourseViewModel
    {
        public List<PostCourseLearnerViewModel> Learners { get; set; }

        public List<InstructorViewModel> Instructors { get; set; }

        public ActivityViewModel Activity { get; set; }

        public List<LearnerOccupationViewModel> LearnerOccupations { get; set; }

        public IndividualViewModel Customer { get; set; }

        public AlsoCourseViewModel AlsoCourse { get; set; }

        public List<NoteViewModel> Notes { get; set; }

        public string AlsoHome
        {
            get
            {
                return $"{ApplicationConfig.BaseUrl}/also/home";
            }
        }

        public string PreCourseUrl
        {
            get
            {
                return $"{ApplicationConfig.BaseUrl}/also/precourse/activity/" + Activity.ActivityNumber;
            }
        }

        public string UploadUrl
        {
            get
            {
                return $"{ApplicationConfig.BaseUrl}/also/fileupload/ajaxupload";
            }
        }
    }
}