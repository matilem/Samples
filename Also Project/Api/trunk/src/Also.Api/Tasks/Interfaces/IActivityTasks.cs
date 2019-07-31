using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aafp.Also.Api.Tasks.Interfaces
{
    public interface IActivityTasks
    {
        IActivityQuery ActivityQuery { get; set; }

        IIndividualTasks IndividualTasks { get; set; }

        Task<List<ActivityDto>> GetActivitiesForCourseList(string webLogin);
    }
}