(function($, window, document) {

    var totalCheckboxes = $('input:checkbox').length;

    analytics.trackLiveCMEReportingPageView(totalCheckboxes);

    var mobileVisibility = function () {
        var detailsAccordionControl = $("#accordionControl").get(0);
        return window.getComputedStyle(detailsAccordionControl).display;
    };

    var isDirty = function () {
        var allCheckBoxes = $("input[name*='sessionCheckBox']");

        for (var i = 0; i < allCheckBoxes.length; i++) {
            if (allCheckBoxes[i].checked) {
                return true;
            }
        }

        return false;
    };

    $("#alertSuccess").toggle(document.URL.indexOf("success=true") !== -1);
    $("#alertFalse").toggle(document.URL.indexOf("success=false") !== -1);

    $(".account-portal").click(function () {
        if (mobileVisibility() !== "none") {
            if ($(".-nav-active").length <= 0) {
                $(".account-portal__welcome")[0].innerText = "close";
                $(".account-portal__greeting").addClass("-nav-active");
                $(".account-portal__links").addClass("-is-active");
            } else {
                $(".account-portal__welcome")[0].innerText = "Hi,";
                $(".account-portal__greeting").removeClass("-nav-active");
                $(".account-portal__links").removeClass("-is-active");
            }
        }
    });

    $("input[id$='-check']").click(function(event) {
        var dateDisplay = event.target.id.split("-")[0];
        var isChecked = event.target.checked;

        var allCheckBoxes = $("input[name*='sessionCheckBox']");

        for (var i = 0; i < allCheckBoxes.length; i++) {
            if (allCheckBoxes[i].dataset.date === dateDisplay) {
                if (isChecked) {
                    allCheckBoxes[i].checked = true;
                } else {
                    allCheckBoxes[i].checked = false;
                }
            }
        }
    });

    $("div[id$='-table__row']").click(function (event) {
        if (event.target.type !== "checkbox") {
            $(":checkbox", this).trigger("click");
        }
    });

    $("div[id$='-date-accordion']").click(function () {
        if (mobileVisibility() !== "none") {
            for (var i = 0; i < this.children.length; i++) {
                if (this.children[i].className === "session-table__drawer-control") {
                    var clickedDate = this.dataset.date;
                    if (this.children[i].innerText === "-") {
                        this.children[i].innerText = "+";
                        $("#" + clickedDate + "-sessions").slideToggle();
                    } else {
                        this.children[i].innerText = "-";
                        $("#" + clickedDate + "-sessions").slideToggle();
                    }
                }
            }
        }
    });

    $("#details-accordion").click(function () {
        if (mobileVisibility() !== "none") {
            if ($(".callout-box__drawer-control")[0].innerText === "-") {
                $(".callout-box__drawer-control")[0].innerText = "+";
                $("#details-content").slideToggle();
            } else {
                $(".callout-box__drawer-control")[0].innerText = "-";
                $("#details-content").slideToggle();
            }
        }
    });

    $("button[id$='-reportButton']").click(function () {
        var activityNumber = $("#ActivityNumber").val();
        reportCredits(activityNumber);
    });

    $(window).bind("beforeunload", function (e) {
        if (!isDirty()) {
            return undefined;
        }

        var confirmationMessage = "Do you want to leave this site? "
            + "Changes you made may not be saved.";

        (e || window.event).returnValue = confirmationMessage;
        return confirmationMessage;
    });
}(window.jQuery, window, document));

function reportCredits(activityNumber) {
    var allCheckBoxes = $("input[name*='sessionCheckBox']");
    var sessionKeys = [];
    for (var i = 0; i < allCheckBoxes.length; i++) {
        if (allCheckBoxes[i].checked) {
            var sessionKey = allCheckBoxes[i].id;
            sessionKeys.push(sessionKey);
        }
    }

    var sessionsAndActivity = {
        activityNumber: activityNumber,
        sessionKeys: sessionKeys
    };

    analytics.addCmeLiveReportingTracking(sessionKeys.length);

    if (sessionKeys.length >= 1) {

        $.ajax({
            type: "POST",
            url: "../submit",
            data: JSON.stringify(sessionsAndActivity),
            contentType: "application/json; charset=utf-8"
        }).done(function (result) {
            $(window).unbind("beforeunload");
            if (result.HasError) {
                window.location.replace(activityNumber + "?success=false");
            } else {
                window.location.replace(activityNumber + "?success=true");
            }
        }).fail(function (result) {
            $(window).unbind("beforeunload");
            window.location.replace(activityNumber + "?success=false");
        });
    } else {
        alert("Please select a session to report.");
    }
}