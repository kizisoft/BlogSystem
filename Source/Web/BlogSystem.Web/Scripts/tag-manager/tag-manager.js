$(document).ready(function () {
    var $root = $('.tags-root'),
        $input = $('.tag-input'),
        $info = $('#tag-info'),
        $tags = $('#tags'),
        isTagOperation = false,
        isFocusLooseInProgress = false;

    if ($input.length > 0) {
        $root.on('keypress', onKeypressHandler);
        $root.on('click', '.glyphicon', removeTagHandler);
        $root.on('click', takeRootFocusHandler);
        $root.on('focus', takeRootFocusHandler);
    } else {
        $input = $('#tag-search');
        $('#tag-submit').on('click', onTagSubmitHandler);
    }

    $input.on('focus', takeFocusHandler);
    $input.on('blur', looseFocusHandler);
    $input.on('input', onInputHandler);
    $tags.on('click', 'a', selectTagHandler);

    function onKeypressHandler(e) {
        if (e.keyCode === 13) {
            e.preventDefault();
            var val = $input.val().toLowerCase();
            if (getUniqueTags([{ Name: val }], true) !== null) {
                $root.prepend($('<span class="tag">\n' + val +
                    '\n<span class="glyphicon glyphicon-remove"></span></span><input class="tag-name" type="hidden" name="Tags[' +
                    $root.find('.tag-name').length + '].Name" value="' + val + '" />'));
                $input.val('');
                checkInput();
            }
        }
    }

    function removeTagHandler(e) {
        var $span = $(this).parent();
        e.preventDefault();
        checkInput();
        $span.next('input').remove();
        $span.remove();
        var $inputs = $root.find('.tag-name');
        for (var i = 0, length = $inputs.length; i < length; i++) {
            $inputs[i].name = 'Tags[' + i + '].Name';
        }
    }

    function takeRootFocusHandler(e) {
        $input.focus();
        if (isFocusLooseInProgress) {
            isTagOperation = true;
        }
    }

    function takeFocusHandler(e) {
        $root.addClass('focused');
        $info.removeClass('hidden');
    }

    function looseFocusHandler(e) {
        isFocusLooseInProgress = true;
        setTimeout(function () {
            isFocusLooseInProgress = false;
            if (isTagOperation) {
                isTagOperation = false;
            } else {
                $root.removeClass('focused');
                $info.addClass('hidden');
            }

            $tags.addClass('hidden');
        }, 300);
    }

    function selectTagHandler(e) {
        var tagName = $(this).data('name');
        $input.val(tagName);
        isTagOperation = true;
    }

    function onInputHandler(e) {
        var path = '/Tags/Find/' + $input.val(),
            xhr = new XMLHttpRequest();
        $(xhr).on('load', onLoadHandler);
        xhr.open('GET', path, true);
        xhr.send();
    }

    function onTagSubmitHandler(e) {
        var form = $('#tag-form')[0];
        form.action = form.action + $input.val();
        form.submit();
    }

    function onLoadHandler(e) {
        if (e.target.status !== 200) {
            showJsonError(e.target)
            return false;
        }

        var tags = JSON.parse(e.target.response),
            uniqueTags = getUniqueTags(tags, false),
            tagsCount = uniqueTags.length;
        $tags.find('li').remove();
        if (tagsCount > 0) {
            $tags.removeClass('hidden');
            for (var i = 0; i < tagsCount; i++) {
                $tags.append('<li><a data-name="' + uniqueTags[i] + '"><span class="glyphicon glyphicon-tag"></span>' + uniqueTags[i] + '</a></li>');
            }
        } else {
            $tags.addClass('hidden');
        }
    }

    function checkInput() {
        var tagcCount = $root.find('.tag-name').length;
        if (tagcCount == 3) {
            $input.toggleClass('hidden');
        }
    }

    function getUniqueTags(tags, isWarningOn) {
        var tagsNew = {},
            $inputs = $root.find('.tag-name'),
            length = $inputs.length;

        if (length > 0) {
            for (var i = 0; i < length; i++) {
                for (var t = 0, count = tags.length; t < count; t++) {
                    if ($inputs[i].value === tags[t].Name) {
                        if (isWarningOn) {
                            Notifier.notifyWarning('The tag with name "' + tags[t].Name + '" already added!');
                            return null;
                        }

                        tagsNew[tags[t].Name] = false;
                        continue;
                    }

                    tagsNew[tags[t].Name] = tagsNew[tags[t].Name] === undefined ? true : tagsNew[tags[t].Name];
                }
            }
        } else {
            for (var t = 0, count = tags.length; t < count; t++) {
                tagsNew[tags[t].Name] = true;
            }
        }

        var tagsResult = [];
        for (var tag in tagsNew) {
            if (tagsNew.hasOwnProperty(tag) && tagsNew[tag]) {
                tagsResult.push(tag);
            }
        }

        return tagsResult.sort();
    }
});