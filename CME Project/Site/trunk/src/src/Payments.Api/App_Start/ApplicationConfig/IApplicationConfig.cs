namespace Aafp.Payments.Api.ApplicationConfig
{
    public interface IApplicationConfig
    {
        string ConnectionString { get; }

        string BaseUrl { get; }

        string EventServiceUrl { get; }

        string EventsEditingEndPointUrl { get; }

        string EventsEditingUrl { get; }

        string EventsEditingUserName { get; }

        string EventsEditingPassword { get; }
    }
}
