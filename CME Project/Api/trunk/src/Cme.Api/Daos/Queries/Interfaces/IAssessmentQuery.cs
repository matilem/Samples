using Aafp.Cme.Api.Dtos;
using System;

namespace Aafp.Cme.Api.Daos.Queries.Interfaces
{
    public interface IAssessmentQuery
    {
        Guid GetAssessmentGroupKeyByActivityNumber(int activityNumber);
    }
}
