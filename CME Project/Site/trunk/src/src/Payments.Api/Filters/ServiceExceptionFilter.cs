using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Filters;
using ApiClientHelper.Components;
using log4net;

namespace Aafp.Payments.Api.Filters
{
    public class ServiceExceptionFilter : ExceptionFilterAttribute
    {
        protected static readonly ILog Log = LogManager.GetLogger("PaymentApi");

        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException)
            {
                Log.Error(((HttpResponseException)context.Exception).Response, context.Exception);
                var httpResponseException = (HttpResponseException)context.Exception;

                throw httpResponseException;
            }

            if (!(context.Exception is RegistrationProcessingException) && !(context.Exception is ServiceException))
            {
                Log.Error(context.Exception.Message, context.Exception);

                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent("{\"Message\":\"An error occurred, please try again or contact the administrator.\"}");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response.ReasonPhrase = "CriticalException";
                context.Response = response;
            }
        }
    }
}