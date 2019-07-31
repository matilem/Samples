$(document).ready(function () {

    $(document).on("focus", ".js-mask-phone",function() {
        $(this).mask("(999) 999-9999? x99999");
    });

    $("#receiptButton").click(function (event) {
        event.preventDefault();

        var url = $(this).data("url");
        window.open(url);
    });

    $("#housingButton").click(function (event) {
        event.preventDefault();

        var url = $(this).data("url");
        window.open(url);
    });

    $("#saveCommentsButton").click(function (event) {
        event.preventDefault();

        var comments = $("#Comment").val();
        $(this).hide();
        $("#savingCommentsButton").show();
        $("#commentsWarning").text("").hide();

        var registrationKey = $("#RegistrationKey").val();
        var site = getsiteUrl();

        $.post(site + "/registration/save-comments", { registrationKey: registrationKey, comments: comments })
        .done(function (data) {
            if (data.HasError) {
                $("#commentsWarning").text("Error sending comments.").slideDown();
                $(this).show();
                $("#savingCommentsButton").hide();
            } else {
                if (data.Data) {
                    $("#commentsWarning").text("You feedback has been submitted. Thank you!").slideDown();
                    $("#savingCommentsButton").hide();
                    $("#Comment").attr("disabled", "disabled");
                } else {
                    $("#commentsWarning").text("Error sending comments.").slideDown();
                    $(this).show();
                    $("#savingCommentsButton").hide();
                }
            }
        })
        .fail(function () {
            $("#commentsWarning").text("Error sending comments.").slideDown();
            $(this).show();
            $("#savingCommentsButton").hide();
        });


    });

    $("#emailConfirmation").click(function (event) {
        event.preventDefault();

        $(this).hide();
        $("#sendingEmailButton").show();
        $("#emailWarningConfirmation").text("");
        $("#emailWarningIcon").addClass("fa-exclamation-circle").removeClass("fa-check-circle");
        $("#emailWarning").removeClass("-success").removeClass("-warning").hide();

        var registrantKey = $("#RegistrationKey").val();
        var site = getsiteUrl();

        $.post(site + "/registration/send-confirmation-email/" + registrantKey)
        .done(function (data) {
            if (data.HasError) {
                $("#emailWarningContent").text("There was an error sending the email. Please try again.");
                $("#emailWarning").addClass("-warning").slideDown();
                $("#emailConfirmation").show();
                $("#sendingEmailButton").hide();
            } else {
                if (data.Data) {
                    $("#emailWarningContent").text("Your email confirmation is on its way.");
                    $("#emailWarningIcon").removeClass("fa-exclamation-circle").addClass("fa-check-circle");
                    $("#emailWarning").addClass("-success").slideDown();
                    $("#emailConfirmation").show();
                    $("#sendingEmailButton").hide();
                } else {
                    $("#emailWarningContent").text("There was an error sending the email. Please try again.");
                    $("#emailWarning").addClass("-warning").slideDown();
                    $("#emailConfirmation").show();
                    $("#sendingEmailButton").hide();
                }
            }
        })
        .fail(function () {
            $("#emailWarningContent").text("There was an error sending the email. Please try again.");
            $("#emailWarning").addClass("-warning").slideDown();
            $("#emailConfirmation").show();
            $("#sendingEmailButton").hide();
        });
    });

    $("#sendingEmailButton").click(function (event) {
        event.preventDefault();
    });

    $("#editSessionsButton")
    .click(function (event) {
        event.preventDefault();
        var registrationKey = $("#RegistrationKey").val();
        var site = getsiteUrl();

        window.location.href = site + "/registration/edit/" + registrationKey + "/sessions";
        });
});

function getsiteUrl() {
    var protocol = document.location.protocol;
    var domain = document.location.host;

    return protocol + "//" + domain + "/events";
}