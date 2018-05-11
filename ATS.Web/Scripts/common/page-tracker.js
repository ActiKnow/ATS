var pageTracker = (function () {
    var defaults = {
        context: null,
        strictDirtyCheck: false
    };
    var isDirty = false, isSubmittingForm = false;

    var init = function (options) {
        $.extend(true, defaults, options);

        $(":input").each(function (i, e) {
            var $item = $(e);
            $item.attr("data-original-value", $item.val());
        });

        $("form").on("submit", function () {
            isSubmittingForm = true;
        });

        $(":input").on("input", function () {
            if (defaults.strictDirtyCheck) {
                isDirty = ($(this).val() !== $(this).attr("data-original-value"));
            } else {
                isDirty = true;
            }
        });

        $(window).on("beforeunload", function () {
            if (isSubmittingForm || !isDirty) {
                return undefined;
            }
            return "Changes you made may not be saved.";
        });
    };

    return {
        init: init
    };
})();