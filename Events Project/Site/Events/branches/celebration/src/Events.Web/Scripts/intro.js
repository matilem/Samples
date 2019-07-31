$(document).ready(function () {
    preSelectFee();

    $(".radio-align").change(function () {
        updateTotal();
    });

    // forces Safari to not begin rendering the next page until current page renders
    function safariSubmitBreather() {
        $("#introForm").submit();
    }

    $("#paymentButton").click(function () {
        if (!$("input[id$='_selectedPriceKey']").is(":checked")) {
            $(".error").show();
            tracking.trackError($(".error").html());
            return false;
        }

        $("#paymentButton").hide();
        $("#processingButton").show();
        setTimeout(safariSubmitBreather, 1);
    });
});

function preSelectFee() {
    var empyGuid = "00000000-0000-0000-0000-000000000000";

    var relatedRegistrationKeys = $("input[id$='_RegistrationKey']");
    var relatedOptions = $("input[id$='_relatedSelectedPriceKey']");

    relatedRegistrationKeys.each(function () {
        var relatedRegistrationKey = $(this).val();

        // customer is not registered for the event
        if (relatedRegistrationKey === empyGuid) {
            return true;
        }

        relatedOptions.each(function () {
            var relatedOption = $(this);

            if (relatedOption.data("registration-key") !== relatedRegistrationKey) {
                return true;
            }
        });
    });

    var registrationKey = $("#RegistrationKey").val();
    var options = $("input[id$='_selectedPriceKey']");

    if (registrationKey === empyGuid) {
        updateTotal();
        return;
    }

    if (options.length === 1) {
        options.attr("checked", true);
        return;
    }

    options.each(function () {
        radioButton = $(this);

        if (radioButton.val() === empyGuid) {
            radioButton.attr("checked", true);
        }
    });
}

function updateTotal() {
    var paymentTotal = 0.0;

    $("input[id$='_selectedPriceKey']").each(function () {
        var selectedPriceKey = $(this);
        if (selectedPriceKey.is(":checked")) {
            var eventPrice = currencyToFloat(selectedPriceKey.data("price"));
            var sessionsPrice = currencyToFloat($("#CurrentSessionsCost").val());
            paymentTotal = eventPrice + sessionsPrice;
        }
    });

    $("input[id$='_relatedSelectedPriceKey']").each(function () {
        var relatedSelectedPriceKey = $(this);
        if (relatedSelectedPriceKey.is(":checked")) {
            var eventPrice = currencyToFloat(relatedSelectedPriceKey.data("price"));
            var sessionsPrice = currencyToFloat(relatedSelectedPriceKey.data("sessions_cost"));
            paymentTotal = currencyToFloat(paymentTotal) + sessionsPrice + eventPrice;
        }
    });

    $("#paymentTotal").text(floatToCurrency(paymentTotal));
}

function getsiteUrl() {
    var protocol = document.location.protocol;
    var domain = document.location.host;

    return protocol + "//" + domain + "/events";
}