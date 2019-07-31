using System.Threading.Tasks;
using Aafp.Cme.Api.Dtos;

namespace Aafp.Cme.Api.Tasks.Interfaces
{
    public interface IIndividualTasks
    {
        Task<IndividualDto> GetIndividualByWebLogin(string webLogin);

        Task<bool> CheckUserAccess(string webLogin);
    }
}
