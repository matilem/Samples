namespace Aafp.Events.Api.ApplicationConfig
{
    public interface IApplicationConfig
    {
        string ConnectionString { get; }

        string BaseUrl { get; }

        string CustomerServiceUrl { get; }

        string ReferenceServiceUrl { get; }

        string PaymentServiceUrl { get; }

        string PaymentSiteUrl { get; }

    }
}
