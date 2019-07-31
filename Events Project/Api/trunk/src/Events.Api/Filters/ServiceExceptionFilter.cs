using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using log4net;

namespace Aafp.Events.Api.Filters
{
    public class ServiceExceptionFilter : ExceptionFilterAttribute
    {
        protected static readonly ILog Log = LogManager.GetLogger("EventService");

        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException)
            {
                Log.Error(((HttpResponseException)context.Exception).Response, context.Exception);
                var httpResponseException = (HttpResponseException)context.Exception;

                throw httpResponseException;
            }
            else
            {
                Log.Error(context.Exception.Message, context.Exception);

                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "CriticalException"
                });
            }
        }
    }
}