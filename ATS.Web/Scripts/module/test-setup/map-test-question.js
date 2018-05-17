var mapTestQuestion = (function () {
    'use strict';
    var defaults = {};
    var action = {

    };
    var render = {
        openSelectTest: function () {
            var op = defaults;
            var $testModal = $(op.modalSelectTestContext);
            $testModal.modal('show');
        },
        opernSelectQues: function () {
            var op = defaults;
            var $quesModal = $(op.modalSelectQuesContext);
            $quesModal.modal('show');
        },
    };
    var loader = {
        loadEvents: function () {
            var op = defaults;
            var $testContext = $(op.testContext);
            var $quesContext = $(op.quesContext);

            $testContext.on('click', op.openSelectTest, render.openSelectTest);
            $quesContext.on('click', op.openSelectQues, render.opernSelectQues);
        },
    };
    var setup = function () {
        for (let index in loader) {
            if (typeof loader[index] == 'function') {
                loader[index]();
            }
        }
    };
    var init = function (config) {
        $.extend(true, defaults, config);
        setup();
    };
    return {
        init: init
    };
})();