using Aafp.Also.Api.Dtos;
using System;
using System.Collections.Generic;

namespace Aafp.Also.Api.Daos.Queries.Interfaces
{
    public interface IAlsoStatusQuery
    {
        List<AlsoStatusDto> GetAlsoStatuses(Guid customerKey);

        List<AlsoStatusCourseHistoryDto> GetAlsoCourseHistory(Guid customerKey);

        int GetCmeTeachingCreditsCount(Guid customerKey, int yearsNeeded);

        List<AlsoStatusTypeDto> GetAlsoStatusTypes();

        bool GetAdvisoryFacultyRecommendation(Guid customerKey);
    }
}
