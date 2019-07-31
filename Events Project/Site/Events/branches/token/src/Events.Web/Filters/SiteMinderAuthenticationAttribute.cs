using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Aafp.Events.Web.Filters
{
    public class SiteMinderAuthenticationAttribute : FilterAttribute, IAuthenticationFilter
    {
        public string[] Roles { get; set; }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            string siteMinderHeader = "SM_USER";
            string username = GetUserName(HttpContext.Current.Request, siteMinderHeader);

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

        public static string GetUserName(HttpRequest request, string header)
        {
            return IsTestEnvironment() ? GetTestUserName() : request.Headers.Get(header);
        }

        public static bool IsTestEnvironment()
        {
#if (DEBUG)
            var machineNames = new List<string>();
            machineNames.Add("CHADDESKTOP");
            machineNames.Add("TEEKOONYEOW");
            machineNames.Add("3SQHZQ1");  // Stephanie Chapel
            machineNames.Add("JWALKER-PC");
            machineNames.Add("NMOSHER-DESKTOP"); // Nick Mosher
            machineNames.Add("NMOSHER-LAPTOP"); // Nick Mosher
            machineNames.Add("MMATILE"); // Megan Matile
            machineNames.Add("WINTERM6"); // Luke Dodge

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
                    case "MMATILE":
                        userName = "mmatile@aafp.org";
                        break;
                    case "TEEKOONYEOW":
                        userName = "9016613";
                        break;
                    case "NMOSHER-LAPTOP":
                        userName = "8098155";
                        break;
                    case "chooper-PC":
                        userName = "8133333";
                        break;
                    default:
                        userName = "8590716";
                        break;
                }
            }
#endif

            return userName;
        }
    }
}