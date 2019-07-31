/** 
*  Borrowed from "Single Page Site with Smooth Scrolling, Highlighted Link, and Fixed Navigation" 
* http://callmenick.com/post/single-page-site-with-smooth-scrolling-highlighted-link-and-fixed-navigation
*
* Requires: scrollto.js
*
**/
$(document).ready(function () {
    /** 
     * This part does the "fixed navigation after scroll" functionality
     * We use the jQuery function scroll() to recalculate our variables as the 
     * page is scrolled/
     */
    $(window).scroll(function () {
        var window_top = $(window).scrollTop() + 12; // the "12" should equal the margin-top value for --state-stuck
        var div_top = $('.js-nav-anchor').offset().top;
        if (window_top > div_top) {
            $('.js-tabs').addClass('qr-step-tabs--state-stuck');
        } else {
            $('.js-tabs').removeClass('qr-step-tabs--state-stuck');
        }
    });

    /**
     * This part causes smooth scrolling using scrollto.js
     * We target all a tags inside the nav, and apply the scrollto.js to it.
     */
    $(".js-tabs a").click(function (evn) {
        evn.preventDefault();
        $('html,body').scrollTo(this.hash, this.hash, { offset: 1 });
    });

    /**
     * This part handles the highlighting functionality.
     * We use the scroll functionality again, some array creation and 
     * manipulation, class adding and class removing, and conditional testing
     */
    var aChildren = $(".js-tabs li").children(); // find the a children of the list items
    var aArray = []; // create the empty aArray
    for (var i = 0; i < aChildren.length; i++) {
        var aChild = aChildren[i];
        var ahref = $(aChild).attr('href');
        aArray.push(ahref);
    } // this for loop fills the aArray with attribute href values

    $(window).scroll(function () {
        var windowPos = $(window).scrollTop(); // get the offset of the window from the top of page
        var windowHeight = $(window).height(); // get the height of the window
        var docHeight = $(document).height();

        for (var i = 0; i < aArray.length; i++) {
            var theID = aArray[i];
            var divPos = $(theID).offset().top; // get the offset of the div from the top of page
            var divHeight = $(theID).height(); // get the height of the div in question
            if (windowPos >= divPos && windowPos < (divPos + divHeight)) {
                $("a[href='" + theID + "']").addClass("qr-step-tabs__tab--state-active");
            } else {
                $("a[href='" + theID + "']").removeClass("qr-step-tabs__tab--state-active");
            }
        }

        if (windowPos + windowHeight == docHeight) {
            if (!$("nav li:last-child a").hasClass("qr-step-tabs__tab--state-active")) {
                var navActiveCurrent = $(".qr-step-tabs__tab--state-active").attr("href");
                $("a[href='" + navActiveCurrent + "']").removeClass("qr-step-tabs__tab--state-active");
                $(".js-tabs li:last-child a").addClass("qr-step-tabs__tab--state-active");
            }
        }
    });
});