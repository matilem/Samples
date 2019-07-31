using System;
using System.Collections.Generic;
using Aafp.Cme.Api.Dtos;

namespace Aafp.Cme.Api.Tasks.Interfaces
{
    public interface ICmeSessionTasks
    {
        List<CmeSessionDto> GetByActivityNumber(int activityNumber);
    }
}
