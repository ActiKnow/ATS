var mapTestQuestion = (function () {
    'use strict';
    var defaults = {};
    var apiUrl = {
        getTests: '/Admin/TestSetup/GetTests/'
    };
    var api = (function () {
        var fireAjax = function (url, data, type) {
            var httpMethod = type || 'POST';
            return $.ajax({
                url: url,
                type: httpMethod,
                data: data,
            });
        };
        return {
            firePostAjax: function (url, data) {
                return fireAjax(url, data, "POST");
            },

            fireGetAjax: function (url, data) {
                return fireAjax(url, data, "GET");
            },
        }
    }());
    var action = {
        getTests: function () {
            api.fireGetAjax(apiUrl.getTests, { rawTests : true})
                .done((result) => {
                    if (result && result.Status) {
                        if (result.Message && result.Message.length > 0) {
                        }
                        else {
                            render.fillSelectTest(result.Data);
                        }
                    }
                    else {

                    }
                })
                .fail();
        },
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
        closeSelectTest: function () {
            var op = defaults;
            var $testModal = $(op.modalSelectTestContext);
            $testModal.modal('hide');
        },
        closeSelectQues: function () {
            var op = defaults;
            var $quesModal = $(op.modalSelectQuesContext);
            $quesModal.modal('hide');
        },
        fillSelectTest: function (data) {
            var op = defaults;
            var $testDataContext = $(op.testDataContext);
            var selectTest = "<table class='table'><thead> <tr> <th>#</th> <th>Test Description (Marks/Duration)</th><th>Category</th> <th>Type</th>  <th>Level</th> <th>Status</th><th>Action</th></tr> </thead ><tbody>";

                selectTest += "</tbody></table>";
            $testDataContext.html(selectTest);
        },
        fillSelectQuestion: function () { },
    };
    var loader = {
        loadEvents: function () {
            var op = defaults;
            var $testContext = $(op.testContext);
            var $quesContext = $(op.quesContext);
            var $selectQuesContext = $(op.selectQuesContext);
            var $selectTestContext = $(op.selectTestContext);

            $testContext.on('click', op.openSelectTest, render.openSelectTest);
            $quesContext.on('click', op.openSelectQues, render.opernSelectQues);

            $selectQuesContext.on('click', op.closeSelectQues, render.closeSelectQues);
            $selectTestContext.on('click', op.closeSelectTest, render.closeSelectTest);
        },
        loadApiData: function () {
            action.getTests();
        }
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