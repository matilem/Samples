using System.Collections.Generic;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos;
using Aafp.Events.Api.Tasks.Interfaces;

namespace Aafp.Events.Api.Tasks
{
    public class HealthCheckTasks : IHealthCheckTasks
    {
        public IHealthCheckDao HealthCheckDao { get; set; }

        public List<HealthCheckResultDto> CheckHealth()
        {
            var results = new List<HealthCheckResultDto>();

            results.Add(HealthCheckDao.CanConnectToDatabase());
            results.Add(new HealthCheckResultDto
            {
                Success = true,
                Message = "EventService is responding."
            });

            return results;
        } 
    }
}