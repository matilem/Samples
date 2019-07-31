using System;
using System.Collections.Generic;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IRegistrantEventDao
    {
        List<CustomerEvent> GetRegistrantEvents(Guid customerKey);
    }
}
