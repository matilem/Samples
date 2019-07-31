$(document).ready(function () {

    $("#savePreCourseButton").click(function (event) {
        event.preventDefault();
        var status = "Save";

        var ddlvalue = $('#MilitaryBranches option:selected').val();
        if (ddlvalue != "") {
            $("#militaryError").slideUp();
            submit(status);
        }
        else {
            $("#militaryError").slideDown();
            return false;
        }
    });

    $("#approvePreCourseButton").click(function (event) {
        event.preventDefault();
        var status = "Approve";

        submit(status);

    });

    $("#submitPreCourseButton").click(function (event) {
        event.preventDefault();
        var status = "Submit";

        var ddlvalue = $('#MilitaryBranches option:selected').val();
        if (ddlvalue != "") {
            $("#militaryError").slideUp();
            submit(status);
        }
        else {
            $("#militaryError").slideDown();
            return false;
        }
    });

    $("#resendWelcomeEmail").click(function (event) {
        event.preventDefault();

        var emailButton = $("#resendWelcomeEmail");
        var processingButton = $("#sendingWelcomeEmailButton");
        var successMessage = "The email has been sent.";
        var errorMessage = "Due to a system error the email could not be sent.";
        var protocol = document.location.protocol;
        var domain = document.location.host;
        var site = protocol + "//" + domain + "/also-api/email/welcome/";

        $("#successAlertBox").slideUp();
        $("#errorAlertBox").slideUp();
        emailButton.hide();
        processingButton.show();

        var model = {
            DiscountCode: $("#Activity_ActivityNumber").val(),
            ActivityBeginDate: $("#Activity_ActivityBeginDate").val(),
            ActivityEndDate: $("#Activity_ActivityEndDate").val(),
            ActivityLocation: $("#ActivityLocation").val(),
            ActivitySponsorName: $("#ActivitySponsorName").val(),
            CourseDirectorId: $("#CourseDirectorId").val(),
            CourseDirectorEmail: $("#CourseDirectorEmail").val(),
            CourseCoordinatorId: $("#CourseCoordinatorId").val(),
            CourseCoordinatorEmail: $("#CourseCoordinatorEmail").val(),
            ActivityCourseType: $("#Activity_ActivityCourseType").val()
        };

        $.ajax({
            type: "POST",
            url: site,
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8"
        }).done(function (result) {
            if (result) {
                $("#successMessageParagraph").html(successMessage);
                $("#successAlertBox").slideDown();
                emailButton.show();
                processingButton.hide();
            } else {
                $("#errorMessageParagraph").html(errorMessage);
                $("#errorAlertBox").slideDown();
                emailButton.show();
                processingButton.hide();
            }
        }).fail(function () {
            $("#errorMessageParagraph").html(errorMessage);
            $("#errorAlertBox").slideDown();
            emailButton.show();
            processingButton.hide();
        });
    });

    $("#createDiscount").click(function (event) {
        event.preventDefault();

        var createButton = $("#createDiscount");
        var processingButton = $("#creatingDiscountButton");
        var successMessage = "The discount has been created.";
        var errorMessage = "Due to a system error the discount could not be created.";
        var protocol = document.location.protocol;
        var domain = document.location.host;
        var site = protocol + "//" + domain + "/also-api/discount/create/";

        $("#successAlertBox").slideUp();
        $("#errorAlertBox").slideUp();
        createButton.hide();
        processingButton.show();

        var model = {
            ActivityNumber: $("#Activity_ActivityNumber").val(),
            WebLogin: $("#Customer_WebLogin").val(),
        };

        $.ajax({
            type: "POST",
            url: site,
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8"
        }).done(function (result) {
            if (result) {
                $("#successMessageParagraph").html(successMessage);
                $("#successAlertBox").slideDown();
                createButton.show();
                processingButton.hide();
            } else {
                $("#errorMessageParagraph").html(errorMessage);
                $("#errorAlertBox").slideDown();
                createButton.show();
                processingButton.hide();
            }
        }).fail(function () {
            $("#errorMessageParagraph").html(errorMessage);
            $("#errorAlertBox").slideDown();
            createButton.show();
            processingButton.hide();
        });
    });

    $("#verifyDirector").click(function (event) {
        event.preventDefault();
        $("#successAlertBox").slideUp();
        $("#errorAlertBox").slideUp();
        var cstId = $("#directorId").val();
        var errorMessage = "Invalid Customer Id. ";
        var protocol = document.location.protocol;
        var domain = document.location.host;
        var site = protocol + "//" + domain + "/also-api/individual/verify/" + cstId;

        $.ajax({
            type: "GET",
            url: site,
            success: function (data) {
                console.log("success", data);
                $("#CourseDirectorKey").val(data.Key);
                $("#directorId").val(data.CustomerId);
                $("#directorName").val(data.FullName);
                $("#CourseDirectorEmail").val(data.Email);
            },
            error: function () {
                $("#errorMessageParagraph").html(errorMessage);
                $("#errorAlertBox").slideDown();
            }
        });
    });

    $("#verifyCoordinator").click(function (event) {
        event.preventDefault();
        $("#successAlertBox").slideUp();
        $("#errorAlertBox").slideUp();
        var cstId = $("#coordinatorId").val();
        var errorMessage = "Invalid Customer Id. ";
        var protocol = document.location.protocol;
        var domain = document.location.host;
        var site = protocol + "//" + domain + "/also-api/individual/verify/" + cstId;

        $.ajax({
            type: "GET",
            url: site,
            success: function (data) {
                console.log("success", data);
                $("#CourseCoordinatorKey").val(data.Key);
                $("#coordinatorId").val(data.CustomerId);
                $("#coordinatorName").val(data.FullName);
                $("#CourseCoordinatorEmail").val(data.Email);
            },
            error: function () {
                $("#errorMessageParagraph").html(errorMessage);
                $("#errorAlertBox").slideDown();
            }
        });
    });

    $('#requestAgenda').click(function (event) {
        var email = $("#CourseDirectorEmail").val() + '; ' + $("#CourseCoordinatorEmail").val();
        document.location = "mailto:" + email;
    });

    $("#saveNote").click(function (event) {
        event.preventDefault();
        $("#successAlertBox").slideUp();
        $("#errorAlertBox").slideUp();
        var successMessage = "The note has been saved.";
        var errorMessage = "Due to a system error the note could not be saved.";
        var protocol = document.location.protocol;
        var domain = document.location.host;
        var site = protocol + "//" + domain + "/also-api/notes/save/";

        var model = {
            AlsoCourseKey: $("#AlsoCourse_AlsoCourseKey").val(),
            WebLogin: $("#Customer_WebLogin").val(),
            Note: $("#courseNote").val()
        };

        $.ajax({
            type: "POST",
            url: site,
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8"
        }).done(function (result) {
            if (result) {
                $("#successMessageParagraph").html(successMessage);
                $("#successAlertBox").slideDown();
                location.reload();
            } else {
                $("#errorMessageParagraph").html(errorMessage);
                $("#errorAlertBox").slideDown();
            }
        }).fail(function () {
            $("#errorMessageParagraph").html(errorMessage);
            $("#errorAlertBox").slideDown();
        });
    });
});

