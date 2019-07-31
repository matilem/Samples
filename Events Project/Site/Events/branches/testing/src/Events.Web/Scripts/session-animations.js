var emptyGuid = "00000000-0000-0000-0000-000000000000";

var RequiredSessionsModule = (function () {
    function setWarningDivText(warningDiv, requiredSessionInput) {
        var requiredSessionCode = requiredSessionInput.data("code");
        var requiredSessionTitle = requiredSessionInput.data("title");
        warningDiv.text(requiredSessionTitle + " (" + requiredSessionCode + ") is required to attend this session.");
    }

    function showPrerequisiteWarning(sessionInput, masterSessionKey) {
        var sessionKey = sessionInput.data("key");
        var requiredSessionKey = sessionInput.data("reqsession");
        var sessionSelected = Number(sessionInput.val()) > 0 || sessionInput.is(":checked");

        if (!sessionSelected) {
            return true;
        }

        if (requiredSessionKey !== masterSessionKey) {
            return true;
        }

        var sessionWarningDiv = $("#" + sessionKey + "_requiredSessionDiv");
        var requiredSessionInput = $("#" + requiredSessionKey + "_sessionCheckBox");
        var sessionTableRow = $("#" + masterSessionKey + "_sessionTableRow");

        setWarningDivText(sessionWarningDiv, requiredSessionInput);
        sessionWarningDiv.show();
        sessionTableRow.addClass("table--state-requiredSession");
    }

    function showPrerequisiteWarnings(sessionKey) {
        $("input[id$='_sessionCheckBox']").each(function () {
            showPrerequisiteWarning($(this), sessionKey);
        });
        $("select[id$='_sessionQuantityDropDown']").each(function () {
            showPrerequisiteWarning($(this), sessionKey);
        });
    }

    function hidePrerequisiteWarnings(sessionKey) {
        $("div[id$='_requiredSessionDiv']").each(function () {
            var requiredSessionWarningDiv = $(this);
            if (requiredSessionWarningDiv.data("reqsession") === sessionKey) {
                requiredSessionWarningDiv.hide();
            }
        });
    }

    function confirmRequiredSessions(sessionInput) {
        var sessionKey = sessionInput.data("key");
        var requiredSessionKey = sessionInput.data("reqsession");
        var sessionSelected = Number(sessionInput.val()) > 0 || sessionInput.is(":checked");

        if (sessionSelected) {
            hidePrerequisiteWarnings(sessionKey);
        }
        else {
            showPrerequisiteWarnings(sessionKey);
        }

        if (requiredSessionKey === emptyGuid) {
            // No Required Session
            return;
        }

        var sessionWarningDiv = $("#" + sessionKey + "_requiredSessionDiv");
        // TODO: Handle required dropdown inputs
        var requiredSessionInput = $("#" + requiredSessionKey + "_sessionCheckBox");
        var requiredSessionSelected = requiredSessionInput.is(":checked") || Number(requiredSessionInput.val()) > 0;
        var missingRequiredSession = !requiredSessionSelected && sessionSelected;
        var sessionTableRow = $("#" + sessionKey + "_sessionTableRow");

        setWarningDivText(sessionWarningDiv, requiredSessionInput);
        sessionWarningDiv.toggle(missingRequiredSession);
        sessionTableRow.toggleClass("table--state-requiredSession", missingRequiredSession);
    }

    return {
        confirmRequiredSessions: confirmRequiredSessions
    };
})();

$(document).ready(function () {
    $(".session__table-content-learn-more-click").click(function () {
        $(this).prevAll(".session__table-content-title").children(".session__table-content-details").slideToggle("slow");
        $(this).toggleClass("-state-hidden").next(".session__table-content-less-click").toggleClass("-state-hidden");
    });
    $(".session__table-content-less-click").click(function () {
        $(this).prevAll(".session__table-content-title").children(".session__table-content-details").slideToggle("slow");
        $(this).toggleClass("-state-hidden").prev(".session__table-content-learn-more-click").toggleClass("-state-hidden");
    });

    $(".session__table-content-title").click(function () {
        $(this).children(".session__table-content-details").slideToggle("slow");
        $(this).nextAll(".session__table-content-less-click").toggleClass("-state-hidden");
        $(this).nextAll(".session__table-content-learn-more-click").toggleClass("-state-hidden");
    });

    /** Highlight table rows when their checkbox is checked **/
    $(".session__add-label input[type=checkbox]").on("change", function () {
        $(this).parents(".session__table-content").toggleClass("-state-selected");
    }).filter(":checked").each(function () {
        $(this).parents(".session__table-content").addClass("-state-selected");
    });
    /** Highlight table rows when their select box value is greater than 0 **/
    $(".session__table-content-add select").on("change", function () {
        $(this).parents(".session__table-content").toggleClass("-state-selected", $(this).val() > 0);
    }).each(function () {
        $(this).parents(".session__table-content").toggleClass("-state-selected", $(this).val() > 0);
    });
});