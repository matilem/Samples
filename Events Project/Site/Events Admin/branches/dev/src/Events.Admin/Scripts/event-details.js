function refreshRegTypeButton() {
    var eventKey = $("#EventToAddKey").val();
    var customerKey = $("#addEventCustomerKey").val();
    var registrationDate = $("#registrationDate").val();

    $("#newEventDetail")
        .html('<div class="qr-input"><p style="text-align: center"><i class="fa fa-spinner fa-pulse"></i> Loading...</p><div></div></div>');
    $.get("event/" + eventKey + "/customer/" + customerKey + "?registrationDate=" + registrationDate)
        .done(function(data) {
            $("#newEventDetail").html(data);
            $("#registrationDate").val(registrationDate);
        })
        .fail(function() {
            $("#newEventDetail").html = "An error was encountered while trying to retrive the event data.";
        });
}

$(document)
    .ready(function() {

        if (document.querySelector("[name=feeRadioButton]") != null) {
            document.querySelector("[name=feeRadioButton]").checked = true;
        }

        $("#earlyRegistrationLink")
            .on("click",
                function(event) {
                    event.preventDefault();

                    var earlyRegistrationDate = $("#CutOffDateDisplay").val();
                    $("#registrationDate").val(earlyRegistrationDate);
                    refreshRegTypeButton();
                });

        $("#refreshRegTypeButton")
            .on("click",
                function(event) {
                    event.preventDefault();
                    refreshRegTypeButton();
                });

        $("#addNewEventButton")
            .click(function(event) {
                event.preventDefault();
                $("#addNewEventButton").hide();
                $("#saveNewEvent").show();

                var eventKey = $("#EventToAddKey").val();
                var customerKey = $("#addEventCustomerKey").val();
                var registrationDate = $("#registrationDate").val();
                var registrationTypeKey = $("input[name=feeRadioButton]:checked").val();
                var site = getsiteUrl();

                $.get("get-registrant-details/event/" + eventKey + "/customer/" + customerKey,
                    function(data) {
                        if (data.Key !== "00000000-0000-0000-0000-000000000000") {
                            window.location.href = site + "/registration/confirmation/" + data.Key;
                            return;
                        }

                        if (registrationTypeKey == undefined || registrationDate === "" || eventKey === "") {
                            $("#addNewEventError")
                                .html('<div class="alert-box -warning"><div class="alert-box__icon"><span class="fa fa-exclamation-circle fa-2x -white"></span></div><div class="alert-box__content">Please select a registration date and type.</div></div>');
                        } else {
                            window.location.href = "new?eventKey=" +
                                eventKey +
                                "&customerKey=" +
                                customerKey +
                                "&registrationTypeKey=" +
                                registrationTypeKey +
                                "&registrationDate=" +
                                registrationDate;
                        }
                    }
                );

            });
    });