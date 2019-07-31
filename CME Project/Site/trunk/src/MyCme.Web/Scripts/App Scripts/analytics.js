var analytics = {
    trackCMEDashboardPageView: function () {
        var pageTitle = $("#tabTitle").text();
        
        var titleKicker = $(".title-kicker").text(); //My Quizzes and Courses
        s.events = "";
        s.eVar26 = "";
        s.pageName = "myaccount:myquizzesandcourses:" + pageTitle.toLowerCase();
        s.referrer = document.referrer;

        s.prop1 = titleKicker;
        s.prop3 = "main";
        s.prop17 = "restricted";
        s.prop18 = "web";
        s.prop21 = "myaccount:myquizzesandcourses";
        s.prop22 = s.pageName;
        s.prop40 = "cme";
        s.prop41 = "serve members";
        s.prop42 = "nf";

        /************* DO NOT ALTER ANYTHING BELOW THIS LINE ! **************/
        var s_code = s.t(); if (s_code) document.write(s_code);  //
    }
    ,
    addCmeReportingTracking: function () {
        $(".__get-credit").each(function () {
            var href = $(this).attr("href");
            if (href && href.indexOf("/cme/reporting/") !== -1) {
                $(this).click(function() {
                    s.events = "event36";
                    s.linkTrackVars = 'events';
                    s.linkTrackEvents = s.events;
                    s.tl(this, 'o', s.pageName);
                });
            }
        });
    }
    ,
    trackLiveCMEReportingPageView: function (totalCheckboxes) {
        var memberIdSC = getMemberIdEnCodedWithCookie();
        s.events = "event36,event37";
        s.eVar2 = "login";
        s.eVar3 = "Direct Influence Pages";
        s.eVar14 = memberIdSC;
        s.eVar17 = "D=pageName";
        s.evar25 = $(".page-titles__title").text();
        s.eVar37 = totalCheckboxes;
        s.evar38 = "Live Course Sessions";
        s.eVar41 = "D=c41";
        s.eVar42 = "D=c40";
        s.eVar43 = "D=pageName";
        s.pageName = "myaccount:reportcme:sessionresults";
        s.referrer = document.referrer;

        s.prop1 = "report live cme sessions";
        s.prop2 = "login"
        s.prop14 = memberIdSC;
        s.prop3 = "main";
        s.prop17 = "restricted";
        s.prop18 = "web";
        s.prop21 = "myaccount:reportcme:sessionresults";
        s.prop22 = s.pageName;
        s.prop36 = "Page URL";
        s.prop40 = "membership";
        s.prop41 = "serve members";
        s.prop42 = "nf";

        /************* DO NOT ALTER ANYTHING BELOW THIS LINE ! **************/
        var s_code = s.t(); if (s_code) document.write(s_code);  //
    }
    ,
    addCmeLiveReportingTracking: function (sessionCount) {
        s.events = "event38";
        //s.eVar37 = sessionCount;
        s.eVar38 = "Live Course Sessions";
        s.linkTrackVars = 'events,eVar37,eVar38';
        s.linkTrackEvents = s.events;
        s.tl(this, 'o', s.events);
    },
};