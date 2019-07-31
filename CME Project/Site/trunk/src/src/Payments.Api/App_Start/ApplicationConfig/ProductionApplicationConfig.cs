namespace Aafp.Payments.Api.ApplicationConfig
{
    public class ProductionApplicationConfig : IApplicationConfig
    {
        public string BaseUrl => "https://nf.aafp.org/";

        public string ConnectionString => "server=db1.webad.aafp.org;database=netForum;uid=netForumAdminUser;pwd=iW3bBegone;Application Name=AAFP";
        
        public string EventServiceUrl => $"{BaseUrl}/events-api/";

        public string EventsEditingEndPointUrl => "https://nf.aafp.org/api/v1.0/";

        public string EventsEditingUrl => "https://nf.aafp.org/api/v1.0/registration/ModifyRegistration";

        public string EventsEditingUserName => "GSWebApi";

        public string EventsEditingPassword => "xW3bCred3nt!@l$";

    }
}