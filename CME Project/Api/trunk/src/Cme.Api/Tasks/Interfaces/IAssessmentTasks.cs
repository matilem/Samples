using System;

namespace Aafp.Cme.Api.Tasks.Interfaces
{
    public interface IAssessmentTasks
    {
        Guid GetAssessmentByActivityNumber(int activityNumber);
    }
}