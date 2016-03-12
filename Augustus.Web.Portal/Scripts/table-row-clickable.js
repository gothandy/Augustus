$(document).ready(function () {

    $('table.table-hover tr').click(function () {
        var href = $(this).find("a").attr("href");
        if (href) {
            window.location = href;
        }
    });

});