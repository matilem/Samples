using System;
using System.Configuration;

namespace Aafp.Cme.Api
{
    public static class ApplicationConfig
    {
        private static string environment => ConfigurationManager.AppSettings["Aafp.Environment"];

        public static string DatabaseConnectionString
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return "server=nf2011-db.webad.aafp.org;database=netforum;uid=netForumAdminUser;pwd=iW3bBegone;Application Name=AAFP";
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

        public static string AemBaseUrl
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return Environment.GetEnvironmentVariable("DevAemBaseUrl", EnvironmentVariableTarget.Machine);
                    case "Testing":
                        return Environment.GetEnvironmentVariable("TestingAemBaseUrl", EnvironmentVariableTarget.Machine);
                    case "Production":
                        return Environment.GetEnvironmentVariable("AemBaseUrl", EnvironmentVariableTarget.Machine);
                    default:
                        return Environment.GetEnvironmentVariable("DevAemBaseUrl", EnvironmentVariableTarget.Machine);
                }
            }
        }

        public static string LmsBaseUrl
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return "https://aafpint.dlcdev.com/";
                    case "Testing":
                        return "https://aafpint.dlcdev.com/";
                    case "Production":
                        return "https://aafpint.dlcdev.com/";
                    default:
                        return "https://aafpint.dlcdev.com/";
                }
            }
        }

        public static int PrescribedCreditsRequired => 75;

        public static int GroupCreditsRequired => 25;

        public static int FloridaChapterCreditsRequired => 12;

        public static int MarylandChapterCreditsRequired => 6;

        public static int TotalCreditsRequired => 150;
    }
}