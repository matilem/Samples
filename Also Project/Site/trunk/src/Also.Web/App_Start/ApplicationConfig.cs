using System;
using System.Configuration;
using WebSite.Components;

namespace Aafp.Also.Web
{
    public class ApplicationConfig
    {
        private static string environment => ConfigurationManager.AppSettings["Aafp.Environment"];

        public static string DatabaseConnectionString
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return Environment.GetEnvironmentVariable("DevNetForumDatabaseConnection", EnvironmentVariableTarget.Machine);
                    case "Testing":
                        return Environment.GetEnvironmentVariable("TestingNetForumDatabaseConnection", EnvironmentVariableTarget.Machine);
                    case "Production":
                        return Environment.GetEnvironmentVariable("ProductionNetForumDatabaseConnection", EnvironmentVariableTarget.Machine);
                    default:
                        return Environment.GetEnvironmentVariable("DevNetForumDatabaseConnection", EnvironmentVariableTarget.Machine);
                }
            }
        }

        public static string BaseUrl
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return Environment.GetEnvironmentVariable("DevBaseUrl", EnvironmentVariableTarget.Machine);
                    case "Testing":
                        return Environment.GetEnvironmentVariable("TestingBaseUrl", EnvironmentVariableTarget.Machine);
                    case "Production":
                        return Environment.GetEnvironmentVariable("BaseUrl", EnvironmentVariableTarget.Machine);
                    default:
                        return Environment.GetEnvironmentVariable("DevBaseUrl", EnvironmentVariableTarget.Machine);
                }
            }
        }

        public static string AuthenticationUrl
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return Environment.GetEnvironmentVariable("DevAuthUrl", EnvironmentVariableTarget.Machine);
                    case "Testing":
                        return Environment.GetEnvironmentVariable("TestingAuthUrl", EnvironmentVariableTarget.Machine);
                    case "Production":
                        return Environment.GetEnvironmentVariable("AuthUrl", EnvironmentVariableTarget.Machine);
                    default:
                        return Environment.GetEnvironmentVariable("DevAuthUrl", EnvironmentVariableTarget.Machine);
                }
            }
        }

        public static string BaseStylesUrl
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return Environment.GetEnvironmentVariable("DevBaseStylesUrl", EnvironmentVariableTarget.Machine);
                    case "Testing":
                        return Environment.GetEnvironmentVariable("TestingBaseStylesUrl", EnvironmentVariableTarget.Machine);
                    case "Production":
                        return Environment.GetEnvironmentVariable("BaseStylesUrl", EnvironmentVariableTarget.Machine);
                    default:
                        return Environment.GetEnvironmentVariable("DevBaseStylesUrl", EnvironmentVariableTarget.Machine);
                }
            }
        }

        public static string BaseJsUrl
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return Environment.GetEnvironmentVariable("DevBaseJsUrl", EnvironmentVariableTarget.Machine);
                    case "Testing":
                        return Environment.GetEnvironmentVariable("TestingBaseJsUrl", EnvironmentVariableTarget.Machine);
                    case "Production":
                        return Environment.GetEnvironmentVariable("BaseJsUrl", EnvironmentVariableTarget.Machine);
                    default:
                        return Environment.GetEnvironmentVariable("DevBaseJsUrl", EnvironmentVariableTarget.Machine);
                }
            }
        }

        public static string BaseImagesUrl
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return Environment.GetEnvironmentVariable("DevBaseImagesUrl", EnvironmentVariableTarget.Machine);
                    case "Testing":
                        return Environment.GetEnvironmentVariable("TestingBaseImagesUrl", EnvironmentVariableTarget.Machine);
                    case "Production":
                        return Environment.GetEnvironmentVariable("BaseImagesUrl", EnvironmentVariableTarget.Machine);
                    default:
                        return Environment.GetEnvironmentVariable("DevBaseImagesUrl", EnvironmentVariableTarget.Machine);
                }
            }
        }

        public static string AdobeAnalyticsUrl
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return $"{BaseJsUrl}/omniture/aafp-dev-omniture.js";
                    case "Testing":
                        return $"{BaseJsUrl}/omniture/aafp-dev-omniture.js";
                    case "Production":
                        return $"{BaseJsUrl}/omniture/aafp-prod-omniture.js";
                    default:
                        return $"{BaseJsUrl}/omniture/aafp-dev-omniture.js";
                }
            }
        }

        public static string Header
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return SyndicationHelper.GetSyndicatedHtml($"{BaseJsUrl}/simple/header.html");
                    case "Testing":
                        return SyndicationHelper.GetSyndicatedHtml($"{BaseJsUrl}/simple/header.html");
                    case "Production":
                        return SyndicationHelper.GetSyndicatedHtml($"{BaseJsUrl}/simple/header.html");
                    default:
                        return SyndicationHelper.GetSyndicatedHtml($"{BaseJsUrl}/simple/header.html");
                }
            }
        }

        public static string Footer
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return SyndicationHelper.GetSyndicatedHtml($"{BaseJsUrl}/simple/footer.html");
                    case "Testing":
                        return SyndicationHelper.GetSyndicatedHtml($"{BaseJsUrl}/simple/footer.html");
                    case "Production":
                        return SyndicationHelper.GetSyndicatedHtml($"{BaseJsUrl}/simple/footer.html");
                    default:
                        return SyndicationHelper.GetSyndicatedHtml($"{BaseJsUrl}/simple/footer.html");
                }
            }
        }

        public static string AlsoServiceUrl
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return $"{BaseUrl}/also-api/";
                    case "Testing":
                        return $"{BaseUrl}/also-api/";
                    case "Production":
                        return $"{BaseUrl}/also-api/";
                    default:
                        return $"{BaseUrl}/also-api/";
                }
            }
        }

        public static string AlsoUploadDirectory
        {
            get { return @"\\gordon\ALSO\"; }
        }

        public static bool ShowStaffTools
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return true;
                    case "Testing":
                        return true;
                    case "Production":
                        return false;
                    default:
                        return false;
                }
            }
        }

        public static string JQueryCdn => "https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js";

        public static string JQueryUiCdn => "https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js";

        public static string JQueryUiCssCdn => "https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css";

        public static string BootstrapCdn22 => "https://maxcdn.bootstrapcdn.com/twitter-bootstrap/2.2.2/css/bootstrap-combined.min.css";

        public static string DiscountUrl => $"{AlsoServiceUrl}discount/create/";
        
    }
}