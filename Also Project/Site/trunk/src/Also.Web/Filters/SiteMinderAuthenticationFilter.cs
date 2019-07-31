using System.Collections.Generic;
using System.Web;

namespace Aafp.Also.Web.Filters
{
    public class SiteMinderAuthenticationFilter
    {
        public static string GetUserName(HttpRequest request)
        {
            return IsTestEnvironment() ? GetTestUserName() : request.Headers.Get("SM_USER");
        }

        public static bool IsTestEnvironment()
        {
#if (DEBUG)
            var machineNames = new List<string>();
            machineNames.Add("CHADDESKTOP");
            machineNames.Add("3SQHZQ1");  // Stephanie Chapel
            machineNames.Add("JWALKER-VM");
            machineNames.Add("NMOSHER-DESKTOP"); // Nick Mosher
            machineNames.Add("NMOSHER-LAPTOP"); // Nick Mosher
            machineNames.Add("MMATILE"); // Megan Matile
            machineNames.Add("7Y7ZXQ1"); // Carl Lehman
            machineNames.Add("JBRUTUS"); //Jaime Brown
            machineNames.Add("KEVINMOTT"); // Kevin Mott
            machineNames.Add("WOODY-MACBOOKPRO"); // Jason Wood
            machineNames.Add("WOODYVM");          // Jason Wood
            machineNames.Add("MEYSENBURG-E");          // Jason Wood            

            foreach (var name in machineNames)
            {
                if (System.Environment.MachineName == name)
                    return true;
            }
#endif

            return false;
        }

        public static string GetTestUserName()
        {
            var userName = string.Empty;

#if (DEBUG)
            if (IsTestEnvironment())
            {
                switch (System.Environment.MachineName)
                {
                    case "JWALKER-VM":
                        userName = "9071109";
                        break;
                    case "MMATILE":
                        userName = "8152494";
                        break;
                    case "NMOSHER-LAPTOP":
                        userName = "nmosher";
                        break;
                    case "NMOSHER-DESKTOP":
                        userName = "8152494";
                        break;
                    case "chooper-PC":
                        userName = "8133333";
                        break;
                    case "7Y7ZXQ1":
                        userName = "8140752";
                        break;
                    case "KEVINMOTT":
                        userName = "8152494";
                        break;
                    case "WOODY-MACBOOKPRO":
                        userName = "8152494";
                        break;
                    case "WOODYVM":
                        userName = "8152494";
                        break;
                    default:
                        userName = "8133333";
                        break;
                }
            }
#endif

            return userName;
        }
    }
}