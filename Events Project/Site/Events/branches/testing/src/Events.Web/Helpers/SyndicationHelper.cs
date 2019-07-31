using System.IO;
using System.Net;
using Aafp.Events.Web.ApplicationConfig;

namespace Aafp.Events.Web.Helpers
{
    public class SyndicationHelper
    {
        public static string GetSyndicationHeader()
        {
            return GetHtml(ApplicationConfigManager.Settings.SyndicationHeaderUrl);
        }

        public static string GetSyndicationFooter()
        {
            return GetHtml(ApplicationConfigManager.Settings.SyndicationFooterUrl);
        }

        public static string GetSyndicationNav(string resource)
        {
            return GetHtml(string.Concat(ApplicationConfigManager.Settings.SyndicationJsBaseUrl, resource));
        }

        public static string GetCssSyndicationLink(string resource)
        {
            return string.Concat(ApplicationConfigManager.Settings.SyndicationCssBaseUrl, resource);
        }

        public static string GetJsSyndicationLink(string resource)
        {
            return string.Concat(ApplicationConfigManager.Settings.SyndicationJsBaseUrl, resource);
        }

        public static string GetImageSyndicationLink(string resource)
        {
            return string.Concat(ApplicationConfigManager.Settings.SyndicationImageBaseUrl, resource);
        }

        private static string GetHtml(string resource)
        {
            string result;
            WebResponse response;
            WebRequest request = HttpWebRequest.Create(resource);
            response = request.GetResponse();
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }

            return result;
        }
    }
}