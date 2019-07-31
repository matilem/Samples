function updatePaymentTotal() {
    var paymentTotal = currencyToFloat("0");
    var registrationFee = currencyToFloat($("#SelectedRegistrationFeePrice").val());
    paymentTotal = paymentTotal + registrationFee;

    $("input[id$='_sessionCheckBox']")
        .each(function() {
            var fee = currencyToFloat($(this).data("price"));

            if ($(this).is(":checked")) {
                paymentTotal = paymentTotal + fee;
            }
        });

    $("select[id$='_sessionQuantityDropDown']")
        .each(function() {
            var fee = currencyToFloat($(this).data("price"));
            var quantity = Number($(this).val());

            if (quantity > 0) {
                var itemTotal = fee * quantity;
                paymentTotal = paymentTotal + itemTotal;
            }
        });

    $("#paymentTotal").text(floatToCurrency(paymentTotal));
}

$(document)
    .ready(function() {
        updatePaymentTotal();

        $("input[id$='_sessionCheckBox']")
            .on("change",
                function() {
                    checkForConflicts($(this));
                    updatePaymentTotal();
                });

        $("select[id$='_sessionQuantityDropDown']").on("change", updatePaymentTotal);
    });