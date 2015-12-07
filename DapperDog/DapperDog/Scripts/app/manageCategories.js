var categoryuri = "/api/Category";

$(document).ready(function () {
    $('#addCategoryInputDiv').hide();
    loadCategories();
    $('#btnShowAddCategoryInput').on('click', function() {
        $('#addCategoryInputDiv').show();
    });
    $('#btnSaveNewCategory').on('click', function() {
        postNewCategory();
        $('#addCategoryInputDiv').hide();
    });
    $('#btnHideAddCategoryDiv').on('click', function() {
        $('#addCategoryInputDiv').hide();
    });
});

function loadCategories() {
    $.getJSON(categoryuri + '/Get')
        .done(function(data) {
            $('#inputCategoryID option').remove(); // clears the current options from the dropdown
            $.each(data, function(index, category) {
                $(createCategoryOption(category, index)).appendTo($('#inputCategoryID')); // repopulates the dropdown with the categories from the ajax call
            });
            // call another document ready here if you want functionality for each new thing populated
        });
}

function createCategoryOption(category) {
    return '<option value="' + category.CategoryID + '">' + category.CategoryName + '</option>'; // html to add to the drop down
}

function postNewCategory() {
    var category = {};
    category.CategoryName = $('#addCategoryInput').val();
    if ($('#addCategoryInput').val() === '') {
        // function to add error message eg: "you must put text here!!"
    } else {
        $.post(categoryuri + '/Post/', category)
            .done(function() {
                loadCategories();
            })
        .fail(function(jqXhr, status, err) {
            alert(status + '-' + err);
        });
    }
}