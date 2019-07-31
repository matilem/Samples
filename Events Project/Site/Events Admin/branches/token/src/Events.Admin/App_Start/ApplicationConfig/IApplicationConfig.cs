namespace Aafp.Events.Admin.ApplicationConfig
{
    public interface IApplicationConfig
    {
        string BaseUrl { get; }

        string ConnectionString { get; }

        string SyndicationHeaderUrl { get; }

        string SyndicationFooterUrl { get; }

        string SyndicationCssBaseUrl { get; }

        string SyndicationJsBaseUrl { get; }

        string SyndicationImageBaseUrl { get; }

        string AuthenticationLoginUrl { get; }

        string ApplicationUrl { get; }

        string EventServiceUrl { get; }

        string ReportServerUrl { get; }
    }
}
