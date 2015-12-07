$(document).ready(function () {
    $('.btnShowConfirmDelete').on('click', function() {
        $('.ConfirmDelete').show();
    });
    $('.btnCancelConfirmDelete').on('click', function() {
        $('.ConfirmDelete').hide();
    });
})