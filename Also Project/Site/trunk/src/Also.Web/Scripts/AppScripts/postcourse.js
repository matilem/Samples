﻿$(document).ready(function () {

    $("#learnerOccupation").change(function () {

        $(this).find('option').removeAttr('selected');

        var occupation = $(this).find("option:selected").val();

        $(this).find("option[value=" + occupation + "]").attr('selected', true);
        $(this).find("option[value=" + occupation + "]").prop('selected', true);

    });

    $("#savePostCourseButton").click(function (event) {
        event.preventDefault();
        var status = "Save";
        var learners = [];
        var instructors = [];

        $("#learnersTable tbody tr").each(function (i) {
            var learnerKey = $(this).find("#learner_LearnerKey").val();
            var custKey = $(this).find("#learner_CustomerKey").val();
            var occupationKey = $(this).find('#learnerOccupation option:selected').val();
            var gradeName = $(this).find("input:radio").attr("name");
            var gradeVal = $("input:radio[name = " + gradeName + "]:checked").val();

            var learner = {
                LearnerKey: learnerKey,
                CustomerKey: custKey,
                OccupationKey: occupationKey,
                Grade: gradeVal
            };

            learners.push(learner);
        })

        $("#instructorTable tbody tr:not(:first-child)").each(function (i) {
            var instructorKey = $(this).find("#InstructorKey").val();
            var cstKey = $(this).find("#CustomerKey").val();
            var advisor = $(this).find("#RecommendAdvisoryFaculty").prop('checked');
            var instructorRec = $(this).find("#RecommendInstructor").prop('checked');

            var instructor = {
                InstructorKey: instructorKey,
                CustomerKey: cstKey,
                AdvisoryFacultyRecommended: advisor,
                InstructorRecommended: instructorRec
            };

            instructors.push(instructor);
        })

        submit(learners, instructors, status);
    });

    $("#submitPostCourseButton").click(function (event) {
        event.preventDefault();
        $("#instructorErrorAlertBox").slideUp();
        var status = "Submit";
        var learners = [];
        var instructors = [];
        var errorMessage = "";
        var all_answered = false;
        var hasError = false;

        $("#learnersTable tbody tr").each(function (i) {
            var learnerKey = $(this).find("#learner_LearnerKey").val();
            var custKey = $(this).find("#learner_CustomerKey").val();
            var occupationKey = $(this).find('#learnerOccupation option:selected').val();
            var gradeName = $(this).find("input:radio").attr("name");
            var gradeVal = $("input:radio[name = " + gradeName + "]:checked").val();
            var eligibility = $(this).find("#learner_Eligible").val();

            if (occupationKey === "") {
                all_answered = false;
                $("#occupationErrorSpan").slideDown();
            }

            if (gradeVal === undefined && eligibility == "True")
            {
                all_answered = false;
                $("#gradeErrorSpan").slideDown();
            } else {
                all_answered = true;
                var learner = {
                    LearnerKey: learnerKey,
                    CustomerKey: custKey,
                    OccupationKey: occupationKey,
                    Grade: gradeVal
                };

                learners.push(learner);
            }
        })

        $("#instructorTable tbody tr:not(:first-child)").each(function (i) {
            var instructorKey = $(this).find("#InstructorKey").val();
            var cstKey = $(this).find("#CustomerKey").val();
            var advisor = $(this).find("#RecommendAdvisoryFaculty").prop('checked');
            var instructorRec = $(this).find("#RecommendInstructor").prop('checked');
            var existingStatus = $(this).find("#currentStatus").html();

            if (existingStatus === "Instructor Candidate") {
                errorMessage = "You need to add an Advisory Faculty.";
                hasError = true;
            }

            if (existingStatus === "Advisory Faculty") {
                errorMessage = "";
                hasError = false;
            }
            
            var instructor = {
                InstructorKey: instructorKey,
                CustomerKey: cstKey,
                AdvisoryFacultyRecommended: advisor,
                InstructorRecommended: instructorRec
            };

            instructors.push(instructor);
        })

        if (hasError === true) {
            $("#instructorErrorMessageParagraph").html(errorMessage);
            $("#instructorErrorAlertBox").slideDown();
            return false;
        }

        if (all_answered) {
            $("#postCourseErrorSpan").slideUp();
            $("#occupationErrorSpan").slideUp();
            $("#gradeErrorSpan").slideUp();
            submit(learners, instructors, status);
        }
        else {
            return false;
        }
    });

    $("#completeCourseButton").click(function (event) {
        event.preventDefault();
        var status = "Complete";

        var learners = [];
        var instructors = [];
        var errorMessage = "";
        var all_answered = false;

        $("#learnersTable tbody tr").each(function (i) {
            var learnerKey = $(this).find("#learner_LearnerKey").val();
            var custKey = $(this).find("#learner_CustomerKey").val();
            var occupationKey = $(this).find('#learnerOccupation option:selected').val();
            var gradeName = $(this).find("input:radio").attr("name");
            var gradeVal = $("input:radio[name = " + gradeName + "]:checked").val();

            if (occupationKey === "") {
                all_answered = false;
                $("#occupationErrorSpan").slideDown();
            }

            if (gradeVal === undefined) {
                all_answered = false;
                $("#gradeErrorSpan").slideDown();
            }

            else {
                all_answered = true;
                var learner = {
                    LearnerKey: learnerKey,
                    CustomerKey: custKey,
                    OccupationKey: occupationKey,
                    Grade: gradeVal
                };

                learners.push(learner);
            }
        })

        $("#instructorTable tbody tr:not(:first-child)").each(function (i) {
            var instructorKey = $(this).find("#InstructorKey").val();
            var cstKey = $(this).find("#CustomerKey").val();
            var advisor = $(this).find("#RecommendAdvisoryFaculty").prop('checked');
            var instructorRec = $(this).find("#RecommendInstructor").prop('checked');
            var existingStatus = $(this).find("#currentStatus").html();

            var instructor = {
                InstructorKey: instructorKey,
                CustomerKey: cstKey,
                AdvisoryFacultyRecommended: advisor,
                InstructorRecommended: instructorRec
            };

            instructors.push(instructor);
        })

        if (all_answered) {

            $("#postCourseErrorSpan").slideUp();
            $("#occupationErrorSpan").slideUp();
            $("#gradeErrorSpan").slideUp();
            submit(learners, instructors, status);
        }
    });
    
    $("#addInstructor").click(function (event) {

        event.preventDefault();
        $("#successAlertBox").slideUp();
        $("#errorAlertBox").slideUp();
        $("#instructorErrorAlertBox").slideUp();
        var hasError = false;
        var cstId = $("#instructorId").val();
        var errorMessage = "Invalid Customer ID";
        var protocol = document.location.protocol;
        var domain = document.location.host;
        var site = protocol + "//" + domain + "/also-api/individual/verify/" + cstId;
        var instructorSite = protocol + "//" + domain + "/also/postcourse/instructor/";
        var isFirstEntry = true;

        $("#instructorTable tbody tr").each(function (i) {
            var existingId;
            existingId = $(this).attr("id");
            if ( $.isNumeric(existingId) ) {
                isFirstEntry = false;
            }
            if (cstId === existingId) {
                errorMessage = "The Instructor ID you entered already exists";
                hasError = true;
            }
        })

        if (hasError === true) {
            $("#instructorErrorMessageParagraph").html(errorMessage);
            $("#instructorErrorAlertBox").slideDown();
            return false;
        }
        else {
            $.ajax({
                type: "GET",
                url: site,
                success: function (data) {
                    var cstKey = data.Key;
                    var aafpId = data.CustomerId;
                    var instructorFName = data.FirstName;
                    var instructorLName = data.LastName;
                    var alsoStatus = data.CurrentAlsoStatus;

                    var model = {
                        CustomerKey: cstKey,
                        CustomerId: aafpId,
                        FirstName: instructorFName,
                        LastName: instructorLName,
                        CurrentAlsoStatus: alsoStatus
                    };

                    addInstructor(instructorSite, model, isFirstEntry);
                    $("#instructorId").val(""); //clear the form
                },
                error: function () {
                    $("#instructorErrorMessageParagraph").html(errorMessage);
                    $("#instructorErrorAlertBox").slideDown();
                }
            });
        }
    });

    //attach click event to tbody as icon does not exist on pageload if adding new instructors
    $("#instructorTable tbody").on("click",'a.removeInstructor', function (event) {
        //console.log("removeInstructor click");
        event.preventDefault();

        var rowCnt = $("#instructorTable tbody tr").length; //is this instructor the only one
        var row = $(this).closest("tr");
        if (this.closest("tr").classList.contains("new")) {
            $(this).closest('tr').remove();
            if (rowCnt == 2) {
                //show default row
                $("#instructorTable > tbody > tr").removeClass("d-none");
            }
        } else {
            $(this).closest('tr').remove();
            if (rowCnt == 2) {
                //show default row
                $("#instructorTable > tbody > tr").removeClass("d-none");
            }

            var customerKey = $(row).find('input[id="CustomerKey"]').val();
            var instructorKey = $(row).find('input[id="InstructorKey"]').val();

            $("#successAlertBox").slideUp();
            $("#errorAlertBox").slideUp();
            $("#instructorErrorAlertBox").slideUp();
            var errorMessage = "Unable to Remove Instructor.";
            var protocol = document.location.protocol;
            var domain = document.location.host;
            var site = protocol + "//" + domain + "/also-api/instructor/remove/";

            var model = {
                InstructorKey: instructorKey,
                CustomerKey: customerKey,
            };

            $.ajax({
                type: "POST",
                url: site,
                data: JSON.stringify(model),
                contentType: "application/json; charset=utf-8"
            }).done(function (result) {
                //console.log("result: ", result);
                if (result) {
                    //do nothing
                } else {
                    $("#instructorErrorMessageParagraph").html(errorMessage);
                    $("#instructorErrorAlertBox").slideDown();
                }
            }).fail(function () {
                $("#instructorErrorMessageParagraph").html(errorMessage);
                $("#instructorErrorAlertBox").slideDown();
                //console.log("result: ", result);
            });
        }       
    });

    $('#requestCourseRoster').click( function(event) {
        event.preventDefault();
        var email = $("#Activity_ActivityDirectorEmail").val() + '; ' + $("#Activity_ActivityCoordinatorEmail").val();
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

function submit(learners, instructors, status) {
    var saveButton = $("#savePostCourseButton");
    var submitButton = $("#submitPostCourseButton");
    var completeButton = $("#completeCourseButton");
    var processingButton = $("#savingPostCourseButton");
    var postCourseErrorSpan = $("#postCourseErrorSpan");
    var site = getsiteUrl() + "/postcourse/save/";
    var errorMessage = "Due to a system error some of your current changes were unable to be saved. Please try again or call us at (800) 274-2237 for assistance in resolving the issue.";

    saveButton.hide();
    submitButton.hide();
    completeButton.hide();
    processingButton.show();

    var model = {
        ActivityKey: $("#Activity_ActivityKey").val(),
        ActivityNumber: $("#Activity_ActivityNumber").val(),
        ActivityCourseType: $("#Activity_ActivityCourseType").val(),
        ActivityEndDate: $("#Activity_ActivityCourseType").val(),
        AlsoCourseKey: $("#AlsoCourse_AlsoCourseKey").val(),
        WebLogin: $("#Customer_WebLogin").val(),
        Learners: learners,
        Instructors: instructors,
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
            $("#postCourseErrorSpan").slideDown();
            processingButton.hide();
            saveButton.show();
            submitButton.show();
            completeButton.show();
        }
    }).fail(function () {
        $("#postCourseErrorSpan").slideDown();
        processingButton.hide();
        saveButton.show();
        submitButton.show();
        completeButton.show();
    });
}

function getsiteUrl() {
    var protocol = document.location.protocol;
    var domain = document.location.host;

    return protocol + "//" + domain + "/also";
}

function addInstructor(instructorSite, instructorModel, isFirstEntry) {

    $.ajax({
        type: "POST",
        url: instructorSite,
        data: JSON.stringify(instructorModel),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: true
    }).done(function (result) {
        if (result) {
            if (isFirstEntry) {
                //hide the default row
                $("#instructorTable > tbody > tr").addClass("d-none");
                $("#instructorTable > tbody").addClass("new").append(result);
            } else {
                $("#instructorTable > tbody").addClass("new").append(result);
            }
            var id = "#" + instructorModel.CustomerId;
            $(id).addClass("new");

        } else {
            //console.log(result);
        }
    }).fail(function (result) {
        //console.log(result);
    })
};