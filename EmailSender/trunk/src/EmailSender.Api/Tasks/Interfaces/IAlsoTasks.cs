using System.Web.Routing;
using Aafp.EmailSender.Api.Dtos;

namespace Aafp.EmailSender.Api.Tasks.Interfaces
{
    public interface IAlsoTasks
    {
        bool SendTestEmail(AlsoMessageDto dto, RequestContext context);

        bool SendWelcomeEmail(AlsoMessageDto dto, RequestContext context);

        bool SendLearnerInstructionsEmail(AlsoLearnerMessageDto dto, RequestContext context);

        bool SendStatusChangeEmail(AlsoStatusChangeMessageDto dto, RequestContext context);
    }
}