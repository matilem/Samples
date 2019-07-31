using System;
using System.Configuration;

namespace Aafp.MyCme.Web
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
                        return Environment.GetEnvironmentVariable("NetForumDatabaseConnection", EnvironmentVariableTarget.Machine);
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

        public static bool ShowSorting
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return true;
                    case "Testing":
                        return false;
                    case "Production":
                        return false;
                    default:
                        return false;
                }
            }
        }

        public static string CmeServiceUrl => $"{BaseUrl}/cme-api/";

        public static string JQueryCdn => "https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js";

        public static string AssessmentUrl => $"{BaseUrl}/Assessment/Listing/";
    }
}