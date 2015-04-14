function showJsonError(data) {
    if (data.status === 500) {
        Notifier.notifyError('Something bad happend. Please try again!');
        return;
    }

    var response = JSON.parse(data.responseText);
    Notifier.notifyError(response.errorMessage);
}

function commentsLoaded() {
    var isLocked = false;
    $('#comments').on('click', 'a', function (e) {
        var $target = $(e.target),
            $up, $down, $element, type, path, formData, xhr;

        $up = $target.closest('.vote-up');
        $down = $target.closest('.vote-down');

        if (($up.length || $down.length) && !isLocked) {
            isLocked = true;
            e.preventDefault();
            $element = $up.length ? $up : $down;
            type = $element.data('type');
            path = (type === 'up' ? '@Url.Action("VoteUp", "Comment")' : '@Url.Action("VoteDown", "Comment")') + '/' + $element.data('id');
            xhr = new XMLHttpRequest();
            xhr.addEventListener('load', function (data) {
                if (this.status == 200) {
                    isLocked = false;
                    $target.closest('.vote').replaceWith(data.target.response);
                    Notifier.notifyInfo('Thank you for your vote!');
                } else {
                    isLocked = false;
                    showJsonError(data.target);
                }
            });
            xhr.addEventListener('error', function (error) {
                isLocked = false;
                Notifier.notifyError('Something bad happend. Please try again!');
            });

            xhr.open("POST", path, true);
            xhr.send();
        }
    });
}