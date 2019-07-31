namespace Aafp.Payments.Api.ApplicationConfig
{
    public class TestingApplicationConfig : IApplicationConfig
    {
        public string BaseUrl => "http://testing.ams.aafp.org/";

        public string ConnectionString => "server=testingdb.webad.aafp.org;database=netForum;uid=netForumAdminUser;pwd=iW3bBegone;Application Name=AAFP";
        
        public string EventServiceUrl => $"{BaseUrl}/events-api/";

        public string EventsEditingEndPointUrl => "http://testing.ams.aafp.org/api/v1.0/";

        public string EventsEditingUrl => "http://testing.ams.aafp.org/api/v1.0/registration/ModifyRegistration";

        public string EventsEditingUserName => "GSWebApi";

        public string EventsEditingPassword => "xW3bCred3nt!@l$";

    }
}