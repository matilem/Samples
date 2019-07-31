using log4net;

namespace Aafp.Cme.Api.Helpers
{
    public class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger("CMEService");

        public static void LogError(string message)
        {
            Log.Error(message);
        }

        public static void LogInfo(string message)
        {
            Log.Info(message);
        }
    }
}