﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aafp.Cme.Api.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    #line 7 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
    using Aafp.Cme.Api;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class CmeStatsTemplate : RazorGenerator.Templating.RazorTemplateBase
    {
#line hidden

        #line 9 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"

    public dynamic Model { get; set; }

        #line default
        #line hidden

        public override void Execute()
        {
WriteLiteral("\r\n");



WriteLiteral("\r\n\r\n");


WriteLiteral("\r\n");


WriteLiteral("\r\n\r\n<div id=\"cmeNumericStatsDiv\">\r\n    <div class=\"mini-dashboard\">\r\n        <h1 " +
"class=\"mini-dashboard__title mini-dashboard--hidable\"><a href=\"");


            
            #line 15 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                                                                       Write($"{ApplicationConfig.BaseUrl}/my-cme/");

            
            #line default
            #line hidden
WriteLiteral("\">My Quizzes and Courses</a></h1>\r\n        <div class=\"mini-dashboard__wrapper\">\r" +
"\n            <div class=\"mini-dashboard__data dashboard-numeric\">\r\n             " +
"   <span class=\"dashboard-numeric__numeral\">");


            
            #line 18 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                                                    Write(Model.CreditAvailableInfo.CreditsExpiring);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n                <span class=\"dashboard-numeric__units\">credits</span>\r\n " +
"               <a id=\"js-stats-all\" href=\"");


            
            #line 20 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                                       Write($"{ApplicationConfig.BaseUrl}/my-cme/#all");

            
            #line default
            #line hidden
WriteLiteral("\" class=\"dashboard-numeric__desc\">expiring soon</a>\r\n            </div>\r\n        " +
"    <div class=\"mini-dashboard__data dashboard-numeric\">\r\n                <span " +
"class=\"dashboard-numeric__numeral\">");


            
            #line 23 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                                                    Write(Model.CreditAvailableInfo.CreditsPurchased);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n                <span class=\"dashboard-numeric__units\">credits of</span>" +
"\r\n                <a id=\"js-stats-purchased\" href=\"");


            
            #line 25 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                                             Write($"{ApplicationConfig.BaseUrl}/my-cme/#purchased");

            
            #line default
            #line hidden
WriteLiteral("\" class=\"dashboard-numeric__desc\">purchases available</a>\r\n            </div>\r\n  " +
"          <div class=\"mini-dashboard__data dashboard-numeric mini-dashboard--hid" +
"able\">\r\n                <span class=\"dashboard-numeric__numeral\">");


            
            #line 28 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                                                    Write(Model.CreditAvailableInfo.QuizzesAvailable);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n                <span class=\"dashboard-numeric__units\">credits of</span>" +
"\r\n                <a id=\"js-stats-subscriptions\" href=\"");


            
            #line 30 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                                                 Write($"{ApplicationConfig.BaseUrl}/my-cme/#subscriptions");

            
            #line default
            #line hidden
