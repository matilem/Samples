$(document).ready(function () {

    $(".js-jumpToTop").click(function () {
        $("html, body").animate({ scrollTop: 0 }, "fast");
        return false;
    });
});