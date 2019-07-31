using System.Web.Routing;
using Aafp.EmailSender.Api.Dtos;

namespace Aafp.EmailSender.Api.Tasks.Interfaces
{
    public interface IPhysicianWellBeingTasks
    {
        bool SendTestEmail(PhysicianWellBeingMessageDto dto, RequestContext context);

        bool SendFeedbackEmail(PhysicianWellBeingMessageDto dto, RequestContext context);
    }
}
