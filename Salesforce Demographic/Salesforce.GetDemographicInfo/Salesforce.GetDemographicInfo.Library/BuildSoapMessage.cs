using System;
using System.Xml;
using QuestDiagnostics.ExamCentral.Core.Interfaces;
using QuestDiagnostics.Paramed.Applicint.Core.Common;
using System.Configuration;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using Salesforce.GetDemographicInfo.Repo;

namespace Salesforce.GetDemographicInfo.Library
{
    public class BuildSoapMessage
    {
        private ILogger Logger { get; set; }

        public void BuildMessage(ILogger Logger)
        {
            Logger.Log(Statics.BuildLogMessage(LogMessage.Level.Trace, "Method:  BuildSoapMessage.BuildMessage Begin {0}"));

            Applicant applicant = new Applicant();

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(Serialize(applicant));

            Logger.Log(Statics.BuildLogMessage(LogMessage.Level.Trace, "Method:  BuildSoapMessage.BuildMessage End {0}"));
        }

        public static string Serialize(object obj)
        { 
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            MemoryStream buffer = new MemoryStream();
            xs.Serialize(buffer, obj);
            return ASCIIEncoding.ASCII.GetString(buffer.ToArray());
        }

        public static object Deserialize(Type typeToDeserialize, string xmlString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
            MemoryStream mem = new MemoryStream(bytes);
            XmlSerializer ser = new XmlSerializer(typeToDeserialize);
            return ser.Deserialize(mem);
        }
    }
}

     
