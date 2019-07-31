using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos;

namespace Aafp.Events.Api.Tasks.Interfaces
{
    public interface IFeeTasks
    {
        IFeeDao FeeDao { get; set; }

        List<EventFeeDto> GetEventFeesByCustomer(Guid eventKey, Guid customerKey, bool isMember, DateTime? registrationDate);

        List<EventFeeDto> GetEventFeesByCustomerForAdmin(Guid eventKey, Guid customerKey, DateTime? registrationDate);

        List<SessionFeeDto> GetSessionFeesByCustomer(Guid customerKey, Guid eventKey, int number, DateTime? registrationDate);
    }
}
