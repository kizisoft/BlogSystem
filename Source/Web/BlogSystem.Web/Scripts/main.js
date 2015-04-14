function showJsonError(data) {
    if (data.status === 500) {
        Notifier.notifyError('Something bad happend. Please try again!');
        return;
    }

    var response = JSON.parse(data.responseText);
    Notifier.notifyError(response.errorMessage);
}