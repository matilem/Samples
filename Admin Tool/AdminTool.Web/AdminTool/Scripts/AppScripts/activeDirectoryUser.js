$(document).ready(function () {

    $("#submitButton").click(function (event) {
        event.preventDefault();

        $("#errorAlertBox").slideUp();

        var userName = $("#userName").val().trim();
        var userNameError = $("#userNameErrorSpan");
        var processingSpan = $("#processingSpan");
        var submitButton = $("#submitButton");
        var errorMessage = "Please input a user name. ";

        if (userName == "") {
            $("#errorMessageParagraph").html(errorMessage);
            $("#errorAlertBox").slideDown();
            processingSpan.hide();
            submitButton.show();
        }
        else {
            submitButton.hide();
            processingSpan.show();
            submit();
        }
    });
});


function submit() {

    var protocol = document.location.protocol;
    var domain = document.location.host;
    var site = getsiteUrl() + "/admintool-api/user/create/";
    var processingSpan = $("#processingSpan");
    var submitButton = $("#submitButton");
    var errorMessage = "Unable to create user. ";

    var model = {
        UserName: $("#userName").val().trim(),
        HydraUser: $("#hydraUserCheckbox").prop("checked"),
        TookKitUser: $("#toolKitUserCheckbox").prop("checked")
    };

    $.ajax({
        type: "POST",
        url: site,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8"
    }).done(function (result) {
        if (result) {
            if (hydraCheckbox.prop("checked") == true || toolKitCheckbox.prop("checked") == true) {
                alert("Woo");
                window.location.href = getsiteUrl() + "/home/?status=" + status
            }
            else {
                $("#successMessageParagraph").html(successMessage);
                $("#successAlertBox").slideDown();
            }
        } else {
            $("#errorMessageParagraph").html(errorMessage);
            $("#errorAlertBox").slideDown();
            submitButton.show();
            processingSpan.hide();
        }
    }).fail(function (result) {
        $("#errorMessageParagraph").html(errorMessage);
        $("#errorAlertBox").slideDown();
        submitButton.show();
        processingSpan.hide();
    });
}

function getsiteUrl() {
    var protocol = document.location.protocol;
    var domain = document.location.host;

    return protocol + "//" + domain;
}