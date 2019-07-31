$(document).ready(function () {

    $("tr[id$='_currentRegistrationTableRow']").slice(10, $("tr[id$='_currentRegistrationTableRow']").length).hide();

    $("#moreResults").click(function(event) {
        event.preventDefault();
        $("tr[id$='_currentRegistrationTableRow']").show();
    });
});
$(".session__load-more").click(function () {
    $(".session__load-more").hide();
});