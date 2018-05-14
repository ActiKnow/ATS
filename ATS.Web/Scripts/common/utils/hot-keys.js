var hotKeys = (function () {
    var init = function (options) {

        bindEvents();
    };

    var bindEvents = function () {
        $(document).on("keypress", "[data-hot-key]", function (e) {
            console.log(e);
        });
    };

    return {
        init: init
    };
})();