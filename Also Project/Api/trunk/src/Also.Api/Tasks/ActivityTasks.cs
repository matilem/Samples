using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aafp.Also.Api.Tasks
{
    public class ActivityTasks : IActivityTasks
    {
        public IIndividualTasks IndividualTasks { get; set; }

        public IActivityQuery ActivityQuery { get; set; }

        public async Task<List<ActivityDto>> GetActivitiesForCourseList(string webLogin)
        {
            var dto = new List<ActivityDto>();
            var individual = await IndividualTasks.GetIndividualByWebLogin(webLogin);

            if (individual.IsAafpStaff)
            {
                dto = ActivityQuery.GetAlsoActivitiesForStaff();
            }
            else
            {
                dto = ActivityQuery.GetLearnerAlsoActivities(individual.Key);
            }

            return dto;
        }
    }
}