using Aafp.Events.Api.Dtos;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IHealthCheckDao
    {
        HealthCheckResultDto CanConnectToDatabase();
    }
}
