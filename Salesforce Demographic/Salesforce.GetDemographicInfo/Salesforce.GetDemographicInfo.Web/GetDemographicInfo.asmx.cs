using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using QuestDiagnostics.ExamCentral.Core.Interfaces;
using Salesforce.GetDemographicInfo.Library;
using Salesforce.GetDemographicInfo.Repo;
using QuestDiagnostics.ExamCentral.Core.Logging;
using QuestDiagnostics.ExamCentral.Core.MessageRepository;

namespace Salesforce.GetDemographicInfo.Web
{
    /// <summary>
    /// Summary description for GetDemographicInfo
    /// </summary>
    [WebService(Namespace = "http://Salesforce/GetDemographicInfo")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Salesforce : System.Web.Services.WebService
    {
        public AuthHeader Authentication;

        [SoapHeader("Authentication")]
        [WebMethod]
        public Applicant GetDemoInfoResponse()
        {
            IMessageQueueRepository iMessageQueueRepository = new MSMQRespository();

            ILogger iLogger = new Logger(iMessageQueueRepository, StaticStrings.AuditQueuekey);

            IHTTPInboundRequestScraper iHTTPInboundRequestScraper = new InboundRequestScraper();

            var DataPush = new InboundRequestProcessor(HttpContext.Current.Request.InputStream);

            var applicant = DataPush.Process(iLogger, iHTTPInboundRequestScraper);

            return applicant;//DataPush.Process(iLogger, iHTTPInboundRequestScraper, null);
        }

        public class AuthHeader : SoapHeader
        {
            public string Username { get { return ConfigurationManager.AppSettings["UserName"]; } }
            public string Password { get { return ConfigurationManager.AppSettings["Password"]; } }
            public string TrackingId { get { return ConfigurationManager.AppSettings["TrackingId"]; } }
        }
    }

    public class GetDemoInfoRequest
    {
        public int Id { get; set; }
        public string LineofBusiness { get; set; }
    }
}
