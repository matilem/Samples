$(document).ready(function () {
    var emptyGuid = "00000000-0000-0000-0000-000000000000";

    $("input[id$='_sessionCheckBox']").on("change", function () {
        var sessionCheckBox = $(this);
        var price = sessionCheckBox.data("price");
        var key = sessionCheckBox.data("key");
        var isChecked = sessionCheckBox.is(":checked");

        if (price > 0) {
            if (isChecked) {
                increasePaymentTotal(price);
            } else {
                decreasePaymentTotal(price);
            }
        }

        RequiredSessionsModule.confirmRequiredSessions(sessionCheckBox);
    });

    $("select[id$='_sessionQuantityDropDown']").change(function () {
        var sessionQuantityDropDown = $(this);
        var previousQuantity = Number(sessionQuantityDropDown.data("previousquantity"));
        var price = sessionQuantityDropDown.data("price");
        var key = sessionQuantityDropDown.data("key");
        var priceFloat = currencyToFloat(price);
        var quantity = Number(sessionQuantityDropDown.val());
        var decreaseTotal = priceFloat * previousQuantity;

        RequiredSessionsModule.confirmRequiredSessions(sessionQuantityDropDown);

        if (!isNaN(decreaseTotal)) {
            decreasePaymentTotal(decreaseTotal);
        }

        if (quantity > 0) {
            var increaseTotal = priceFloat * quantity;
            increasePaymentTotal(increaseTotal);
        }

        sessionQuantityDropDown.data("previousquantity", quantity);
    });

    $(".guest-badge").each(function () {
        var name = $(this).find("input[id$='Name']");

        if (name.val() === "") {
            $(this).hide();
        }
    });

    $("#addGuest").on("click", function (event) {
        event.preventDefault();

        var rowCount = $(".guest-badge").length;
        var shown = rowCount - $(".guest-badge").filter(":hidden").size();

        $(".guest-badge").filter(":hidden").filter(":first").show();

        if (shown === (rowCount - 1)) {
            $(this).hide();
        }
    });

    $(".guest-badge__remove").on("click", function (event) {
        event.preventDefault();

        if ($(this).hasClass("guest-badge__remove")) {
            $(this).html('<i class="fa fa-minus-circle"></i> Delete Guest?').addClass('guest-badge__confirm-remove').removeClass('guest-badge__remove');
        } else {
            var tableRow = $(this).closest(".guest-badge").hide();
            tableRow.find("input:text").val("");
            $(this).html('<i class="fa fa-minus-circle"></i> Remove Guest').addClass('guest-badge__remove').removeClass('guest-badge__confirm-remove');
            $('#addGuest').show();
        }
    });
    // forces Safari to not begin rendering the next page until current page renders
    function safariSubmitBreather() {
        $("#sessionForm").submit();
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

    $("#paymentButton").click(function (event) {
        var requiredErrors = [];

        $("[id$='_headingTableDiv']").each(function () {
            var headingTableDiv = $(this);
            var requiredHeading = headingTableDiv.attr("data-isRequired");

            if (requiredHeading !== "True") {
                return true;
            }

            var requiredHeadingDiv = headingTableDiv.parent().find("[id$='_requiredHeadingDiv']");

            if (headingTableDiv.children().find("[id$='_sessionCheckBox']").is(":checked") || headingTableDiv.children().find("[id$='paidItemSpan']").length > 0) {
                requiredHeadingDiv.hide();
            }
            else {
                requiredErrors.push(requiredHeadingDiv.attr("id"));
                requiredHeadingDiv.show();
            }
        });

        $("input[id$='_sessionCheckBox']").each(function () {
            var requiredSession = $(this).data("reqsession");
            var isChecked = $(this).is(":checked");

            if (!isChecked) {
                return true;
            }

            if (requiredSession === emptyGuid) {
                return true;
            }

            $("input[id$='_sessionCheckBox']").each(function () {
                var sessionCheckBox = $(this);
                var reqSessionCheckbox = sessionCheckBox.data("key");
                var isChecked = sessionCheckBox.is(":checked");

                if (requiredSession !== reqSessionCheckbox) {
                    return true;
                }

                if (!isChecked) {
                    var requiredSessionDiv = sessionCheckBox.parent().parent().find("[id$='_requiredSessionDiv']");
                    requiredErrors.push(requiredSessionDiv.attr("id"));
                    RequiredSessionsModule.confirmRequiredSessions(sessionCheckBox);
                }
            });
        });

        $("select[id$='_sessionQuantityDropDown']").each(function () {
            var sessionQuantityDropDown = $(this);
            var requiredSession = sessionQuantityDropDown.data("reqsession");
            var quantity = Number(sessionQuantityDropDown.val());

            if (quantity <= 0) {
                return true;
            }

            if (requiredSession === emptyGuid) {
                return true;
            }

            var requiredSessionInput = $("#" + requiredSession + "_sessionQuantityDropDown");

            if (Number(requiredSessionInput.val()) === 0) {
                var requiredSessionDiv = $(this).parent().parent().find("[id$='_requiredSessionDiv']");
                requiredErrors.push(requiredSessionDiv.attr("id"));
                RequiredSessionsModule.confirmRequiredSessions(sessionQuantityDropDown);
            }
        });

        if (requiredErrors.length > 0) {
            var requiredErrorId = $("#" + requiredErrors[0]);
            var requiredErrorPosition = requiredErrorId.position();

            window.scroll(0, requiredErrorPosition.top);

            return false;
        }

        $("#paymentButton").hide();
        $("#processingButton").show();
        setTimeout(safariSubmitBreather, 1);
    });
});