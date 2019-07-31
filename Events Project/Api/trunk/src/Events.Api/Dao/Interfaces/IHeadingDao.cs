using System;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IHeadingDao
    {
        Heading GetByKey(Guid key);
    }
}
