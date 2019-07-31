using System;
using System.Collections.Generic;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Tasks.Interfaces;

namespace Aafp.Cme.Api.Tasks
{
    public class CmeSessionTasks : ICmeSessionTasks
    {
        public ICmeSessionQuery CmeSessionQuery { get; set; }

        public List<CmeSessionDto> GetByActivityNumber(int activityNumber)
        {
            throw new NotImplementedException();
        }
    }
}