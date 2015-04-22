$(document).ready(function () {
    $('.radio').on('change', 'input', function (e) {
        $('.pic-source').addClass('hidden');
        $('#' + this.value).removeClass('hidden');
    });

    $('#uploader').on('submit', function (e) {
        var source = $('input[name=source]:checked').val(),
            $sourceElement = $('input[name=' + source + ']'),
            formData, xhr;

        e.preventDefault();
        if (source == 'none') {
            $('#user-pic').addClass('hidden');
            $('#user-glyph').removeClass('hidden');
            $('#AvatarUrl').val('');
            $('.close').trigger('click');
            return;
        }

        if (!$sourceElement.val()) {
            showErrorHolder('#user-error', 'Choose a file or enter an URL!');
            return;
        }

        $('#user-pic-loader').css('display', 'inline-block');
        if (!!window.FormData) {
            Notifier.notifySuccess('FormData exists!', { isSticky: true });
            formData = new FormData(this);
        } else {
            Notifier.notifyError('FormData does not existe in your browser!', { isSticky: true }); // TO DO: Debug - to be removed
            if (!!window.FileReader) {
                Notifier.notifySuccess('FileReader exists!', { isSticky: true });
            } else {
                if (source === 'file') {
                    showErrorHolder('#user-error', 'Your browser does not support file upload! Try to add picture from the web.');
                    return;
                }
            }

            $('input[name=file]').val('');
            AJAXSubmit(this, xhrHandler);
            return;
        }

        xhr = new XMLHttpRequest();
        $(xhr).on('load', xhrHandler);

        if (source === 'file') {
            formData.append('file', $sourceElement[0].files[0]);
        }

        xhr.open('POST', this.action, true);
        xhr.send(formData);
    })

    function xhrHandler(e) {
        if (this.status != 200) {
            showErrorHolder('#user-error', e.target.statusText);
            return false;
        }

        showAvatar(e.target.responseText);
    }

    function showErrorHolder(selector, message) {
        var $element = $(selector);
        $element.text(message).fadeIn().delay(3000).fadeOut(function () {
            $element.text('');
        });

        $('#user-pic-loader').css('display', '');
        Notifier.notifyInfo(message, { isSticky: true });
    }

    function showAvatar(url) {
        $('#user-pic').attr('src', '').on('load', function (e) {
            $('#AvatarUrl').val(url);
            $('#user-pic-loader').css('display', '');
            $('.user-pic>span').addClass('hidden');
            $(this).removeClass('hidden');
            $('.close').trigger('click');
        }).error(function (error) {
            showErrorHolder('#user-error', 'Something bad happend. Please try again!');
        }).attr('src', url);
    }
});