$(document).ready(function () {
    $('input').hover(function () {
        $(this).prev().addClass('hover');
    },
      function () {
          $(this).prev().removeClass('hover');
      }
    );
});

function cursorInput(field) {
    if (field.length) {
        // Multiply by 2 to ensure the cursor always ends up at the end;
        // Opera sometimes sees a carriage return as 2 characters.
        var strLength = field.val().length * 2;
        //console.log("testing changes 3");
        field.focus();
        field[0].setSelectionRange(strLength, strLength);
        $(field).parents('.qr-input').addClass('-state-selected');
    } else {
        console.log("That keyboard shortcut does not exist on this page");
    }
}

function getsiteUrl() {
    var protocol = document.location.protocol;
    var domain = document.location.host;

    return protocol + "//" + domain + "/events-admin";
}
