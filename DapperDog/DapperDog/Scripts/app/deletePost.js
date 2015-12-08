$(document).ready(function () {
    $('.btnShowConfirmDelete').on('click', function () {
        $('#ConfirmDeleteDiv_' + $(this).val()).show();
    });
    $('.btnCancelConfirmDelete').on('click', function() {
        $('.ConfirmDelete').hide();
    });
})