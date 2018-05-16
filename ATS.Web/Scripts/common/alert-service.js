//first create  a div or partial call such as :
// <div class='alert_class' id='alertID'></div>  or    @Html.Partial("_MessageView")

// configure alert Service for div using id or class as:
//alertService.configure({ messageContext: 'divID/divClass' });

// then call these method to alert message :
//for Error: alertService.showError('message');
//for Warning : alertService.showWarning('message');
//for Success : alertService.showSuccess('message');

var alertService = (function () {
    const AlertTypes = {
        Success: 0,
        Info: 1,
        Error: 2,
        Warning: 3
    };
    var defaults = {
        messageContext: '#message-context',
        className: 'alert',
        showTitle: true,
        dismissible: true
    };
    var messageContext = defaults.messageContext;
    var $messageContext = $(messageContext);

    var init = function () {
        // Read the message context and initial variables again
        messageContext = defaults.messageContext;
        $messageContext = $(messageContext);
    };

    var configure = function (config) {
        $.extend(true, defaults, config);
        init();
    };

    var createAlert = function (classes) {
        if (classes && classes.length) {
            if (defaults.dismissible) {
                if (classes.indexOf('alert-dismissible') < 0) {
                    classes += ' alert-dismissible';
                }
            } else {
                classes.replace('alert-dismissible', '');
            }
        }
        var mAlert = $('<div class="' + defaults.className + '"/>');
        var content = '<button type="button" class="close" data-dismiss="alert">&times;</button>';
        content += '<strong class="alert-title">&nbsp;&nbsp;</strong>';
        content += '<span class="alert-message"></span>';
        mAlert.html(content);
        mAlert.addClass(classes);
        mAlert.attr('style', 'display:none;');
        return mAlert;
    }

    var showAlert = function (message, type, classes, msgContext) {
        if (msgContext) {
            messageContext = msgContext;
        } else {
            messageContext = defaults.messageContext;
        }
        $messageContext = $(messageContext);

        if ($messageContext && $messageContext.length) {
            // Check for existing alert object
            var mAlert = $(messageContext + ' .' + defaults.className);
            if (mAlert && mAlert.length) {
                mAlert.removeClass(function (index, className) {
                    return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
                });
                mAlert.addClass(classes);
            } else {
                mAlert = createAlert(classes);
                $messageContext.html(mAlert);
            }
            if (defaults.showTitle) {
                var alertTitle = $(mAlert.find('.alert-title'));
                if (alertTitle) {
                    switch (type) {
                        case AlertTypes.Success:
                            //alertTitle.text('Success! ');
                            alertTitle.addClass("fa fa-check-circle-o");
                            break;
                        case AlertTypes.Error:
                            //alertTitle.text('Error! ');
                            alertTitle.addClass("fa fa-times-circle-o");
                            break;
                        case AlertTypes.Warning:
                            alertTitle.addClass('fa fa-exclamation-triangle');
                            break;
                        case AlertTypes.Info:
                            alertTitle.addClass('fa fa-info-circle');
                            break;
                    }
                }
            }
            var span = $(mAlert.find(".alert-message"));
            if (span && span.length) {
                span.text(message);
            }
            mAlert.show();
        } else {
            console.error("Message context not found");
        }
    };

    var hide = function (msgContext) {
        if (msgContext) {
            messageContext = msgContext;
        } else {
            messageContext = defaults.messageContext;
        }
        var mAlert = $(messageContext + ' .alert');
        if (mAlert) {
            mAlert.hide();
        }
    };

    var hideAll = function () {
        var mAlerts = $('.alert');
        if (mAlerts && mAlerts.length) {
            for (var mAlert in mAlerts) {
                mAlert.hide();
            }
        }
    };

    var showSuccess = function (message, messageContext) {
        showAlert(message, AlertTypes.Success, 'alert-success alert-dismissible', messageContext);
    };
    var showAllSuccess = function (messageList, messageContext) {
        var msg = " ";
        if (messageList && messageList.length > 0) {
            $.each(messageList, function (index, value) {
                msg += value;
            });
            showAlert(msg, AlertTypes.Success, 'alert-success alert-dismissible', messageContext);
        }
    };
    var showInfo = function (message, messageContext) {
        showAlert(message, AlertTypes.Info, 'alert-info alert-dismissible', messageContext);
    };

    var showWarning = function (message, messageContext) {
        showAlert(message, AlertTypes.Warning, 'alert-warning alert-dismissible', messageContext);
    };

    var showError = function (message, messageContext) {
        showAlert(message, AlertTypes.Error, 'alert-danger alert-dismissible', messageContext);
    };

    var showAllErrors = function (messageList, messageContext) {
        var msg = " ";
        if (messageList && messageList.length > 0) {
            $.each(messageList, function (index, value) {
                msg += value;
            });
        showAlert(msg, AlertTypes.Error, 'alert-danger alert-dismissible', messageContext);
        }
    }
    return {
        configure: configure,
        showInfo: showInfo,
        showSuccess: showSuccess,
        showAllSuccess: showAllSuccess,
        showWarning: showWarning,
        showError: showError,
        showAllErrors: showAllErrors,
        hide: hide,
        hideAll: hideAll
    }
})();
