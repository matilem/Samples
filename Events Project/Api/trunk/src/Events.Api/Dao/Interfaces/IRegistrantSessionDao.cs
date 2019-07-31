using System;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IRegistrantSessionDao
    {
        RegistrantSession GetByKey(Guid key);
    }
}
