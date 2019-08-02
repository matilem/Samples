﻿$(document).ready(function () {
    $("div[id$='PastEventsDiv']").hide();

    /* necessary for Select2 to work within a jQuery dialog */
    if ($.ui && $.ui.dialog && $.ui.dialog.prototype._allowInteraction) {
        var ui_dialog_interaction = $.ui.dialog.prototype._allowInteraction;
        $.ui.dialog.prototype._allowInteraction = function (e) {
            if ($(e.target).closest('.select2-dropdown').length) return true;
            return ui_dialog_interaction.apply(this, arguments);
        };
    }
   
    var addEventDialog = $("#addEventDialog");

    addEventDialog.dialog({
        bgiframe: true,
        autoOpen: false,
        modal: true,
        width: 500,
        height: "auto",
        resizable: false,
        position: { my: "center top+100", at: "center top" },
        closeOnEscape: true,
        draggable: true,
        title: "Select Event",
        close: function () {
            resetEventDetails();
        },
        open: function (event, ui) { $('.ui-widget-overlay').bind('click', function () { $("#addEventDialog").dialog('close'); }); }
    });

    var printDialog = $("#printDialog");

    printDialog.dialog({
        bgiframe: true,
        autoOpen: false,
        modal: true,
        width: 750,
        height: "auto",
        resizable: false,
        position: { my: "center top+100", at: "center top" },
        closeOnEscape: true,
        draggable: true,
        title: "Print Badges and Sessions",
        open: function (event, ui) { $('.ui-widget-overlay').bind('click', function () { $("#printDialog").dialog('close'); }); }
    });

    var emailDialog = $("#emailDialog");

    emailDialog.dialog({
        bgiframe: true,
        autoOpen: false,
        modal: true,
        width: 500,
        height: "auto",
        resizable: false,
        position: { my: "center top+100", at: "center top" },
        closeOnEscape: true,
        draggable: true,
        title: "Resend Email Confirmation",
        close: function () {
            $("#emailWarning").text("").hide();
        },
        open: function(event, ui) { $('.ui-widget-overlay').bind('click', function(){ $("#emailDialog").dialog('close'); }); }
    });

    /* Highlights individual when event are being hovered over */
    if ($('.qr-result-event').length > 0) {
        //console.log('class exists');
        $('.qr-result-event').hover(function () {
            $(this).parents('.qr-result-events').prev('.qr-result-header').toggleClass('-state-hover');
            //console.log('state toggled');
        });
    }

    /* Highlights selected events */
    if ($('.qr-result-event').length > 0) {
        //console.log('class exists');
        $('.qr-result-event').click(function () {
            $(this).toggleClass('-state-selected');
            //console.log('state toggled');
        });
    }

    $(".qr-result-event-more__btn").click(function (event) {
        event.preventDefault();

        var cstId = $(this).data("cstid");
        var elementId = cstId + "PastEventsDiv";
        $("#" + elementId).show();
        $(this).hide();
    });

    $("#EventToAddKey").select2({
        theme: "qr"
    });

    $("a[id$='_sendEmailButton']").click(function (event) {
        event.preventDefault();

        var registrantKey = $(this).data("registrantkey");

        $.get("email/" + registrantKey)
        .done(function (data) {
            emailDialog.html(data);
            emailDialog.dialog("open");
        })
        .fail(function () {
            emailDialog.html('<div class="alert-box -warning"><div class="alert-box__icon"><span class="fa fa-support fa-2x -white"></span></div><div class="alert-box__content"><h3>Oops!</h3><p>We are not able to retrieve information from the server at this time.</p></div></div>');
            emailDialog.dialog("open");
        });
    });

    $("a[id$='_printLink']").click(function(event) {
        event.preventDefault();

        printDialog.html("");
        var registrantKey = $(this).attr("data-registrantKey");

        $.get("print/" + registrantKey)
        .done(function (data) {
            printDialog.html(data);
            printDialog.dialog("open");
        })
        .fail(function() {
            printDialog.html('<div class="alert-box -warning"><div class="alert-box__icon"><span class="fa fa-support fa-2x -white"></span></div><div class="alert-box__content"><h3>Oops!</h3><p>We are not able to retrieve print options from the server at this time.</p></div></div>');
            printDialog.dialog("open");
        });
    });

    $("a[id$='AddEventButton']").click(function(event) {
        event.preventDefault();

        var customerName = $(this).data("name");
        var customerKey = $(this).data("cstkey");

        addEventDialog.dialog("option", "title", customerName);
        $("#addEventTitle").html("Add new event for " + customerName);
        $("#addEventCustomerKey").val(customerKey);

        addEventDialog.dialog("open");
    });

    $("#EventToAddKey").change(function () {
        $("#addNewEventButton").hide();
        var eventKey = $("#EventToAddKey").val();
        var customerKey = $("#addEventCustomerKey").val();
        var registrationDate = $.datepicker.formatDate("MM dd, yy", new Date());

        $("#newEventDetail").html('<div class="qr-input"><p style="text-align: center"><i class="fa fa-spinner fa-pulse"></i> Loading...</p><div></div></div>');

        $.get("event/" + eventKey + "/customer/" + customerKey + "?registrationDate=" + registrationDate)
        .done(function (data) {
            $("#addNewEventButton").show();
            $("#newEventDetail").html(data);
        })
        .fail(function () {
            $("#newEventDetail").html('<div class="alert-box -warning"><div class="alert-box__icon"><span class="fa fa-support fa-2x -white"></span></div><div class="alert-box__content"><h3>Oops!</h3><p>We are not able to retrieve registration fees from the server at this time.</p></div></div>');
        });
    });
});

function resetEventDetails() {
    $("#newEventDetail").html("");
    $("#EventToAddKey").val("");
    $('#addNewEventButton').hide();
};