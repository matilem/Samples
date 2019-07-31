using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Aafp.MyCme.Web.Filters
{
    public class SiteMinderAuthenticationFilter : FilterAttribute, IAuthenticationFilter
    {
        public string[] Roles { get; set; }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            string username = GetUserName(HttpContext.Current.Request);

            if (string.IsNullOrEmpty(username))
            {
                HttpContext.Current.User = null;
                HttpContext.Current.ApplicationInstance.Response.Redirect("http://www.aafp.org/internal/restricted-access.html");
            }
            else
            {
                GenericIdentity identity = new GenericIdentity(username, "SiteMinder");
                var roles = GetRoles();
                GenericPrincipal principal = new GenericPrincipal(identity, roles);
                filterContext.HttpContext.User = principal;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var isInRole = false;

            foreach (var role in Roles)
            {
                if (filterContext.HttpContext.User.IsInRole(role))
                    isInRole = true;
            }

            if (!isInRole)
            {
                HttpContext.Current.User = null;
                HttpContext.Current.ApplicationInstance.Response.Redirect("http://www.aafp.org/internal/restricted-access.html");
            }
        }

        private string[] GetRoles()
        {
            var rolesString = string.Empty;
            string[] roles = null;

            if (IsTestEnvironment())
            {
                roles = new[] { "customer" };
                Roles = roles;
            }
            else
            {
                var siteMinderRoleHeaders = HttpContext.Current.Request.Headers.AllKeys;
                var headers = siteMinderRoleHeaders.Where(x => x.EndsWith("ROLE", true, CultureInfo.CurrentCulture)).ToList();

                if (siteMinderRoleHeaders.Any())
                {
                    foreach (var header in headers)
                    {
                        var headerValue = HttpContext.Current.Request.Headers[header];

                        if (headerValue != "n/a")
                            rolesString = string.IsNullOrWhiteSpace(rolesString) ? headerValue : $"{rolesString}, {headerValue}";
                    }
                }

                if (!string.IsNullOrEmpty(rolesString))
                {
                    roles = rolesString.Split(',');
                }

                Roles = roles;
            }

            return roles;
        }

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
            machineNames.Add("JWALKER-PC");
            machineNames.Add("JWALKER-VM");
            machineNames.Add("NMOSHER-DESKTOP"); // Nick Mosher
            machineNames.Add("NMOSHER-LAPTOP"); // Nick Mosher
            machineNames.Add("MMATILE"); // Megan Matile
            machineNames.Add("7Y7ZXQ1"); // Carl Lehman
            machineNames.Add("JWESLEY-PC");

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
                    case "JWALKER-PC":
                        userName = "9071109";
                        break;
                    case "JWALKER-VM":
                        userName = "9071109";
                        break;
                    case "MMATILE":
                        userName = "9033381";
                        break;
                    case "NMOSHER-LAPTOP":
                        userName = "nmosher";
                        break;
                    case "chooper-PC":
                        userName = "8133333";
                        break;
                    case "7Y7ZXQ1":
                        userName = "8140752";
                        break;
                    case "JWESLEY-PC":
                        userName = "9216083"; /*"8070979";*/
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