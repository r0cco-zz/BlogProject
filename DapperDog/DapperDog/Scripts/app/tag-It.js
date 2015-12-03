var tags = [];
$.getJSON('/api/Tag')
    .done(function(data) {
        $.each(data, function(index, tag) {
            tags.push(tag.TagName);
        });
    });


$(document).ready(function () {
    $("#myTags").tagit({
        caseSensitive: false,
        placeholderText: "Enter your tags here!",
        availableTags: tags    
    });
});