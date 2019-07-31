using System.Web.Routing;
using Aafp.EmailSender.Api.Dtos;
using Aafp.EmailSender.Api.Helpers;
using Aafp.EmailSender.Api.Tasks.Interfaces;

namespace Aafp.EmailSender.Api.Tasks
{
    public class PhysicianWellBeingTasks : IPhysicianWellBeingTasks
    {
        public bool SendTestEmail(PhysicianWellBeingMessageDto dto, RequestContext context)
        {
            dto.HtmlBody = RazorViewGenerator.RenderView("PWB", "Feedback", dto, context);
            dto.TextBody = RazorViewGenerator.RenderPlainTextView("PWB", "Feedback", dto, context);

            var success = EmailClient.Send(dto);

            return success;
        }

        public bool SendFeedbackEmail(PhysicianWellBeingMessageDto dto, RequestContext context)
        {
            dto.From = dto.From;
            dto.To.Add("dagboola@aafp.org");//required email;
            dto.Subject = "Feedback to Planner From " + dto.Name;

            dto.HtmlBody = RazorViewGenerator.RenderView("PhysicianWellBeing", "Feedback", dto, context);
            dto.TextBody = RazorViewGenerator.RenderPlainTextView("PhysicianWellBeing", "FeedbackPlainText", dto, context);

            var success = EmailClient.Send(dto);

            return success;
        }
    }
}