using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using System.Threading.Tasks;

namespace Aafp.Also.Api.Tasks.Interfaces
{
    public interface IActivityPreCourseTasks
    {
        IActivityPreCourseQuery ActivityPreCourseQuery { get; set; }

        IIndividualTasks IndividualTasks { get; set; }

        Task<ActivityPreCourseDto> GetPreCourse(string activityNumber, string webLogin);

        Task<bool> SavePreCourse(ActivityPreCourseSubmissionDto dto);
    }
}
