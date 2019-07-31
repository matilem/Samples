using Aafp.Cme.Api.Dtos;

namespace Aafp.Cme.Api.Daos.Queries.Interfaces
{
    public interface ICmeActivityQuery
    {
        CmeActivityDto GetCmeSessionsByActivity(int activityNumber);
    }
}
