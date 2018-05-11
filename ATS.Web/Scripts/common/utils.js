var utils = {
    forms: (function () {
        var getContext = function (options) {
            var context = options ? options.context : "";
            var $context = $(context);
            return $context;
        };

        /**
         * Resets all the fields, which are within the options.context property
         * @param {any} options
         *
         */
        var reset = function (options) {
            var $context = getContext(options);
            if (!$context || !$context.length) {
                console.error('The provided Context was invalid or not found');
                return;
            }
            var textFields = $(options.context + ' input[type=text]:visible');
            if (textFields && textFields.length) {
                textFields.val("");
            }

            var selectFields = $(options.context + ' select:visible');
            if (selectFields && selectFields.length) {
                $.each(selectFields, (index, field) => {
                    $(field)[0].selectedIndex = 0;
                });
            }
        };

        var resetFields = function () {
            if (!arguments || arguments.length) {
                console.warn('No fields provided');
                return;
            }

            //TODO: Add code to reset fields
        };

        return {
            reset: reset,
            resetFields: resetFields
        };
    })(),
    ui: (function () {
        var timeOutRef = 0;

        var setFocusOnElement = function (selector, applyInMillis) {
            var timeOut = applyInMillis || 500;

            var $selector = $(selector);
            if (!$selector || $selector.length === 0) {
                console.error('Invalid Selector');
                return;
            }

            if (timeOutRef) {
                clearTimeout(timeOutRef);
            }

            timeOutRef = setTimeout(function () {
                $selector.trigger('focus');
            }, timeOut);
        };

        ///Set focus on any first of input,select,table controls for visible, enabled, not readonly controls 
        var setFocusOnFirstElement = function (contextContainer) {
            var $container = $(contextContainer);
            var firstControl = $container.find('input,select,table').filter(':visible:enabled:not([readonly]):first');

            //check first control found or not
            if (firstControl.length > 0) {
                setFocusOnElement('#' + firstControl.attr('id'));
            }
        };
        return {
            setFocus: setFocusOnElement,
            setFocusOnFirst: setFocusOnFirstElement,
        };
        
    })()
}