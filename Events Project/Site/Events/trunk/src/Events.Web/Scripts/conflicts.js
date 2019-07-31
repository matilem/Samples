$(document).ready(function () {
    var registrationConflicts = true;
    var sessions = [];

    var addSession = function () {
        var sessionElement = $(this);
        var id = sessionElement.prop("id");
        var startTime = new Date(sessionElement.data("start-time"));
        var endTime = new Date(sessionElement.data("end-time"));
        var session = {
            id: id,
            start: startTime,
            end: endTime,
        };
        sessions.push(session);
    }

    $("input[id$='_sessionCheckBox']").each(addSession);
    $("select[id$='_sessionQuantityDropDown']").each(addSession);

    function updateConflicts() {
        registrationConflicts = false;

        sessions.forEach(function (session) {
            var start = session.start;
            var end = session.end;
            var id = session.id;
            var sessionElement = $("#" + id);
            var isSelected = sessionElement.prop("checked") || sessionElement.val() > 0;

            if (!isSelected) {
                sessionElement.parents(".session__table-content").removeClass("-red-selected");
                sessionElement.parents(".session__table-content").removeClass("-green-selected");
                return true;
            }

            var intersections = $.grep(sessions, function (s) { return (id != s.id) && ((start >= s.start && start <= s.end) || (s.start >= start && s.start <= end)); });
            var sessionConflicts = false;

            intersections.forEach(function (intersection) {
                var intersectionElement = $("#" + intersection.id);
                var isIntersectionSelected = intersectionElement.prop("checked") || intersectionElement.val() > 0;

                if (!isIntersectionSelected) {
                    return true;
                }

                sessionConflicts = true;
                intersectionElement.parents(".session__table-content").removeClass("-green-selected");
                intersectionElement.parents(".session__table-content").addClass("-red-selected");
            });
            if (!sessionConflicts) {
                sessionElement.parents(".session__table-content").removeClass("-red-selected");
                sessionElement.parents(".session__table-content").addClass("-green-selected");
            }

            registrationConflicts = registrationConflicts || sessionConflicts;
        });
    };

    $("input[id$='_sessionCheckBox']").change(function () {
        var sessionCheckBox = $(this);
        var price = sessionCheckBox.data("price");

        if (sessionCheckBox.is(":checked")) {
            increasePaymentTotal(price);
        } else {
            decreasePaymentTotal(price);
        }

        updateConflicts();
        RequiredSessionsModule.confirmRequiredSessions(sessionCheckBox);
    });

    $("select[id$='_sessionQuantityDropDown']").change(function () {
        var sessionQuantityDropDown = $(this);
        var priorQuantity = Number(sessionQuantityDropDown.data("previousquantity"));
        var price = currencyToFloat(sessionQuantityDropDown.data("price"));
        var quantity = Number(sessionQuantityDropDown.val());
        var difference = Math.abs(priorQuantity - quantity) * price;

        if (quantity > priorQuantity) {
            increasePaymentTotal(difference);
        } else {
            decreasePaymentTotal(difference);
        }
        sessionQuantityDropDown.data("previousquantity", quantity);

        updateConflicts();
        RequiredSessionsModule.confirmRequiredSessions(sessionQuantityDropDown);
    });

    // forces Safari to not begin rendering the next page until current page renders
    function safariSubmitBreather() {
        $("#conflictsForm").submit();
    }

    // Modal to tell customers that they need to call to cancel paid sessions
    $(function () {
        // Create Modal with Custom Settings
        $("#paidModal").dialog({
            modal: true,
            resizable: false,
            autoOpen: false,
            closeText: "close"
        });

        // Launch Modal on Click Event
        $(".js-paid-modal").click(function () {
            $("#paidModal").dialog('open');
        });
    });

    $("#paymentButton").click(function () {
        var allowConflicts = $("#AllowedConflicts").val().toLowerCase() === 'true';

        if (registrationConflicts) {
            var message = allowConflicts ? "There are still conflicts remaining on your registration. Press 'OK' to continue with these conflicts." : "All conflicts must be resolved before your registration can be completed.";
            tracking.trackError(message);
            var response = confirm(message);

            if (!response || !allowConflicts) {
                return false;
            }
        }

        $("#paymentButton").hide();
        $("#payNowButton").hide();
        $("#processingButton").show();
        setTimeout(safariSubmitBreather, 1);
    });

    $("#payNowButton").click(function () {
        var allowConflicts = $("#AllowedConflicts").val().toLowerCase() === 'true';

        if (registrationConflicts) {
            var message = allowConflicts ? "There are still conflicts remaining on your registration. Press 'OK' to continue with these conflicts." : "All conflicts must be resolved before your registration can be completed.";
            tracking.trackError(message);
            var response = confirm(message);

            if (!response || !allowConflicts) {
                return false;
            }
        }

        $("#payNowButton").hide();
        $("#paymentButton").hide();
        $("#processingButton").show();
        setTimeout(safariSubmitBreather, 1);
    });
});