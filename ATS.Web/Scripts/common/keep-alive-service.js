var keepAlive = (function () {
    const POLL_FREQUENCY = 8000;
    var intervalTracker = null;
    var poll = function () {
        apiBase.fireAjax('/account/keepalive', null, null, false).done(res => {
            if (!res.Status) {
                window.location.href = '/Account/Logout/';
            }
        });
    };

    var init = function (disabled) {
        if (!disabled || disabled !== true) {
            if (!window.apiBase || !apiBase) {
                console.error("Api Base not available");
            } else {
                if (intervalTracker) {
                    clearInterval(intervalTracker);
                }
                intervalTracker = setInterval(poll, POLL_FREQUENCY);
            }
        }
    };

    return {
        init: init
    }
})();