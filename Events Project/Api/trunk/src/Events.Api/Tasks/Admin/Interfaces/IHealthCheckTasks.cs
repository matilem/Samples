using System.Collections.Generic;
using Aafp.Events.Api.Dtos;

namespace Aafp.Events.Api.Tasks.Interfaces
{
    public interface IHealthCheckTasks
    {
        List<HealthCheckResultDto> CheckHealth();
    }
}
