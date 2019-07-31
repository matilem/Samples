$(document).ready(function () {
    $(".js-mask-phone").mask("(999) 999-9999? x99999");
    var badgeNickname = $("#Badge_Nickname").text();
    var badgeCity = $("#Badge_City").text();
    var badgeState = $("#Badge_State").text();
    var badgeShowState = $("#Badge_ShowState").val();

    // forces Safari to not begin rendering the next page until current page renders
    function safariSubmitBreather() {
        $("#contactInfoForm").submit();
    }

    function submit(payNow) {
        $("#PayNow").val(payNow);
        $("#paymentButton").hide();
        $("#payNowButton").hide();
        $("#processingButton").show();
        setTimeout(safariSubmitBreather, 1);
    }

    function checkValdiation(field) {

        if (field == null) {
            return false;
        }
        if (field === "") {
            return false;
        }
        if (field === "undefined") {
            return false;
        }
        else {
            return true;
        }
    }

    $("#payNowButton").click(function () {
        submit(true);
    });

    $("#paymentButton").click(function (event) {
        event.preventDefault();

        badgeNickname = document.getElementById("BadgeNickname").value;
        badgeCity = document.getElementById("BadgeCity").value;
        if (badgeShowState === true) {
            badgeState = document.getElementById("BadgeState").value;
        }

        var nickNameStatus = checkValdiation(badgeNickname);

        if (nickNameStatus === false) {
            $("#badgeNicknameWarning").text("Nickname is required.").slideDown();
            tracking.trackError($("#badgeNicknameWarning").text());
            return;
        }

        var cityStatus = checkValdiation(badgeCity);

        if (cityStatus === false) {
            $("#badgeCityWarning").text("City is required.").slideDown();
            tracking.trackError($("#badgeCityWarning").text());
            return;
        }

        if (badgeShowState === true) {

            var stateStatus = checkValdiation(badgeState);

            if (stateStatus === false) {
                $("#badgeStateWarning").text("State is required.").slideDown();
                tracking.trackError($("#badgeStateWarning").text());
                return;
            }
        } else {
            $("#paymentButton").hide();
            $("#payNowButton").hide();
            $("#processingButton").show();
            setTimeout(safariSubmitBreather, 1);
        }
    });
});