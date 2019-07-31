using System.Collections.Generic;
using System.Threading.Tasks;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;

namespace Aafp.Cme.Api.Tasks.Interfaces
{
    public interface ICreditAvailableTasks
    {
        ICreditAvailableQuery CreditAvailableQuery { get; set; }

        IIndividualTasks IndividualTasks { get; set; }

        Task<List<CreditAvailableDto>> GetAllByCustomer(string webLogin);

        Task<List<CreditAvailableDto>> GetPurchasedByCustomer(string webLogin);

        Task<List<CreditAvailableDto>> GetSubscriptionsByCustomer(string webLogin);

        Task<List<CreditAvailableDto>> GetCompletedByCustomer(string webLogin);

        Task<List<CreditAvailableDto>> GetFreeItems();

        Task<CreditAvailableStatsDto> GetCreditTotals(string webLogin);
    }
}
