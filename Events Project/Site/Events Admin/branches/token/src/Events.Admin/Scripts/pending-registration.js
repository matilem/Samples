﻿$(document).ready(function () {

    if (document.querySelector('[name=feeRadioButton]') != null) {
        document.querySelector('[name=feeRadioButton]').checked = true;
    }

    var editRegTypeDialog = $("#registrationTypeEditDialog");

    $(".qr-guest-badge").each(function() {
        var name = $(this).find("input[id$='Name']");

        if (name.val() === "") {
            $(this).hide();
        }
    });

    editRegTypeDialog.dialog({
        bgiframe: true,
        autoOpen: false,
        modal: true,
        width: 500,
        height: "auto",
        resizable: false,
        position: { my: "center top+100", at: "center top" },
        closeOnEscape: true,
        draggable: true,
        title: "Change Registration Type"
    });

    var oversellDialog = $("#oversellDialog");

    oversellDialog.dialog({
        bgiframe: true,
        autoOpen: false,
        modal: true,
        width: 500,
        height: "auto",
        resizable: false,
        position: { my: "center top+100", at: "center top" },
        closeOnEscape: true,
        draggable: true,
        title: "Oversell Session?"
    });

    updatePaymentTotal();

    $("#registrationTypeEditButton").on("click", function(event) {
        event.preventDefault();

        editRegTypeDialog.dialog("open");
    });

    $("#saveRegTypeEditButton").click(function (event) {
        event.preventDefault();

        updateRegistration();
    });
    $("#saveRegTypeEditButton").click(function (event) {
        event.preventDefault();
        $("#saveRegTypeEditButton").hide();
        $("#saveChangesButton").show();
    });
    $("#earlyRegistrationLink").on("click", function (event) {
        event.preventDefault();
        
        var earlyRegistrationDate = $("#Event_CutOffDateDisplay").val();
        $("#registrationDate").val(earlyRegistrationDate);
        refreshRegTypeButton();
    });

    $("#refreshRegTypeButton").on("click", function (event) {
        event.preventDefault();

        refreshRegTypeButton();
    });

    $("a[id$='_atCapacityLink']").click(function (event) {
        event.preventDefault();

        var sessionTitle = $(this).data("title");
        var sessionCode = $(this).data("code");
        var key = $(this).data("key");

        $("#oversellButton").data("key", key);
        $("#oversellTitle").html(sessionTitle + " (" + sessionCode + ") is at or above capacity. Are you sure you want to oversell?");
        oversellDialog.dialog("open");
    });

    $("#oversellCancelButton").click(function(event) {
        event.preventDefault();

        oversellDialog.dialog("close");
    });

    $("#oversellButton").click(function(event) {
        event.preventDefault();
        
        var key = $(this).data("key");
        var tableCell = $("#" + key + "_SessionCapacityTabelCell");
        var checkBox = $("#" + key + "_sessionCheckBox");
        var site = getsiteUrl();

        $.post(site + "/registration/session/" + key + "/increase-capacity", null)
        .done(function(data) {
            tableCell.html(data);
            checkBox.removeAttr("disabled");
            checkBox.prop("checked", true).change();
            oversellDialog.dialog("close");
        })
        .fail(function () {
            $("#oversellError").html("There was an error in attempting the oversell.");
        });
    });

    $("input[id$='_sessionCheckBox']").on("change", function () {
        checkForConflicts($(this));
        updatePaymentTotal();
    });

    $("select[id$='_sessionQuantityDropDown']").on("change", function() {
        updatePaymentTotal();
    });

    $("#addGuest").on("click", function(event) {
        event.preventDefault();

        var rowCount = $(".qr-guest-badge").length;
        var shown = rowCount - $(".qr-guest-badge").filter(":hidden").size() ;

        if (shown === rowCount) {
            $(this).hide();
        }else if (shown === 0) {
            $(".qr-guest-badge").slice(0, 1).show();
        } else {
            $(".qr-guest-badge").slice(shown - 1, shown + 1).show();
            if (shown === (rowCount - 1)) {
                $(this).hide();
            }
        }
    });

    $(".qr-guest-badge__remove").on("click", function (event) {
        event.preventDefault();

        if ($(this).attr("class") === "qr-guest-badge__remove") {
            $(this).html('<i class="fa fa-minus-circle"></i> Clear Data?').addClass('qr-guest-badge__confirm-remove').removeClass('qr-guest-badge__remove');
        } else {
            var tableRow = $(this).closest(".qr-guest-badge");
            tableRow.find("input:text").val("");
            $(this).html('<i class="fa fa-minus-circle"></i> Clear').addClass('qr-guest-badge__remove').removeClass('qr-guest-badge__confirm-remove');
        }
    });

    $("#saveAndPayButton").click(function(event) {
        event.preventDefault();

        $("#saveAndPayButton").hide();
        $("#savingButton").show();

        var href = $(this).attr("href");
        var data = $("#pendingRegistrationForm").serialize();
        var site = getsiteUrl();

        $.post(site + "/registration/save-pending-registration", data)
        .done(function(result) {
            window.location.href = href + result.Data;
        })
        .fail(function () {
            alert("There was an error saving your registration.");
            $("#saveAndPayButton").show();
            $("#savingButton").hide();
        });
    });

    $('.qr-table__desc-btn').click(function (event) {
        event.preventDefault();
        $(this).next('.qr-table__desc').slideToggle();
    })
});

