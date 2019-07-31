using System.Threading.Tasks;
using Aafp.EmailSender.Api.Dtos;

namespace Aafp.EmailSender.Api.Tasks.Interfaces
{
    public interface IIndividualTasks
    {
        Task<IndividualDto> GetByEmail(string email);
    }
}
