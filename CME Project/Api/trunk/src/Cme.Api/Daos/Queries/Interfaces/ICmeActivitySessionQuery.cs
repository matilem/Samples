using Aafp.Cme.Api.Dtos;
using System.Collections.Generic;

namespace Aafp.Cme.Api.Daos.Queries.Interfaces
{
    public interface ICmeActivitySessionQuery
    {
       List<CmeActivitySessionDto> GetCmeSessionsByActivity(int activityNumber);
    }
}
