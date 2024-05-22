function showConfirmation(message, callback) {
    $('#confirmationModal').find('.modal-body p').text(message);
    $('#confirmButton').show();
    $('#cancelButton').show();
    $('#okButton').hide();
    $('#confirmationModal').modal('show');

    $('#confirmButton').off('click').on('click', function () {
        callback(true);
        $('#confirmationModal').modal('hide');
    });

    $('#cancelButton').off('click').on('click', function () {
        callback(false);
        $('#confirmationModal').modal('hide');
    });
}

function showMessage(message) {
    $('#confirmationModal').find('.modal-body p').text(message);
    $('#confirmButton').hide();
    $('#cancelButton').hide();
    $('#okButton').show();
    $('#confirmationModal').modal('show');
}