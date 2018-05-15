var testBankSetup = (function () {
    'use strict';
    var defaults = {};
    const testType = AppConstant.TESTTYPE, levelType = AppConstant.LEVEL, categoryType = AppConstant.CATEGORY;
    var apiUrl = {
        getTestType: "/Admin/Setup/GetAllSubTypes/",
        getLevelType: "/Admin/Setup/GetAllSubTypes/",
        getCategoryType: "/Admin/Setup/GetAllSubTypes/",
        createTest: '/Admin/Setup/CreateTest/',
        getTests: '/Admin/Setup/GetTests/'
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
        getTestType: function () {
            api.fireGetAjax(apiUrl.getTestType, { parentTypeValue: testType })
                .done((result) => {
                    if (result && result.Status) {
                        if (result.Message && result.Message.length > 0) {
                        }
                        else {
                            render.fillSelector($(defaults.selectTestType), result.Data, "--select--", 'Value', 'Description');
                        }
                    }
                    else {

                    }
                })
                .fail();
        },
        getLevelType: function () {
            api.fireGetAjax(apiUrl.getLevelType, { parentTypeValue: levelType })
                .done((result) => {
                    if (result && result.Status) {
                        if (result.Message && result.Message.length > 0) {
                        }
                        else {
                            render.fillSelector($(defaults.selectLevel), result.Data, "--select--", 'Value', 'Description');
                        }
                    }
                    else {

                    }
                })
                .fail();
        },
        getCategoryType: function () {
            api.fireGetAjax(apiUrl.getCategoryType, { parentTypeValue: categoryType })
                .done((result) => {
                    if (result && result.Status) {
                        if (result.Message && result.Message.length > 0) {
                        }
                        else {
                            render.fillSelector($(defaults.selectCategory), result.Data, "--select--", 'Value', 'Description');
                        }
                    }
                    else {

                    }
                })
                .fail();
        },
        createTest: function () {
            var parameter = {
                CategoryTypeValue: inputs().categoryTypeValue,
                LevelTypeValue: inputs().levelTypeValue,
                Description: inputs().description,
                Instructions: inputs().instructions,
                Duration: inputs().duration,
                TestTypeValue: inputs().testTypeValue,
                TotalMarks: inputs().totalMarks
            };
            api.firePostAjax(apiUrl.createTest, parameter)
                .done()
                .fail();
        },
        getTests: function () {
            api.fireGetAjax(apiUrl.getTests, {})
                .done((result) => {
                    if (result && result.Status) {
                        if (result.Message && result.Message.length > 0) {
                        }
                        else {
                            render.fillTests(result.Data);
                        }
                    }
                    else {

                    }
                })
                .fail();
        },
    };
    var render = {
        fillSelector: function ($selector, data, defaultOption, value = 'Value', text = 'Text') {
            let options = "";
            if (defaultOption) {
                options = '<option>' + defaultOption + '</option>';
            }
            $.each(data, (indx, item) => {
                options += '<option value="' + item[value] + '">' + item[text] + '</option>';
            });
            if ($selector) {
                $selector.html(options);
            }
        },
        fillTests: function (data) {
            var testContext = $(defaults.testTableContext);
            testContext.html(data);
        },
    };
    var loader = {
        loadSelector: function () {
            action.getTestType();
            action.getLevelType();
            action.getCategoryType();
        },
        loadTest: function () { action.getTests(); },
    };
    var inputs = function () {
        return {
            categoryTypeValue: $(defaults.selectCategory).val(),
            levelTypeValue: $(defaults.selectLevel).val(),
            description: $(defaults.testDescription).val(),
            instructions: $(defaults.testInstruction).val(),
            duration: $(defaults.testDuration).val(),
            testTypeValue: $(defaults.selectTestType).val(),
            totalMarks: $(defaults.testMarks).val()
        };
    };
    var binder = {
        bindControls: function () {
            var $testContext = $(defaults.testContext);
            $testContext.on('click', defaults.createTest, action.createTest);
        },
    };
    var setup = function () {
        for (let indx in loader) {
            if (typeof loader[indx] == 'function') {
                loader[indx]();
            }
        }
        for (let indx in binder) {
            if (typeof binder[indx] == 'function') {
                binder[indx]();
            }
        }
    };
    var init = function (settings) {

        $.extend(true, defaults, settings);
        setup();
    };
    return { init: init };
})();