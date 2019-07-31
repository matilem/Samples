using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Tasks.Interfaces;
using System;

namespace Aafp.Cme.Api.Tasks
{
    public class AssessmentTasks : IAssessmentTasks
    {
        public IAssessmentQuery AssessmentQuery { get; set; }

        public Guid GetAssessmentByActivityNumber(int activityNumber)
        {
            return AssessmentQuery.GetAssessmentGroupKeyByActivityNumber(activityNumber);
        }
    }
}