using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos.User.Registration;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IStepDao
    {
        Step GetByKey(Guid key);

        List<UserRegistrationNavigationStepDto> GetNavigationSteps(Guid eventKey);
    }
}
