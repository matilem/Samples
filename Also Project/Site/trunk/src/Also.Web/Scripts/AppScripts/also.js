$(document).ready(function () {
    $(".clickable-row").click(function () {
        window.location = $(this).data("href");
    });

    var successMessage = "";
    var status = getUrlParameter("status");
    if (status === "Save") {
        successMessage = "Saved Successfully!";
        $("#successMessageParagraph").html(successMessage);
        $("#successAlertBox").slideDown();
    }
    if (status === "Submit") {
        successMessage = "Submitted Successfully!";
        $("#successMessageParagraph").html(successMessage);
        $("#successAlertBox").slideDown();
    }
    if (status === "Approve") {
        successMessage = "Approved Successfully!";
        $("#successMessageParagraph").html(successMessage);
        $("#successAlertBox").slideDown();
    }
    if (status === "Complete") {
        successMessage = "Completed Successfully!";
        $("#successMessageParagraph").html(successMessage);
        $("#successAlertBox").slideDown();
    }

    var courseTable = $('#CourseListTable').DataTable({
        responsive: true,
        visible: true,
        paging: true,
        columnDefs: [
            { type: 'date-eu', targets: 3 }
        ],
        "order": []
    });
    function autoSearch(text) {
        courseTable.search(text).draw();
    }
    $("#filter-all").click(function () { autoSearch(''); });
    $("#filter-precourse").click(function () { autoSearch('Pre-Course View'); });
    $("#filter-approvalPending").click(function () { autoSearch('Approval Pending');  });
    $("#filter-approved").click(function () { autoSearch('Approved');  });
    $("#filter-completionPending").click(function () { autoSearch('Completion Pending');  });

    $("#view-filter-select").change( function() {
        var val = $(this).val();
        $("#filter-" + val).trigger("click");
    });
    

    function getUrlParameter(sParam) {
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : sParameterName[1];
            }
        }
    };

    //sort function for Activity Date
    jQuery.extend(jQuery.fn.dataTableExt.oSort, {
        "date-eu-pre": function (date) {

            //splits date between beginDate and endDate
            date = date.split(" - ", 1);

            var year;
            var eu_date = date.toString().split("/");

            /*year (optional)*/
            if (eu_date[2]) {
                year = eu_date[2];
            }
            else {
                year = 0;
            }

            /*month*/
            var month = eu_date[0];
            if (month.length === 1) {
                month = 0 + month;
            }

            /*day*/
            var day = eu_date[1];
            if (day.length === 1) {
                day = 0 + day;
            }

            return (year + month + day) * 1;
        },

        "date-eu-asc": function (a, b) {
            return ((a < b) ? -1 : ((a > b) ? 1 : 0));
        },

        "date-eu-desc": function (a, b) {
            return ((a < b) ? 1 : ((a > b) ? -1 : 0));
        }
    });
});

