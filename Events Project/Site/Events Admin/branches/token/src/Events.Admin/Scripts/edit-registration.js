$(document).ready(function () {
    var oversellDialog = $("#oversellDialog");

    $(".qr-guest-badge").each(function () {
        var name = $(this).find("input[id$='Name']");

        if (name.val() === "") {
            $(this).hide();
        }
    });

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

    var printDialog = $("#printDialog");

    printDialog.dialog({
        bgiframe: true,
        autoOpen: false,
        modal: true,
        width: 1000,
        height: "auto",
        resizable: false,
        position: { my: "center top+100", at: "center top" },
        closeOnEscape: true,
        draggable: true,
        title: "Print"
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
        title: "Email"
    });

    $("#atCapacityLink").click(function (event) {
        event.preventDefault();

        var sessionTitle = $(this).data("title");
        var sessionCode = $(this).data("code");
        var key = $(this).data("key");

        $("#oversellButton").data("key", key);
        $("#oversellTitle").html(sessionTitle + " (" + sessionCode + ") is at or above capacity. Are you sure you want to oversell?");
        oversellDialog.dialog("open");
    });

    $("#oversellCancelButton").click(function (event) {
        event.preventDefault();

        oversellDialog.dialog("close");
    });

    $("#oversellButton").click(function (event) {
        event.preventDefault();

        var key = $(this).data("key");
        var tableCell = $("#" + key + "_SessionCapacityTabelCell");
        var checkBox = $("#" + key + "_sessionCheckBox");
        var site = getsiteUrl();

        $.post(site + "/registration/session/" + key + "/increase-capacity", null, function (result) {
            tableCell.html(result);
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

    $("select[id$='_sessionQuantityDropDown']").on("change", function () {
        updatePaymentTotal();
    });

    $("#printLink").click(function (event) {
        event.preventDefault();

        printDialog.html("");
        var registrantKey = $("#Key").val();
        var site = getsiteUrl();

        $.get(site + "/registration/print/" + registrantKey)
        .done(function (data) {
            printDialog.html(data);
            printDialog.dialog("open");
        })
        .fail(function () {
            $("#newEventDetail").html('<div class="alert-box -warning"><div class="alert-box__icon"><span class="fa fa-exclamation-circle fa-2x -white"></span></div><div class="alert-box__content">An error occured while loading your data. Please try again.</div></div>');
        });
    });

    $("#emailButton").click(function (event) {
        event.preventDefault();

        var site = getsiteUrl();
        var registrantKey = $(this).data("registrantkey");

        $.get(site + "/registration/email/" + registrantKey)
        .done(function (data) {
            emailDialog.html(data);
            emailDialog.dialog("open");
        })
        .fail(function () {
            emailDialog.html('<div class="alert-box -warning"><div class="alert-box__icon"><span class="fa fa-support fa-2x -white"></span></div><div class="alert-box__content"><h3>Oops!</h3><p>We are not able to send the confirmation email at this time.</p></div></div>');
            emailDialog.dialog("open");
        });
    });

    $("#addGuest").on("click", function (event) {
        event.preventDefault();

        var rowCount = $(".qr-guest-badge").length;
        var shown = rowCount - $(".qr-guest-badge").filter(":hidden").size();

        if (shown === rowCount) {
            $(this).hide();
        } else if (shown === 0) {
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

    $("#saveButton").click(function (event) {
        event.preventDefault();

        var regKey = $("#Registration_Key").val();
        $("#saveButton").hide();
        $("#savingButton").show();

        var href = $(this).attr("href");
        var data = $("#editRegistrationForm").serialize();
        var site = getsiteUrl();
        var url = window.location.href.split('?')[0];
        $.post(site + "/registration/edit/save-registration/", data)
            .done(function (result) {
                window.location.href = href + result.Data;
            })
            .success(function () {              
                window.location.href = url + "?saved";
            })
        .fail(function () {
            alert("There was an error saving your registration.");
            $("#saveButton").show();
            $("#savingButton").hide();
        });
    });

    var queryString = window.location.search;
    if (queryString == "?saved") {
        var timeoutID = window.setTimeout(hideAlert, 3000);
        $("#saveWarning").text("Save Successful.").show().addClass('js-slide-up');
        function hideAlert() {
            $("#saveWarning").removeClass('js-slide-up');
        }
        
    }

});

function resetConflictDialog() {
    $("#conflictDialog").html("");
};

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

    $("input[id$='_sessionCheckBox']").each(function () {
        var fee = currencyToFloat($(this).data("price"));

        if ($(this).is(":checked") && $(this).attr("disabled") !== "disabled") {
            paymentTotal = paymentTotal + fee;
        }
    });

    $("select[id$='_sessionQuantityDropDown']").each(function () {
        var fee = currencyToFloat($(this).data("price"));
        var quantity = Number($(this).val());

        if (quantity > 0 && $(this).attr("disabled") !== "disabled") {
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