function submit(status) {
    var saveButton = $("#savePreCourseButton");
    var approveButton = $("#approvePreCourseButton");
    var submitButton = $("#submitPreCourseButton");
    var processingButton = $("#savingPreCourseButton");
    var preCourseErrorSpan = $("#preCourseErrorSpan");
    var site = getsiteUrl() + "/precourse/save/";
    var errorMessage = "Due to a system error some of your current changes were unable to be saved. Please try again or call us at (800) 274-2237 for assistance in resolving the issue.";

    saveButton.hide();
    submitButton.hide();
    approveButton.hide();
    processingButton.show();

    var alsoCourseKey = $("#AlsoCourse_AlsoCourseKey").val()
    var militaryBranch = "";

    if (alsoCourseKey != "") {
        militaryBranch = $('#AlsoCourse_MilitaryBranchKey option:selected').val();
    }
    else {
        militaryBranch = $('#MilitaryBranches option:selected').val();
    }

    var model = {
        ActivityKey: $("#Activity_ActivityKey").val(),
        ActivityNumber: $("#Activity_ActivityNumber").val(),
        ActivityCourseType: $("#Activity_ActivityCourseType").val(),
        WebLogin: $("#Customer_WebLogin").val(),
        CourseDirectorKey: $("#CourseDirectorKey").val(),
        CourseDirectorId: $("#directorId").val(),
        CourseDirectorName: $("#directorName").val(),
        CourseDirectorEmail: $("#CourseDirectorEmail").val(),
        CourseCoordinatorKey: $("#CourseCoordinatorKey").val(),
        CourseCoordinatorId: $("#coordinatorId").val(),
        CourseCoordinatorName: $("#coordinatorName").val(),
        CourseCoordinatorEmail: $("#CourseCoordinatorEmail").val(),
        AlsoCourseKey: $("#AlsoCourse_AlsoCourseKey").val(),
        ActivityBeginDate: $("#Activity_ActivityBeginDate").val(),
        ActivityEndDate: $("#Activity_ActivityEndDate").val(),
        ActivityLocation: $("#ActivityLocation").val(),
        ActivitySponsorName: $("#ActivitySponsorName").val(),
        MilitaryBranchKey: militaryBranch,
        Status: status
    };

    $.ajax({
        type: "POST",
        url: site,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8"
    }).done(function (result) {
        if (result) {
            window.location.href = getsiteUrl() + "/home/?status=" + status
        } else {
            $("#preCourseErrorSpan").slideDown();
            processingButton.hide();
            saveButton.show();
            submitButton.show();
            approveButton.show();
            console.log(result);
        }
    }).fail(function (result) {
        $("#preCourseErrorSpan").slideDown();
        processingButton.hide();
        saveButton.show();
        submitButton.show();
        approveButton.show();
        console.log(result);
    });
}

function getsiteUrl() {
    var protocol = document.location.protocol;
    var domain = document.location.host;

    return protocol + "//" + domain + "/also";
}