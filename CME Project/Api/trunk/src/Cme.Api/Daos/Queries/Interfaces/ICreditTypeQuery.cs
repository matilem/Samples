using System.Collections.Generic;
using Aafp.Cme.Api.Dtos;

namespace Aafp.Cme.Api.Daos.Queries.Interfaces
{
    public interface ICreditTypeQuery
    {
        List<CreditTypeDto> GetAll();

        List<CreditTypeDto> GetByLimitType(string limitType);
    }
}
