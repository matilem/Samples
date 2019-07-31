using Aafp.Also.Api.Dtos;
using System;
using System.Collections.Generic;

namespace Aafp.Also.Api.Daos.Queries.Interfaces
{
    public interface IActivityQuery
    {
        List<ActivityDto> GetLearnerAlsoActivities(Guid customerKey);

        List<ActivityDto> GetAlsoActivitiesForStaff();

        ActivityDto GetActivity(string activityNumber);
    }
}