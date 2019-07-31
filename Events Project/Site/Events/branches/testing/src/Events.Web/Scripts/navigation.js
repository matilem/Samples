$(document).ready(function () {
    $("#registration-navigation-steps-select").change(function () {
        window.document.location.href = this.options[this.selectedIndex].value;
    });

    $("#registration-navigation-back-button").click(function () {
        var stepsDropdown = document.getElementById("registration-navigation-steps-select");
        var priorStepLink = stepsDropdown.options[stepsDropdown.selectedIndex - 1].value;
        window.document.location.href = priorStepLink;

        return false;
    });
});