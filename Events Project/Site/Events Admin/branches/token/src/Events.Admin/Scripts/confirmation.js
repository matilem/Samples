$(document).ready(function () {

    $("#resendEmailConfirmationButton").click(function (event) {
        event.preventDefault();

        $("#resendEmailConfirmationButton").hide();
        $("#sendingEmailButton").show();
        $("#emailWarning").text("").hide();

        var registrantKey = $("#RegistrationKey").val();
        var emailAddress = $("#emailAddress").val();
        var site = getsiteUrl();

        $.post(site + "/registration/" + registrantKey + "/send-email", { email: emailAddress })
        .done(function (data) {
            if (data.HasError) {
                $("#emailWarning").text("Error sending email.").slideDown();
                $("#resendEmailConfirmationButton").show();
                $("#sendingEmailButton").hide();
            } else {
                if (data.Data) {
                    $("#emailWarning").text("Email sent successfully.").slideDown();
                    $("#resendEmailConfirmationButton").show();
                    $("#sendingEmailButton").hide();
                } else {
                    $("#emailWarning").text("Error sending email.").slideDown();
                    $("#resendEmailConfirmationButton").show();
                    $("#sendingEmailButton").hide();
                }
            }
        })
        .fail(function () {
            $("#emailWarning").text("Error sending email.").slideDown();
            $("#resendEmailConfirmationButton").show();
            $("#sendingEmailButton").hide();
        });
    });

    $("#sendingEmailButton").click(function (event) {
        event.preventDefault();
    });
});