using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Serialization;
using System;

namespace Salesforce.GetDemographicInfo.Library
{
    public interface IHTTPInboundRequestScraper
    {
        InboundRequestScrappedData ScrapeRequest(Stream RequestStream);
    }

    public class InboundRequestScraper : IHTTPInboundRequestScraper
    {
        //public InboundRequestScraper()
        //{}

        public InboundRequestScrappedData ScrapeRequest(Stream RequestStream)
        {
           InboundRequestScrappedData ScrappedData = new InboundRequestScrappedData();

           XmlDocument xDoc = new XmlDocument();

           RequestStream.Position = 0;

           xDoc.Load(RequestStream);

           ScrappedData = Deserialize(typeof(InboundRequestScrappedData), xDoc.FirstChild.FirstChild.NextSibling.InnerXml) as InboundRequestScrappedData;

           return ScrappedData;
        }

        public object Deserialize(Type typeToDeserialize, string xmlString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
            MemoryStream mem = new MemoryStream(bytes);
            XmlSerializer ser = new XmlSerializer(typeToDeserialize);
            return ser.Deserialize(mem);
        }
    }

    public class InboundRequestScrappedData
    {
        public string LineOfBusiness { get; set; }
        public int Id { get; set; }
    }
}