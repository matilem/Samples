function increasePaymentTotal(value) {
    var paymentTotal = currencyToFloat($("#paymentTotal").text());
    paymentTotal = paymentTotal + currencyToFloat(value);
    $("#paymentTotal").text(floatToCurrency(paymentTotal));
}

function decreasePaymentTotal(value) {
    var paymentTotal = currencyToFloat($("#paymentTotal").text());
    paymentTotal = paymentTotal - currencyToFloat(value);
    $("#paymentTotal").text(floatToCurrency(paymentTotal));
}

function currencyToFloat(input) {
    if (input == undefined || typeof input === "boolean")
        return 0;
    
    if (Number(input) === input)
        return input;

    var number = input.replace(/\$/g, "");

    // have to remove "," or number is treated as a decimal beginning where the "," is
    number = number.replace(/\,/g, "");
    var amount = parseFloat(number);

    if (isNaN(amount)) {
        return 0;
    } else {
        return amount;
    }
}

function floatToCurrency(input) {
    var amount = parseFloat(input).toFixed(2);

    if (isNaN(amount)) {
        return "$0.00";
    } else {
        return "$" + amount;
    }
}