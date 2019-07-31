$(document).ready(function () {
    $(".session__load-more").click(function () {
        $(".session__load-more").hide();
    });

    $("tr[id$='_currentRegistrationTableRow']").slice(10, $("tr[id$='_currentRegistrationTableRow']").length).hide();

    $("#moreResults").click(function(event) {
        event.preventDefault();
        $("tr[id$='_currentRegistrationTableRow']").show();
    });

    var pageTitle = $("h1").text();

    s.pageName = "events:registration:summary";
    s.referrer = document.referrer;

    s.linkTrackVars = "prop1";
    s.linkTrackEvents = s.events;
    s.prop1 = pageTitle;

    s.tl(this, 'o', s.events);

    /************* DO NOT ALTER ANYTHING BELOW THIS LINE ! **************/
    var s_code = s.t(); if (s_code) document.write(s_code);  //
});