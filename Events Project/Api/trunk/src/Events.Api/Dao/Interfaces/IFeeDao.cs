using System;
using System.Collections.Generic;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IFeeDao
    {
        List<Fee> GetEventFeesByCustomerForAdmin(Guid eventKey, Guid customerKey, DateTime? registrationDate);

        List<Fee> GetEventFeesByCustomer(Guid eventKey, Guid customerKey, bool isMember, DateTime? registrationDate);

        List<Fee> GetSessionFeesForCustomer(Guid customerKey, Guid eventKey, DateTime? registrationDate);

        List<Fee> GetEventFeesForBatch(Guid eventKey);
    }
}
