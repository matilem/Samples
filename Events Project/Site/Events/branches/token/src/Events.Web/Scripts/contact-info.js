$(document).ready(function () {
    $(".js-mask-phone").mask("(999) 999-9999? x99999");

    // forces Safari to not begin rendering the next page until current page renders
    function safariSubmitBreather() {
        $("#contactInfoForm").submit();
    }

    $("#paymentButton").click(function () {
        $("#paymentButton").hide();
        $("#processingButton").show();
        setTimeout(safariSubmitBreather, 1);
    });
});