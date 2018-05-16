var testBankSetup = (function () {
    'use strict';
    var defaults = {};
    const update = "Update", create = "Create";
    var testData = {} ;
    const testType = AppConstant.TESTTYPE, levelType = AppConstant.LEVEL, categoryType = AppConstant.CATEGORY;
    var apiUrl = {
        getTestType: "/Admin/Setup/GetAllSubTypes/",
        getLevelType: "/Admin/Setup/GetAllSubTypes/",
        getCategoryType: "/Admin/Setup/GetAllSubTypes/",
        createTest: '/Admin/TestSetup/CreateTest/',
        updateTest: '/Admin/TestSetup/UpdateTest/',
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
        createOrUpdateTest: function (e) {
            if (!validationService.validateForm({ messageContext: defaults.popupMessageContext })) {
                return false;
            }
            var input = inputs();
            var parameter = {
                CategoryTypeValue: input.categoryTypeValue.val(),
                LevelTypeValue: input.levelTypeValue.val(),
                Description: input.description.val().trim(),
                Instructions: input.instructions.val().trim(),
                Duration: input.duration.val().trim(),
                TestTypeValue: input.testTypeValue.val(),
                TotalMarks: input.totalMarks.val().trim(),
                StatusId: input.status.val(),
            };
            let url = apiUrl.createTest;
            if (testData.id) {
                url = apiUrl.updateTest;
                $.extend(true, parameter, { TestBankId: testData.id });
            }
            api.firePostAjax(url, parameter)
                .done((result) => {
                    if (result.Status) {
                        action.getTests();
                        render.closeTestSetupPopup();
                        alertService.showAllSuccess(result.Message, defaults.mainMessageContext);
                    }
                    else {
                        alertService.showAllErrors(result.Message, defaults.popupMessageContext);
                    }
                })
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
        resetTestData: function () {
            return {
                id: '',
                categoryValue: '',
                typeValue: '',
                levelValue: '',
                marks: '',
                description: '',
                instruction: '',
                duration: '',
                status: 'true',
            };
        },
    };
    var render = {
        fillSelector: function ($selector, data, defaultOption, value = 'Value', text = 'Text') {
            let options = "";
            if (defaultOption) {
                options = '<option value>' + defaultOption + '</option>';
            }
            $.each(data, (indx, item) => {
                options += '<option value="' + item[value] + '">' + item[text] + '</option>';
            });
            if ($selector) {
                $selector.html(options);
            }
        },
        fillTests: function (data) {
            var $testContext = $(defaults.testTableContext);
            $testContext.html(data);
            $testContext.on('click', defaults.testEdit, render.setEditTest);
        },
        openTestSetupPopup: function () {
            alertService.hide(defaults.mainMessageContext);
            validationService.removeValidationError({ messageContext: defaults.popupMessageContext });
            var $modalTest = $(defaults.modalTestContext);
            render.setTestControls();
            $modalTest.modal('show');
            var $create = $(defaults.createTest);
            $create.html(create);
        },
        closeTestSetupPopup: function () {
            var $modalTest = $(defaults.modalTestContext);
            testData = action.resetTestData();
            render.setTestControls();
            $modalTest.modal('hide');
        },
        setTestControls: function (data=null) {
            data = data || action.resetTestData();
            var input = inputs();
            input.categoryTypeValue.val(data.categoryValue);
            input.levelTypeValue.val(data.levelValue);
            input.description.val(data.description);
            input.instructions.val(data.instruction);
            input.duration.val(data.duration);
            input.testTypeValue.val(data.typeValue);
            input.totalMarks.val(data.marks);
            input.status.val(data.status);
        },
        setEditTest: function (e) {
            var op = defaults;
            var $row = $(e.target).closest('tr');
            testData = action.resetTestData();
            testData.id = $row.find(op.testId).val();
            testData.categoryValue = $row.find(op.categoryValue).val();
            testData.typeValue = $row.find(op.typeValue).val();
            testData.levelValue = $row.find(op.levelValue).val();
            testData.marks = $row.find(op.testMarks).val();
            testData.description = $row.find(op.testDescription).val();
            testData.instruction = $row.find(op.testInstruction).val();
            testData.duration = $row.find(op.testDuration).val();
            testData.status = $row.find(op.testStatus).val();

            render.openTestSetupPopup();
            render.setTestControls(testData);
            var $update = $(defaults.createTest);
            $update.html(update);
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
            categoryTypeValue: $(defaults.selectCategory),
            levelTypeValue: $(defaults.selectLevel),
            description: $(defaults.testDescriptionInput),
            instructions: $(defaults.testInstructionInput),
            duration: $(defaults.testDurationInput),
            testTypeValue: $(defaults.selectTestType),
            totalMarks: $(defaults.testMarksInput),
            status: $(defaults.testStatusInput),
        };
    };

    var binder = {
        bindControls: function () {
            var $testContext = $(defaults.testContext);
            var testListContext = $(defaults.testListContext);
            $testContext.on('click', defaults.createTest, action.createOrUpdateTest);
            $testContext.on('click', defaults.cancelTestSetup, render.closeTestSetupPopup);
            testListContext.on('click', defaults.openSetup, render.openTestSetupPopup);
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