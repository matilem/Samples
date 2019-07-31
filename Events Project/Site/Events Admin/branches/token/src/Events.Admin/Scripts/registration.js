$(document).ready(function () {
    /* Highlights selected input boxes */
    if ($('.qr-input').length > 0) {
        //console.log('class exists');
        $('.qr-input__input').focus(function () {
            $(this).parents('.qr-input').addClass('-state-selected');
            console.log('state-selected added');
        });
        $('.qr-input__input').focusout(function () {
            $(this).parents('.qr-input').removeClass('-state-selected');
            console.log('state-selected removed');
        });
    }

    /* Scrolls to the top */
    $(".js-jumpToTop").click(function () {
        $("html, body").animate({ scrollTop: 0 }, "fast");
        return false;
    });

});