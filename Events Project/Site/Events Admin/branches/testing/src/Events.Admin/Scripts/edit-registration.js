function updateRowStateDisplay(input, quantity) {
    var originalQuantity = input.data("original-quantity");

    var increased = quantity > originalQuantity;
    var decreased = quantity < originalQuantity;

    input.parents(".qr-table__row").toggleClass("qr-table--state-pending-addition", increased);
    input.parents(".qr-table__row").toggleClass("qr-table--state-pending-removal", decreased);
}

$(document)
    .ready(function() {
        var paymentTotal = 0.00;
        $("#paymentTotal").text("0.00");

        $("input[id$='_sessionCheckBox']")
            .on("change",
                function() {
                    var checkbox = $(this);
                    checkForConflicts(checkbox);

                    var fee = currencyToFloat(checkbox.data("price"));
                    var quantity = checkbox.is(":checked") ? 1 : 0;

                    if (quantity > 0) {
                        paymentTotal = paymentTotal + fee;
                    } else {
                        paymentTotal = paymentTotal - fee;
                    }

                    updateRowStateDisplay(checkbox, quantity);
                    $("#paymentTotal").text(floatToCurrency(paymentTotal));
                });

        $("select[id$='_sessionQuantityDropDown']")
            .on("change",
                function() {
                    var dropdown = $(this);
                    var fee = currencyToFloat(dropdown.data("price"));
                    var quantity = Number(dropdown.val());
                    var originalQuantity = dropdown.data("original-quantity");
                    var itemTotal = fee * (quantity - originalQuantity);
                    paymentTotal = paymentTotal + itemTotal;

                    updateRowStateDisplay(dropdown, quantity);
                    $("#paymentTotal").text(floatToCurrency(paymentTotal));
                });
    });