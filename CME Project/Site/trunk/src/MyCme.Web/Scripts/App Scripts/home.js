(function ($, window, document) {
    var errorHtml = "<p>We are unable to retrieve your items at this time.";
    var cmeStatsDiv = $("#cmeStatsDiv");
    var cardsDiv = $("#cardsDiv");

    var mobileTabSelect = $("#js-tab-mobile");

    var allTab = $("#js-tab-all");
    var purchasedTab = $("#js-tab-purchased");
    var subscriptionTab = $("#js-tab-subscriptions");
    var freeTab = $("#js-tab-free");
    var completedTab = $("#js-tab-completed");

    window.onhashchange = function() {
        var hash = location.hash;

        if (hash === "#all") {
            showAll(allTab, cardsDiv, errorHtml);
        }
        else if (hash === "#purchased") {
            showPurchased(purchasedTab, cardsDiv, errorHtml);
        }
        else if (hash === "#subscriptions") {
            showSubscriptions(subscriptionTab, cardsDiv, errorHtml);
        }
        else if (hash === "#free") {
            showFree(freeTab, cardsDiv, errorHtml);
        }
        else if (hash === "#completed") {
            showCompleted(completedTab, cardsDiv, errorHtml);
        }
        analytics.trackCMEDashboardPageView();
    }

    if (cmeStatsDiv.length) {
        getCmeStats().done(function (data) {
            cmeStatsDiv.html(data);
        });
    }

    mobileTabSelect.on("change", function () {
        var selectedItem = $("#js-tab-mobile option:selected").text();

        if (selectedItem === "All") {
            location.hash = "#all";
        }
        else if (selectedItem === "Purchased") {
            location.hash = "#purchased";
        }
        else if (selectedItem === "Subscriptions") {
            location.hash = "#subscriptions";
        }
        else if (selectedItem === "Free") {
            location.hash = "#free";
        }
        else if (selectedItem === "Completed") {
            location.hash = "#completed";
        }
    });

    // purchased should be selected on page load unless location hash is present
    if (location.hash) {
        var desktopHash = location.hash;
        var mobileHash = location.hash.substr(1);
        mobileHash = mobileHash.charAt(0).toUpperCase() + mobileHash.slice(1);

        $("a[href='" + desktopHash + "']").click();
        mobileTabSelect.val(mobileHash);

        if (desktopHash === "#all") {
            showAll(allTab, cardsDiv, errorHtml);
        } else if (desktopHash === "#purchased") {
            showPurchased(purchasedTab, cardsDiv, errorHtml);
        } else if (desktopHash === "#subscriptions") {
            showSubscriptions(subscriptionTab, cardsDiv, errorHtml);
        } else if (desktopHash === "#free") {
            showFree(freeTab, cardsDiv, errorHtml);
        } else if (desktopHash === "#completed") {
            showCompleted(completedTab, cardsDiv, errorHtml);
        }
    } else {
        setCurrentTab(purchasedTab);
        setTabTitle("Purchased");
        analytics.addCmeReportingTracking();
    }

    // base page tracking for adobe analytics
    analytics.trackCMEDashboardPageView();
    
}(window.jQuery, window, document));

function getCmeStats() {
    return $.ajax({
        url: "/my-cme/stats",
        type: "GET"
    });
}

function getCmeCardTotals() {
    return $.ajax({
        url: "/my-cme/cards/totals",
        type: "GET"
    });
}

function getAllCmeCards() {
    return $.ajax({
        url: "/my-cme/cards/all",
        type: "GET"
    });
}

function getPurchasedCmeCards() {
    return $.ajax({
        url: "/my-cme/cards/purchased",
        type: "GET"
    });
}

function getSubscriptionCmeCards() {
    return $.ajax({
        url: "/my-cme/cards/subscription",
        type: "GET"
    });
}

function getFreeCmeCards() {
    return $.ajax({
        url: "/my-cme/cards/free",
        type: "GET"
    });
}

function getCompletedCmeCards() {
    return $.ajax({
        url: "/my-cme/cards/completed",
        type: "GET"
    });
}

function showAll(tab, div, message) {
    displaySpinner(true);
    setCurrentTab(tab);
    setTabTitle("All");
    getAllCmeCards().done(function (data) {
        div.html(data);
        analytics.addCmeReportingTracking();
        displaySpinner(false);
    }).fail(function () {
        displaySpinner(false);
        div.html(message);
    });
}

function showPurchased(tab, div, message) {
    displaySpinner(true);
    setCurrentTab(tab);
    setTabTitle("Purchased");
    getPurchasedCmeCards().done(function (data) {
        div.html(data);
        analytics.addCmeReportingTracking();
        displaySpinner(false);
    }).fail(function () {
        displaySpinner(false);
        div.html(message);
    });
}

function showSubscriptions(tab, div, message) {
    displaySpinner(true);
    setCurrentTab(tab);
    setTabTitle("Subscriptions");
    getSubscriptionCmeCards().done(function (data) {
        div.html(data);
        analytics.addCmeReportingTracking();
        displaySpinner(false);
    }).fail(function () {
        displaySpinner(false);
        div.html(message);
    });
}

function showFree(tab, div, message) {
    displaySpinner(true);
    setCurrentTab(tab);
    setTabTitle("Free");
    getFreeCmeCards().done(function (data) {
        div.html(data);
        analytics.addCmeReportingTracking();
        displaySpinner(false);
    }).fail(function () {
        displaySpinner(false);
        div.html(message);
    });
}

function showCompleted(tab, div, message) {
    displaySpinner(true);
    setCurrentTab(tab);
    setTabTitle("Completed");
    getCompletedCmeCards().done(function (data) {
        div.html(data);
        analytics.addCmeReportingTracking();
        displaySpinner(false);
    }).fail(function () {
        displaySpinner(false);
        div.html(message);
    });
}

function setCurrentTab(newCurrentTab) {
    var oldCurrentTab = $(".tab__current");
    oldCurrentTab.removeClass("tab__current");

    newCurrentTab.addClass("tab__current");
}

function setTabTitle(title) {
    var tabTitle = $("#tabTitle");
    tabTitle.html(title);
}

function displaySpinner(show) {
    if (show) {
        $("#spinner").show();
        $("#loadedContent").hide();
    } else {
        $("#spinner").hide();
        $("#loadedContent").show();
    }
}