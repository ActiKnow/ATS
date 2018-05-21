var dialogService = (function () {
    const selectors = {
        body: 'body'
    };

    const ContextType = {
        ID: 0,
        CLASS: 0
    }

    var advDefaults = {
        title: 'Confirm Action',
        message: 'Are you sure you want to perform this action',
        agreeLabel: 'Yes',
        disagreeLabel: 'No',
        onAgree: function () { },
        onDisagree: function () { }
    };

    var defaults = {
        modalContext: '#dialog_confirm',
        okLabel: 'Yes',
        cancelLabel: 'No'
    };

    var createDialog = function (title, body, okLabel, cancelLabel) {
        var contextStr = defaults.modalContext;

        if (contextStr.startsWith('#')) {
            contextStr = contextStr.substring(1);
        }

        var $modal = $('<div id="' + contextStr + '" class="modal fade" />');
        var html = '<div class="modal-dialog" role="document">'; // Modal dialog
        html += '<div class="modal-content">';  // Modal content
        html += '<div class="modal-header">';
        html += '<h5 class="modal-title">' + title + '</h5>';
        html += '<button type= "button" class="close" data-dismiss="modal" aria-label="Close" >';
        html += '<span aria-hidden="true">&times;</span></button>';
        html += '</div> '; // Modal header
        html += '<div class="modal-body">';
        html += body;
        html += '</div>';
        html += '<div class="modal-footer">'; // Modal footer
        html += '<button type="button" id="okButton" class="btn btn-primary btn-sm">' + (okLabel ? okLabel : 'OK') + '</button>';
        html += '<button type="button" id="cancelButton" class="btn btn-default btn-sm" data-dismiss="modal">' + (cancelLabel ? cancelLabel : 'Cancel') + '</button>';
        html += '</div>'; // Modal footer
        html += '</div>'; // Modal content
        html += '</div'; // Modal dialog

        $modal.html(html);
        return $modal;
    };

    var showConfirm = function (title, message, yesCallback, cancelCallback) {
        // Check if bootstrap modal is available other wise use the fallback method
        if (typeof $().modal === 'function') {
            // Check if dialog already exists
            var $dialog = $(defaults.modalContext);
            if (!$dialog.length) {
                $dialog = createDialog(title, message, defaults.okLabel, defaults.cancelLabel);
                $(selectors.body).append($dialog);
            } else {
                var titleItem = $(defaults.modalContext + ' .modal-title');
                if (titleItem && titleItem.length) {
                    titleItem.text(title);
                }
                var bodyItem = $(defaults.modalContext + ' .modal-body');
                if (bodyItem && bodyItem.length) {
                    bodyItem.html(message);
                }
            }
            // Turn events off
            $(defaults.modalContext).off("click", ' .modal-footer #okButton');
            $(defaults.modalContext).off("click", ' .modal-footer #cancelButton');

            // Turn events on
            $(defaults.modalContext).on("click", ' .modal-footer #okButton', function () {
                if (yesCallback && typeof yesCallback === 'function') {
                    yesCallback();
                }
                $dialog.modal('hide');
            });
            $(defaults.modalContext).on("click", ' .modal-footer #cancelButton', function () {
                if (cancelCallback && typeof cancelCallback === 'function') {
                    cancelCallback();
                }
            });

            // Show modal
            $dialog.modal({ backdrop: 'static', keyboard: false });
        }
        else {
            if (confirm(message)) {
                if (yesCallback && typeof yesCallback === 'function') {
                    yesCallback();
                }
            } else {
                if (cancelCallback && typeof cancelCallback === 'function') {
                    cancelCallback();
                }
            }
        }
    };

    var advConfirm = function (options) {
        $.extend(true, advDefaults, options);
        var def = advDefaults;
        defaults.okLabel = def.agreeLabel;
        defaults.cancelLabel = def.disagreeLabel;
        showConfirm(def.title, def.message, def.onAgree, def.onDisagree);
    };

    return {
        confirm: showConfirm,
        advConfirm: advConfirm
    }
})();