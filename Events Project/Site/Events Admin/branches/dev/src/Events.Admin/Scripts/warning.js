$(document).ready(function () {

    $("#addToWaitListButton").click(function (event) {
        event.preventDefault();

        $("#addToWaitListButton").hide();
        $("#addingToWaitListButton").show();

        var eventKey = $("#Event_Key").val();
        var customerKey = $("#Customer_Key").val();
        var site = getsiteUrl();

        $.post(site + "/registration/add-to-wait-list/", { eventKey: eventKey, customerKey: customerKey })
        .done(function (data) {
            if (data.HasError) {
                $("#waitListWarning").children(".-warning").slideDown();
                $("#addToWaitListButton").show();
                $("#addingToWaitListButton").hide();
            } else {
                if (data.Data) {
                    $("#waitListWarning").children(".-success").slideDown();
                    $("#waitListQuestion").hide();
                    $("#addingToWaitListButton").hide();
                } else {
                    $("#waitListWarning").children(".-warning").slideDown();
                    $("#addToWaitListButton").show();
                    $("#addingToWaitListButton").hide();
                }
            }
        })
        .fail(function () {
            $("#waitListWarning").children(".-warning").slideDown();
            $("#addToWaitListButton").show();
            $("#addingToWaitListButton").hide();
        });
    });
});