/* $(document).bind('keydown', 'F1', testFN);


function testFN() {
    console.log('hello world');
}; */

$(document).bind('keydown', 'F4', searchBox);

function searchBox() {
    //console.log('F4');
    cursorInput($('.js-search-bar'));
};


$(document).bind('keydown', 'F2', shortcutBadgeNotes);
$(document).bind('keydown', 'ctrl+F2', shortcutBadgeNotes);

function shortcutBadgeNotes() {
    //console.log('F10');
    cursorInput($('#Badge_Notes'));
};

$(document).bind('keydown', 'F12', saveAndPay);

function saveAndPay(event) {
    event.preventDefault();
    //console.log('F12');
    if ($('#saveAndPayButton').length) {
        $('#saveAndPayButton').trigger("click");
        //console.log("Saving and moving to payment screen (F12)");
    } else {
        //console.log("That keyboard shortcut does not exist on this page");
    }
};

$(document).bind('keydown', 'F12', save);

function save(event) {
    event.preventDefault();
    //console.log('F12');
    if ($('#saveButton').length) {
        $('#saveButton').trigger("click");
        //console.log("Saving (F12)");
    } else {
        //console.log("That keyboard shortcut does not exist on this page");
    }
};

/* Bug: Won't trigger the button click 
$(document).bind('keydown', 'esc', cancelButton);

function cancelButton() {
    if ($('#cancelButton').length) {
        $('#cancelButton').trigger("click");
        console.log("Canceling (esc)");
    } else {
        console.log("That keyboard shortcut does not exist on this page");
    }
};
*/