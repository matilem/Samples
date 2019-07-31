$(document).ready(function () {
    /* necessary for Select2 to work within a jQuery dialog */
    if ($.ui && $.ui.dialog && $.ui.dialog.prototype._allowInteraction) {
        var ui_dialog_interaction = $.ui.dialog.prototype._allowInteraction;
        $.ui.dialog.prototype._allowInteraction = function (e) {
            if ($(e.target).closest('.select2-dropdown').length) return true;
            return ui_dialog_interaction.apply(this, arguments);
        };
    }

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

    $("#EventToAddKey").select2({
        theme: "qr"
    });

    $("#EventToAddKey").change(function () {
        var eventKey = $("#EventToAddKey").val();
        var site = getsiteUrl();

        $("#newBatchEventDetail").html('<div class="qr-input"><p style="text-align: center"><i class="fa fa-spinner fa-pulse"></i> Loading...</p><div></div></div>');

        $.get(site + "/registration/batch/registrationinfo/" + eventKey)
        .done(function (data) {
            $("#newBatchEventDetail").html(data);
        })
        .fail(function () {
            $("#newBatchEventDetail").html('<div class="alert-box -warning"><div class="alert-box__icon"><span class="fa fa-support fa-2x -white"></span></div><div class="alert-box__content"><h3>Oops!</h3><p>We are not able to retrieve registration fees from the server at this time.</p></div></div>');
        });
    });

    $("#registerparticipants").click(function (event) {
        event.preventDefault();

        var eventKey = $("#EventToAddKey").val();
        var registrationDate = $("#registrationDate").val();
        var registrationTypeKey = $("input[name=feeRadioButton]:checked").val();
        var site = getsiteUrl();
        var fileval = $("#uploadfile").val();
        var filename = fileval.replace(/^.*\\/, "");

        $("#registerparticipants").hide();
        $("#savingButton").show();

        if (registrationTypeKey == undefined || registrationDate === "" || eventKey === "" || fileval === "") {
            $("#addNewEventError").html('<div class="alert-box -warning"><div class="alert-box__icon"><span class="fa fa-exclamation-circle fa-2x -white"></span></div><div class="alert-box__content">Please select an event, registration date and type, and a file to upload.</div></div>');
            $("#registerparticipants").show();
            $("#savingButton").hide();
        }
        else {
            var uploadUrl = site + "/registration/batch/registrationupload/" + eventKey + "/type/" + registrationTypeKey + "?registrationDate=" + registrationDate;
            var formData = new FormData();
            formData.append('file', $('input[type=file]')[0].files[0]);
            $.ajax({
                type: "POST",
                url: uploadUrl,
                data: formData,
                cache: false,
                contentType: false,
                processData: false
            }).success(function (response) {
                console.log(response);
                $("#batchDetails").html(response);
                $("#registerparticipants").show();
                $("#savingButton").hide();
                $("#fileName").text(filename);
            }).fail(function (response) {
                console.log(response);
                $("#registerparticipants").show();
                $("#savingButton").hide();
                alert("There was an error uploading your file at this time.");
            });
        }
    });

    $("#downloadResults").click(function (event) {
        event.preventDefault();

        $('#dtltbl').tableExport({
            type: 'csv',
            escape: 'false'
        });
    });
});

function resetEventDetails() {
    $("#newEventDetail").html("");
    $("#batchDetails").html("");
    $("#EventToAddKey").val("");
    $("#addNewEventButton").hide();
};