namespace Aafp.Events.Api.ApplicationConfig
{
    public class TestingApplicationConfig : IApplicationConfig
    {
        public string ConnectionString => "server=testingdb.webad.aafp.org;database=netForum;uid=netForumAdminUser;pwd=iW3bBegone;Application Name=AAFP";

        public string BaseUrl => "http://testing.ams.aafp.org/";

        public string CustomerServiceUrl => $"{BaseUrl}customers-api/";

        public string ReferenceServiceUrl => $"{BaseUrl}reference-api/";

        public string PaymentServiceUrl => $"{BaseUrl}payments-api/";

        public string PaymentSiteUrl => $"{BaseUrl}payments/";

    }
}