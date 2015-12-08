function dotdotdotCallback(isTruncated, originalContent) {
    if (!isTruncated) {
        $("a", this).remove();
    }
}

$(document).ready(function () {
    $(".ellipsis").dotdotdot({
        after: "a.more",
        watch: "window",
        height: 200,
        callback: dotdotdotCallback
    });
    $("div.ellipsis").on('click', 'a', function () {
        if ($(this).text() === "More") {
            var div = $(this).closest('div.ellipsis');
            div.trigger('destroy').find('a.more').hide();
            div.css('max-height', '');
            $("a.less", div).show();
        }
        else {
            $(this).hide();
            $(this).closest('div.ellipsis').css("max-height", "200px").dotdotdot({ after: "a.more", callback: dotdotdotCallback });
        }
    });
});