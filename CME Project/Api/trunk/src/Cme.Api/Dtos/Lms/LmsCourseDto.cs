namespace Aafp.Cme.Api.Dtos.Lms
{
    public class LmsCourseDto
    {
        public CourseNodeDto Nid { get; set; }
    }

    public class CourseNodeDto
    {
        public string Uri { get; set; }

        public string Id { get; set; }

        public string Resource { get; set; }

        public string Uuid { get; set; }
    }
}