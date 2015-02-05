$(document).ready(function () {
    var $blogPostImg = $('.article-body img');
    $blogPostImg.addClass('img-responsive img-bordered');
    for (var i = 0, length = $blogPostImg.length; i < length; i++) {
        var float = $($blogPostImg[i]).css("float");
        if (float === "left") {
            $($blogPostImg[i]).addClass('margin-right-15px');
        } else if (float === "right") {
            $($blogPostImg[i]).addClass('margin-left-15px');
        }
    }
});