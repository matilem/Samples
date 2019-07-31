using System.Collections.Generic;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;

namespace Aafp.Cme.Api.Tasks.Interfaces
{
    public interface ICreditTypeTasks
    {
        ICreditTypeQuery CreditTypeQuery { get; set; }

        List<CreditTypeDto> GetAllCreditTypes();

        List<CreditTypeDto> GetByLimitType(string limitType);
    }
}
