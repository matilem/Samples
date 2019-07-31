namespace Aafp.Events.Web.ApplicationConfig
{
    public class ProductionApplicationConfig : IApplicationConfig
    {
        public string BaseUrl => "https://nf.aafp.org/";

        public string ConnectionString => "server=10.10.10.40;database=netForum;uid=netForumAdminUser;pwd=Sn1k3rz;Application Name=AAFP";

        public string SyndicationHeaderUrl => @"https://js.aafp.net/header.html";

        public string SyndicationFooterUrl => @"https://js.aafp.net/footer.html";

        public string SyndicationSimpleHeaderUrl => @"http://js.aafp.net/simple/header.html";

        public string SyndicationSimpleFooterUrl => @"http://js.aafp.net/simple/footer.html";

        public string SyndicationCssBaseUrl => @"https://css.aafp.net/";

        public string SyndicationJsBaseUrl => @"https://js.aafp.net/";

        public string SyndicationImageBaseUrl => @"https://img.aafp.net/";

        public string AuthenticationLoginUrl => "https://webx-dev.webad.aafp.org/cgi-bin/lg.pl";

        public string OmnitureUrl => "https://js.aafp.net/omniture/aafp-prod-omniture.js";

        public string ApplicationUrl => $"{BaseUrl}events/";

        public string EventServiceUrl => $"{BaseUrl}events-api/";

        public string PaymentsUrl => $"{BaseUrl}payments/";

        public string ReportServerUrl => "http://rs1.webad.aafp.org/ReportServer?/Netforum/";
    }
}