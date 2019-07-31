using System;
using System.Configuration;
using System.Net.Mime;
using System.Xml;
using System.IO;
using QuestDiagnostics.ExamCentral.Core.Interfaces;
using QuestDiagnostics.Paramed.Applicint.Core.Common;
using QuestDiagnostics.Paramed.Applicint.Core.Interfaces;
using Salesforce.GetDemographicInfo.Repo;

namespace Salesforce.GetDemographicInfo.Library
{
    public class InboundRequestProcessor
    {
        private Stream _requestStream { get; set; }
        
        public InboundRequestProcessor(Stream requestStream)
        {
            _requestStream = requestStream;
        }

        public Applicant Process(ILogger Logger, IHTTPInboundRequestScraper HTTPInboundRequestScraper)
        {
            //Logger.Log(Statics.BuildLogMessage(LogMessage.Level.Trace, "InboundRequestProcessor.Process Begin"));

            var x = HTTPInboundRequestScraper.ScrapeRequest(_requestStream);

            var applicant = new DB(Logger).p_GetDemographicInfo(x.Id);
            applicant.Services = new DB(Logger).p_GetDemographicInfoSvc(x.Id);

            return applicant;
        }
    }
}
