$(document).bind("keydown", "F4", searchBox);

function searchBox() {
    //console.log('F4');
    cursorInput($(".js-search-bar"));
};

$(document).bind("keydown", "F2", shortcutBadgeNotes);
$(document).bind("keydown", "ctrl+F2", shortcutBadgeNotes);

function shortcutBadgeNotes() {
    //console.log('F10');
    cursorInput($("#Badge_Notes"));
};

$(document).bind("keydown", "F12", save);

function save(event) {
    event.preventDefault();
    //console.log('F12');
    if ($("#saveButton").length) {
        $("#saveButton").trigger("click");
        //console.log("Saving (F12)");
    }
};