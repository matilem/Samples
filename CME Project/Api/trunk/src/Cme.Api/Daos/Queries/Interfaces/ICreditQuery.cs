using System;
using System.Collections.Generic;
using Aafp.Cme.Api.Dtos;

namespace Aafp.Cme.Api.Daos.Queries.Interfaces
{
    public interface ICreditQuery
    {
        List<CreditReElectionDto> GetByCustomerForReElectionCalculation(Guid customerKey, int startYear, int endYear);

        List<CreditTranscriptDto> GetByCustomerForTranscript(Guid customerKey, DateTime startDate, DateTime endDate);
    }
}
