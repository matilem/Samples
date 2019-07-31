using System;
using System.Configuration;

namespace Aafp.Events.Web.ApplicationConfig
{
    public class ApplicationConfigManager
    {
        static ApplicationConfigManager()
        {
            LoadSettings();
        }

        public static IApplicationConfig Settings { get; private set; }

        private static void LoadSettings()
        {
            var environment = ConfigurationManager.AppSettings["Aafp.Environment"];
            var typeName = $"Aafp.Events.Web.ApplicationConfig.{environment}ApplicationConfig";
            var type = Type.GetType($"{typeName}, Aafp.Events.Web", true);
            Settings = (IApplicationConfig)Activator.CreateInstance(type);
        }
    }
}