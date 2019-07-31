$(document).ready(function () {

    $("#validateButton").click(function (event) {
        event.preventDefault();

        $("#errorAlertBox").slideUp();

        var userName = $("#userName").val().trim();
        var userNameError = $("#userNameErrorSpan");
        var processingSpan = $("#validatingSpan");
        var userTypeDiv = $("#userType");
        var validateButton = $("#validateButton");
        var errorMessage = "Please input a user name. ";
        var protocol = document.location.protocol;
        var domain = document.location.host;
        var site = protocol + "//" + domain + "/admintool-api/user/validate/" + userName;

        if (userName == "") {
            $("#errorMessageParagraph").html(errorMessage);
            $("#errorAlertBox").slideDown();
            processingSpan.hide();
            validateButton.show();
        }
        else {
            validateButton.hide();
            processingSpan.show();

            //$.ajax({
            //    type: "GET",
            //    url: site,
            //    success: function (data) {
                    $("#userName").prop("disabled", true);
                    userTypeDiv.show();
                    validateButton.hide();
                    processingSpan.hide();
            //    },
            //    error: function () {
            //        errorMessage = "Invalid User Name. ";
            //        $("#errorMessageParagraph").html(errorMessage);
            //        $("#errorAlertBox").slideDown();
            //        processingSpan.hide();
            //        validateButton.show();
            //    }
            //});
        }
    });

    $("#submitButton").click(function (event) {
        event.preventDefault();

        $("#errorAlertBox").slideUp();

        var submitButton = $("#submitButton");
        var processingSpan = $("#processingSpan");
        var userName = $("#userName").val();
        var userChangeType = $('input[name=userChangeType]:checked').val();
        var userNameError = $("#userNameErrorSpan");
        var userChangeTypeError = $("#userChangeTypeErrorSpan");
        var errorMessage = "";
        var hasError = false;

        userNameError.hide();
        userChangeTypeError.hide();
        processingSpan.show();
        submitButton.hide();

        if (userChangeType == undefined) {
            hasError = true;
            errorMessage = "Please input a user type."
            $("#errorMessageParagraph").html(errorMessage);
            $("#errorAlertBox").slideDown();
            processingSpan.hide();
            submitButton.show();
        }

        if (userChangeType == "new") {
            alert("new");
        }

        if (hasError == false) {
            alert("You are a winner!")
            processingSpan.hide();
            submitButton.show();
        }
    });

    //$("#existingUser").click(function (event) {
    //    $(existingUserPermissions).show();
    //});
});