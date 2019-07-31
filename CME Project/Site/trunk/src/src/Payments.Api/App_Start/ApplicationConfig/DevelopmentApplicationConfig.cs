namespace Aafp.Payments.Api.ApplicationConfig
{
    public class DevelopmentApplicationConfig : IApplicationConfig
    {
        public string ConnectionString => "server=nf2011-db.webad.aafp.org;database=netForum;uid=netForumAdminUser;pwd=iW3bBegone;Application Name=AAFP";

        public string BaseUrl => "http://dev.ams.aafp.org/";

        public string EventServiceUrl => $"{BaseUrl}/events-api/";

        public string EventsEditingEndPointUrl => "http://dev.ams.aafp.org/api/v1.0/";

        public string EventsEditingUrl => "http://dev.ams.aafp.org/api/v1.0/registration/ModifyRegistration";

        public string EventsEditingUserName => "GSWebApi";

        public string EventsEditingPassword => "xW3bCred3nt!@l$";

    }
}