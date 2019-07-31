using System;
using System.Linq;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IRegistrantOnWaitDao
    {
        void Store(RegistrantOnWait registrant);

        void Delete(RegistrantOnWait registrant);

        IQueryable<RegistrantOnWait> GetByEventKey(Guid eventKey);
    }
}
