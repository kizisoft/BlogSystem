$(document).ready(function () {
    $('.radio').on('change', 'input', function (e) {
        $('.pic-source').addClass('hidden');
        $('#' + this.value).removeClass('hidden');
    });

    $('#uploader').on('submit', function (e) {
        var source = this.elements.namedItem('source').value;
        if (source == 'none') {
            $('#user-pic').addClass('hidden');
            $('#user-glyph').removeClass('hidden');
            $('#AvatarUrl').val('');
            $('.close').trigger('click');
            return false;
        }

        if (!this.elements.namedItem(source).value) {
            showErrorHolder('#user-error', 'Choose a file or enter an URL!');
            return false;
        }

        $('#user-pic-loader').css('display', 'inline-block');
        var formData = new FormData(this);
        var xhr = new XMLHttpRequest();

        $(xhr).on('load', function (e) {
            if (this.status != 200) {
                showErrorHolder('#user-error', e.target.statusText);
                return false;
            }

            showAvatar(e.target.responseText);
        }).error(function (e) {
            showErrorHolder('#user-error', 'There was an error attempting to upload the file.');
        });

        if (source === 'file') {
            formData.append('file', this.elements.namedItem('file').files[0]);
        }

        xhr.open("POST", this.action, true);
        xhr.send(formData);
        return false;
    })

    function showErrorHolder(selector, message) {
        var $element = $(selector);
        $element.text(message).fadeIn().delay(3000).fadeOut(function () {
            $element.text('');
        });
        $('#user-pic-loader').css('display', '');
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