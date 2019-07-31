using Aafp.Cme.Api.Dtos;
using System.Threading.Tasks;

namespace Aafp.Cme.Api.Tasks.Interfaces
{
    public interface ICmeActivityTasks
    {
        ICreditTasks CreditTasks { get; set; }

        IIndividualTasks IndividualTasks { get; set; }

        Task<CmeActivityDto> GetCmeSessionsByActivity(int activityNumber, string webLogin);
    }
}
