using Aafp.Also.Api.Dtos;
using System.Threading.Tasks;

namespace Aafp.Also.Api.Tasks.Interfaces
{
    public interface IEmailTasks
    {
        Task<bool> SendWelcomeEmail(AlsoMessageDto dto);

        Task<bool> SendStatusChangeEmail(AlsoStatusChangeMessageDto dto);
    }
}
