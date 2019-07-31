using Aafp.Also.Api.Dtos;

namespace Aafp.Also.Api.Daos.Queries.Interfaces
{
    public interface IActivityPreCourseQuery
    {
        ActivityPreCourseDto GetPreCourse(string activityNumber);
    }
}