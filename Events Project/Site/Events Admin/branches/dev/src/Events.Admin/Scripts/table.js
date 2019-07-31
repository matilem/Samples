$(document).ready(function () {

    /* Highlights selected input boxes */
    if ($('.qr-table').length > 0) {
        //console.log('class exists');
        $('.qr-table .qr-input__checkbox').change(function () {
            if ($(this).is(":checked")) {
                $(this).parents('.qr-table__row').addClass('qr-table--state-selected');
                //console.log("checked");
            } else {
                $(this).parents('.qr-table__row').removeClass('qr-table--state-selected');
                //console.log("unchecked");
            }
        });
        $('.qr-table .qr-input__radio').change(function () {
            if ($(this).is(":checked")) {
                $(this).parents('.qr-table').find('.qr-table--state-selected').removeClass('qr-table--state-selected');
                $(this).parents('.qr-table__row').addClass('qr-table--state-selected');
                //console.log("checked");
            }
        });
        $('.qr-table .qr-input__select').change(function () {
            if ($(this).val() !== "0") {
                $(this).parents('.qr-table__row').addClass('qr-table--state-selected');
                //console.log("checked");
            } else {
                $(this).parents('.qr-table__row').removeClass('qr-table--state-selected');
                //console.log("unchecked");
            }
        });
    }
});