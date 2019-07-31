using System;
using System.Configuration;

namespace Aafp.Also.Api
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

        public static string ALSOInstructorProduct = "mdse13070";

        //public static string ALSOProviderProduct = "mdse13100";
        public static string ALSOProviderProduct = "mdse13076";

        public static string BLSOProviderProduct = "mdse13076";

        public static Guid ALSOProviderBLSORevenueKey = new Guid("64E77479-F5D1-4E5E-B304-D9C8CB008888");

        public static Guid ALSOInstructorRevenueKey = new Guid("72AA5C84-771F-4264-95DD-30D938BFFB78");

        public static Guid CompanyKey = new Guid("C625663E-62C4-48C9-995A-081F3597B04A");

        public static Guid PriceTypeKey = new Guid("52405A5C-6393-460D-BD35-2BBB2AA79E33");

        public static Guid ALSOProviderPriceKey = new Guid("A7691160-269D-4034-B1CF-361515BC24F4");

        public static Guid ALSOInstructorPriceKey = new Guid("B0269AE8-9B15-4E87-9290-A898FA55495C");

        //public static Guid BLSOProviderPriceKey = new Guid("955C3848-99A7-43AA-B51A-701FED1C8C1A");
        public static Guid BLSOProviderPriceKey = new Guid("A7691160-269D-4034-B1CF-361515BC24F4");
    }
}