using System;
using System.Configuration;

namespace Aafp.Events.Api.ApplicationConfig
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
            var typeName = $"Aafp.Events.Api.ApplicationConfig.{environment}ApplicationConfig";
            var type = Type.GetType($"{typeName}, Aafp.Events.Api", true);
            Settings = (IApplicationConfig)Activator.CreateInstance(type);
        }
    }
}