using System.Collections.Generic;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Tasks.Interfaces;

namespace Aafp.Cme.Api.Tasks
{
    public class CreditTypeTasks : ICreditTypeTasks
    {
        public ICreditTypeQuery CreditTypeQuery { get; set; }

        public List<CreditTypeDto> GetAllCreditTypes()
        {
            return CreditTypeQuery.GetAll();
        }

        public List<CreditTypeDto> GetByLimitType(string limitType)
        {
            return CreditTypeQuery.GetByLimitType(limitType);
        }
    }
}