function resetConflictDialog() {
    $("#conflictDialog").html("");
};

function updateRegistration() {
    var eventKey = $("#Event_Key").val();
    var customerKey = $("#Customer_Key").val();
    var registrationDate = $("#registrationDate").val();
    var registrationTypeKey = $("input[name$='feeRadioButton']:checked").val();

    if (registrationTypeKey == undefined)
        registrationTypeKey = $("#PriceKey").val();

    var form = $("#pendingRegistrationForm");
    var data = form.serialize();
    var site = getsiteUrl();

    $.post(site + "/registration/update-new-registration/event/" + eventKey + "/customer/" + customerKey + "/type/" + registrationTypeKey + "?newRegistrationDate=" + registrationDate, data)
    .done(function(result) {
        window.location.href = site + "/registration/pending/" + result.Data;
    })
    .fail(function() {
        alert("There was an error updating the registration type.");
    });
};

function refreshRegTypeButton() {
    var eventKey = $("#Event_Key").val();
    var customerKey = $("#Customer_Key").val();
    var registrationDate = $("#registrationDate").val();
    var site = getsiteUrl();

    $("#newEventDetail").html('<div class="qr-input"><p style="text-align: center"><i class="fa fa-spinner fa-pulse"></i> Loading...</p><div></div></div>');

    $.get(site + "/registration/event/" + eventKey + "/customer/" + customerKey + "?registrationDate=" + registrationDate)
    .done(function(data) {
        $("#newEventDetail").html(data);
        $("#registrationDate").val(registrationDate);
    })
    .fail(function() {
        $("#newEventDetail").html = "An error was encountered while trying to retrive the event data.";
    });
}

function checkForConflicts(element) {
    var key = element.data("key");
    var tableRow = $("#" + key + "_sessionTableRow");
    var conflictsString = element.data("conflicts");
    var conflicts = conflictsString.split(",");

    if (conflictsString.length > 0) {
        if (element.is(":checked")) {
            conflicts.forEach(function (entry) {
                var item = $("#" + entry + "_sessionCheckBox");
                var conflictTableRow = $("#" + entry + "_sessionTableRow");
                var conflictDiv = $("#" + key + "_" + entry + "_conflictDiv");
                var reciprocalConflictDiv = $("#" + entry + "_" + key + "_conflictDiv");

                if (item.is(":checked")) {
                    tableRow.addClass("qr-table--state-conflict");
                    conflictTableRow.addClass("qr-table--state-conflict");
                    conflictDiv.show();
                    reciprocalConflictDiv.show();

                } else {
                    conflictTableRow.removeClass("qr-table--state-conflict");
                    conflictDiv.hide();
                }
            });
        } else {
            conflicts.forEach(function (entry) {
                var conflictTableRow = $("#" + entry + "_sessionTableRow");
                var conflictDiv = $("#" + key + "_" + entry + "_conflictDiv");
                var reciprocalConflictDiv = $("#" + entry + "_" + key + "_conflictDiv");

                tableRow.removeClass("qr-table--state-conflict");
                conflictDiv.hide();
                reciprocalConflictDiv.hide();

                var otherConflicts = $("div[id$='_" + entry + "_conflictDiv']:visible");

                if (otherConflicts.length === 0) {
                    conflictTableRow.removeClass("qr-table--state-conflict");
                }
            });
        }
    }
}

function updatePaymentTotal() {
    var paymentTotal = currencyToFloat("0");
    var registrationFee = currencyToFloat($("#SelectedRegistrationFeePrice").val());
    paymentTotal = paymentTotal + registrationFee;

    $("input[id$='_sessionCheckBox']").each(function() {
        var fee = currencyToFloat($(this).data("price"));
        
        if ($(this).is(":checked")) {
            paymentTotal = paymentTotal + fee;
        } 
    });

    $("select[id$='_sessionQuantityDropDown']").each(function() {
        var fee = currencyToFloat($(this).data("price"));
        var quantity = Number($(this).val());

        if (quantity > 0) {
            var itemTotal = fee * quantity;
            paymentTotal = paymentTotal + itemTotal;
        }
    });

    $("#paymentTotal").text(floatToCurrency(paymentTotal));
}

function currencyToFloat(input) {
    if (input == undefined || typeof input === "boolean")
        return 0;

    var amount = parseFloat(input.replace(/\$/g, ""));

    if (isNaN(amount)) {
        return 0;
    } else {
        return amount;
    }
}

function floatToCurrency(input) {
    var amount = parseFloat(input).toFixed(2);

    if (isNaN(amount)) {
        return "0.00";
    } else {
        return amount;
    }
}

$(".js-mask-phone").mask("(999) 999-9999? x99999");
