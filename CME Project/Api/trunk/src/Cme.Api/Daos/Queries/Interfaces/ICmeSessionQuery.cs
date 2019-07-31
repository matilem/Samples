using Aafp.Cme.Api.Dtos;
using System;

namespace Aafp.Cme.Api.Daos.Queries.Interfaces
{
    public interface ICmeSessionQuery
    {
        CmeActivitySessionDto GetCmeSessionsByKey(Guid sessionKey);
    }
}