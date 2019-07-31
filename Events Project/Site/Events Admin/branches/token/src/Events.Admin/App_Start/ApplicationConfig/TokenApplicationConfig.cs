using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Events.Admin.ApplicationConfig
{
    public class TokenApplicationConfig : IApplicationConfig
    {
        public string BaseUrl => "http://token.ams.aafp.org/";

        public string ConnectionString => "server=10.10.10.32;database=netForum;uid=netForumAdminUser;pwd=Sn1k3rz;Application Name=AAFP";

        public string SyndicationHeaderUrl => @"http://js.aafp.net/dev/header.html";

        public string SyndicationFooterUrl => @"http://js.aafp.net/dev/footer.html";

        public string SyndicationCssBaseUrl => @"http://css.aafp.net/dev/";

        public string SyndicationJsBaseUrl => @"http://js.aafp.net/dev/";

        public string SyndicationImageBaseUrl => @"http://img.aafp.net/dev/";

        public string AuthenticationLoginUrl => "http://webx-dev.webad.aafp.org/cgi-bin/lg.pl";

        public string ApplicationUrl => $"{BaseUrl}events-admin/";

        public string EventServiceUrl => $"{BaseUrl}events-api/";

        public string ReportServerUrl => "http://nf2011-db.webad.aafp.org/ReportServer?/NetforumToken/";
    }
}