WriteLiteral("\" class=\"dashboard-numeric__desc\">quizzes available</a>\r\n            </div>\r\n    " +
"    </div>\r\n    </div>\r\n</div>\r\n<div id=\"reElectionStatusDiv\">\r\n    <div class=\"" +
"mini-dashboard mini-dashboard--mobile-hide\">\r\n");


            
            #line 37 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
         if (Model.ReElectionInfo.IsMember)
        {

            
            #line default
            #line hidden
WriteLiteral("            <h1 class=\"mini-dashboard__title\">My Membership Status</h1>\r\n");


            
            #line 40 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
        }
        else
        {

            
            #line default
            #line hidden
WriteLiteral("            <h1 class=\"mini-dashboard__title\">CME Information</h1>\r\n");


            
            #line 44 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        <div class=\"mini-dashboard__wrapper\">\r\n");


            
            #line 46 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
             if (Model.ReElectionInfo.IsMember)
            {

            
            #line default
            #line hidden
WriteLiteral("                <div class=\"mini-dashboard__data dashboard-alpha\">\r\n             " +
"       <a class=\"dashboard-alpha__label\" id=\"js-re-election-status\" href=\"");


            
            #line 49 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                                                                                   Write($"{ApplicationConfig.BaseUrl}/cme/reporting");

            
            #line default
            #line hidden
WriteLiteral("\">Re-Election status</a>\r\n                    <span class=\"dashboard-alpha__value" +
" dashboard-alpha__value--");


            
            #line 50 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                                                                           Write(Model.ReElectionInfo.StatusDisplay);

            
            #line default
            #line hidden
WriteLiteral("\">");


            
            #line 50 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                                                                                                                Write(Model.ReElectionInfo.Message);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n                </div>\r\n");


            
            #line 52 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            <div class=\"mini-dashboard__data dashboard-alpha\">\r\n");


            
            #line 54 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                 if (Model.ReElectionInfo.IsMember)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <a href=\"");


            
            #line 56 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                         Write($"{ApplicationConfig.BaseUrl}/cme/reporting/claimcredit.aspx");

            
            #line default
            #line hidden
WriteLiteral("\" class=\"dashboard-alpha__button button button-secondary\" id=\"js-report-cme\">Repo" +
"rt CME</a>\r\n");



WriteLiteral("                    <a href=\"");


            
            #line 57 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                         Write($"{ApplicationConfig.BaseUrl}/cme/reporting/transcript.aspx");

            
            #line default
            #line hidden
WriteLiteral("\" class=\"dashboard-alpha__button button button-secondary\" id=\"js-view-transcript\"" +
">View Transcript</a>\r\n");


            
            #line 58 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                }
                else
                {

            
            #line default
            #line hidden
WriteLiteral("                    <a href=\"");


            
            #line 61 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                         Write($"{ApplicationConfig.BaseUrl}/cme/reporting/nonmemberparticipation.aspx");

            
            #line default
            #line hidden
WriteLiteral("\" class=\"dashboard-alpha__button large button button-secondary\" id=\"js-letters-of" +
"-participation\">Letters of Participation</a>\r\n");


            
            #line 62 "C:\svn\Aafp Apis\Cme\trunk\src\Cme.Api\Templates\CmeStatsTemplate.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<script>\r\n    $(document)" +
".ready(function() {\r\n\r\n        $(\"#js-stats-all\").click(function () {\r\n         " +
"   clickCMEEvent(\"my cme - expiring soon\");\r\n        });\r\n\r\n        $(\"#js-stats" +
"-purchased\").click(function () {\r\n            clickCMEEvent(\"my cme - purchases " +
"available\");\r\n        });\r\n\r\n        $(\"#js-stats-subscriptions\").click(function" +
" () {\r\n            clickCMEEvent(\"my cme - quizzes available\");\r\n        });\r\n\r\n" +
"        $(\"#js-report-cme\").click(function () {\r\n            clickCMEEvent(\"my c" +
"me – report CME\");\r\n        });\r\n\r\n        $(\"#js-view-transcript\").click(functi" +
"on () {\r\n            clickCMEEvent(\"my cme – view transcript\");\r\n        });\r\n\r\n" +
"        /*$(\"#js-letters-of-participation\").unbind(\"click\").click(function () {\r" +
"\n            clickCMEEvent(\"my cme – letters of participation\");\r\n        });*/\r" +
"\n\r\n        $(\"#js-letters-of-participation, #js-report-cme, #js-view-transcript\"" +
").dblclick(function (event) {\r\n            event.preventDefault();\r\n        });\r" +
"\n\r\n        $(\"#js-re-election-status\").click(function () {\r\n            clickCME" +
"Event(\"my cme - reelection status\");\r\n        });\r\n    });\r\n\r\n    function click" +
"CMEEvent(title) {\r\n        try {\r\n            if (title && \"\" != title) {\r\n     " +
"           var s = s_gi(s_account);\r\n                s.eVar26 = title;\r\n        " +
"        s.events = \'event26\';\r\n                //Track eVar & Event\r\n           " +
"     s.linkTrackVars = \"events,eVar26\"\r\n                s.linkTrackEvents = \"eve" +
"nt26\"\r\n                s.tl(this, \'o\', s.pageName + \":\" + s.eVar26);\r\n          " +
"  }\r\n        } catch (e) {\r\n            //console.log(\"Error setting SC: \" + e);" +
"\r\n        }\r\n    }\r\n</script>");


        }
    }
}
#pragma warning restore 1591
