using System;
using System.Collections.Generic;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;
using System.Threading.Tasks;

namespace Aafp.Cme.Api.Tasks.Interfaces
{
    public interface ICreditTasks
    {
        IIndividualTasks IndividualTasks { get; set; }

        ICreditQuery CreditQuery { get; set; }

        List<CreditReElectionDto> GetByCustomerForReElectionCalculation(Guid customerKey, int startYear, int endYear);

        Task<List<CreditDto>> ReportCmeCredit(string webLogin, List<Guid> credits);

        bool ReportCmeCreditBySession(AlsoCreditDto dto);

        bool ReportTeachingCreditBySession(TeachingCreditDto dto);

        Task<List<CreditTranscriptDto>> GetLiveCreditsForTranscript(string webLogin);
    }
}
