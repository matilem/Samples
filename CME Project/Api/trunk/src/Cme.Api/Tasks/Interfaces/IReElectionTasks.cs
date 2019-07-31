using System.Collections.Generic;
using System.Threading.Tasks;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Helpers;

namespace Aafp.Cme.Api.Tasks.Interfaces
{
    public interface IReElectionTasks
    {
        Task<ReElectionDto> GetReElectionByWebLogin(string webLogin);

        ReElectionTotalsHelper CalculateReElectionTotals(List<CreditReElectionDto> credits, ReElectionTotalsHelper totals, string chapterStateCode);

    }
}
