namespace Aafp.Events.Web.ApplicationConfig
{
    public interface IApplicationConfig
    {
        string BaseUrl { get; }

        string ConnectionString { get; }

        string SyndicationHeaderUrl { get; }

        string SyndicationFooterUrl { get; }

        string SyndicationSimpleHeaderUrl { get; }

        string SyndicationSimpleFooterUrl { get; }

        string SyndicationCssBaseUrl { get; }

        string SyndicationJsBaseUrl { get; }

        string SyndicationImageBaseUrl { get; }

        string AuthenticationLoginUrl { get; }

        string OmnitureUrl { get; }

        string ApplicationUrl { get; }

        string EventServiceUrl { get; }

        string PaymentsUrl { get; }

        string ReportServerUrl { get; }
    }
}
