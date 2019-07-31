using System.Configuration;

namespace AdminTool.Api
{
    public class ApplicationConfig
    {
        private static string environment => ConfigurationManager.AppSettings["Environment"];

        public static string ADLocation = "Garfield.tpa.healtheval.com";

        public static string ADDC = "DC=TPA,DC=healtheval,DC=com";

        public static string HydraADGroup = "db_HydraUsers";

        public static string DatabaseConnectionString
        {
            get
            {
                switch (environment)
                {
                    case "Development":
                        return "server=FraMauro\\Delta;database=DataPipe_PROD;uid=DataPipeAppUser;pwd=XeBaf2PEZe";
                    case "UAT":
                        return "server=Vesconte\\Tango;database=DataPipe_PROD;uid=DataPipeAppUser;pwd=XeBaf2PEZe";
                    case "Production":
                        return "server=Mercator\\Alpha;database=DataPipe_PROD;uid=DataPipeAppUser;pwd=XeBaf2PEZe";
                    default:
                        return "server=FraMauro\\Delta;database=DataPipe_PROD;uid=DataPipeAppUser;pwd=XeBaf2PEZe";
                }
            }
        }
    }
}