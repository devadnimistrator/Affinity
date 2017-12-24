
$(document).ready(function () {
    var maxHeight = 0;
    var t_elem;

    var maxHeight_p = 0;
    var t_elem_p;
    $(".home-blog").each(function () {
        t_elem = $(this).find(".inner h3").height();
        if (t_elem > maxHeight) {
            maxHeight = t_elem;
        }
        t_elem_p = $(this).find(".inner p").height();
        if (t_elem_p > maxHeight_p) {
            maxHeight_p = t_elem_p;
        }
    });
    $(".row").find(".inner h3").height(maxHeight);    
    $(".row").find(".inner p").height(maxHeight_p);
});
