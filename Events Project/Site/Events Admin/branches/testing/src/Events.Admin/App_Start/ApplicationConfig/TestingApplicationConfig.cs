namespace Aafp.Events.Admin.ApplicationConfig
{
    public class TestingApplicationConfig : IApplicationConfig
    {
        public string BaseUrl => "http://testing.ams.aafp.org/";

        public string ConnectionString => "server=10.10.10.32;database=netForumTesting;uid=netForumAdminUser;pwd=Sn1k3rz;Application Name=AAFP";

        public string SyndicationHeaderUrl => @"http://js.aafp.net/test/header.html";

        public string SyndicationFooterUrl => @"http://js.aafp.net/test/footer.html";

        public string SyndicationCssBaseUrl => @"http://css.aafp.net/test/";

        public string SyndicationJsBaseUrl => @"http://js.aafp.net/test/";

        public string SyndicationImageBaseUrl => @"http://img.aafp.net/test/";

        public string AuthenticationLoginUrl => "http://webx-qa.webad.aafp.org/cgi-bin/lg.pl";

        public string ApplicationUrl => $"{BaseUrl}events-admin/";

        public string EventServiceUrl => $"{BaseUrl}events-api/";

        public string ReportServerUrl => "http://nf2011-db.webad.aafp.org/ReportServer?/NetforumTesting/";
    }
}