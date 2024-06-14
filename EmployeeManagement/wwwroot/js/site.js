$(document).ready(function () {
    // Get current URL path
    var path = window.location.pathname;

    $('#menu li a').each(function () {
        var href = $(this).attr('href');
        if (path === href) {
            $(this).parent().addClass('active');
        } else {
            $(this).parent().removeClass('active');
        }
    });
});