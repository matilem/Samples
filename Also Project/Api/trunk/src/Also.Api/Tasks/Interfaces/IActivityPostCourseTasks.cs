using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aafp.Also.Api.Tasks.Interfaces
{
    public interface IActivityPostCourseTasks
    {
        IActivityPostCourseQuery ActivityPostCourseQuery { get; set; }

        IIndividualTasks IndividualTasks { get; set; }

        Task<ActivityPostCourseDto> GetPostCourse(string activityNumber, string webLogin);

        Task<bool> SavePostCourse([FromBody] ActivityPostCourseSubmissionDto dto);

        Task<bool> ReportAlsoCredit(string webLogin, string activityNumber, List<Guid> learners);

    }
}
