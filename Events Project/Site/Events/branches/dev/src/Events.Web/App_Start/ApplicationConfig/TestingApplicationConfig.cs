namespace Aafp.Events.Web.ApplicationConfig
{
    public class TestingApplicationConfig : IApplicationConfig
    {
        public string BaseUrl => "http://testing.ams.aafp.org/";

        public string ConnectionString => "server=testingdb.webad.aafp.org;database=netForum;uid=netForumAdminUser;pwd=iW3bBegone;Application Name=AAFP";

        public string SyndicationHeaderUrl => @"http://js.aafp.net/test/header.html";

        public string SyndicationFooterUrl => @"http://js.aafp.net/test/footer.html";

        public string SyndicationSimpleHeaderUrl => @"http://js.aafp.net/test/simple/header.html";

        public string SyndicationSimpleFooterUrl => @"http://js.aafp.net/test/simple/footer.html";

        public string SyndicationCssBaseUrl => @"http://css.aafp.net/test/";

        public string SyndicationJsBaseUrl => @"http://js.aafp.net/test/";

        public string SyndicationImageBaseUrl => @"http://img.aafp.net/test/";

        public string AuthenticationLoginUrl => "http://webx-qa.webad.aafp.org/cgi-bin/lg.pl";

        public string OmnitureUrl => "http://js.aafp.net/test/omniture/aafp-dev-omniture.js";

        public string ApplicationUrl => $"{BaseUrl}events/";

        public string EventServiceUrl => $"{BaseUrl}events-api/";

        public string PaymentsUrl => $"{BaseUrl}payments/";

        public string ReportServerUrl => "http://nf2011-db.webad.aafp.org/ReportServer?/NetforumTesting/";
    }
}