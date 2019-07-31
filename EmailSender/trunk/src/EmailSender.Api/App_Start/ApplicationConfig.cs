using System;
using System.Configuration;

namespace Aafp.EmailSender.Api
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

        public static string CustomersApi => $"{BaseUrl}/customers-api/";

        public static string SmtpPort => Environment.GetEnvironmentVariable("SmtpPort", EnvironmentVariableTarget.Machine);

        public static string SmtpHost => Environment.GetEnvironmentVariable("SmtpHost", EnvironmentVariableTarget.Machine);

        public static bool UseTestEmail
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
    }
}