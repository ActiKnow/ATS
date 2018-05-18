var mapTestQuestion = (function () {
    'use strict';
    var defaults = {};
    const activeText = AppConstant.ACTIVE, inactiveText = AppConstant.INACTIVE;
    var selectedTest = {};
    var selectedQues = [];
    var apiUrl = {
        getTests: '/Admin/TestSetup/GetTests/',
        getQuestions: '/Admin/Setup/GetQuestionList/',
        mapQuestions: '/Admin/TestSetup/LinkTestQuestion/',
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
            api.fireGetAjax(apiUrl.getTests, { rawTests: true })
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
        getQuestions: function () {
            api.fireGetAjax(apiUrl.getQuestions, { rawQuestion: true })
                .done((result) => {
                    if (result && result.Status) {
                        if (result.Message && result.Message.length > 0) {
                        }
                        else {
                            render.fillSelectQuestion(result.Data);
                        }
                    }
                    else {

                    }
                })
                .fail();
        },
        setSelectedTest: function ($row) {
            var op = defaults;
            selectedTest = action.resetSelectedTest();
            selectedTest.id = $row.find('.' + op.testId).val();
            selectedTest.description = $row.find('.' + op.testDesc).text();
            selectedTest.category = $row.find('.' + op.testCategory).text();
            selectedTest.type = $row.find('.' + op.testtestDescType).text();
            selectedTest.level = $row.find('.' + op.testLevel).text();
            selectedTest.status = $row.find('.' + op.testStatus).text();
            render.fillSelectedTest(selectedTest);
            render.closeSelectTest();
        },
        resetSelectedTest: function () {
            return {
                id: "",
                description: "",
                category: '',
                type: '',
                level: '',
                status: ''
            };
        },
        getEmptyQues: function () {
            return {
                id: "",
                description: "",
                category: '',
                type: '',
                level: '',
                marks: 0,
            };
        },
        getEmptyMapping: function () {
            return {
                //Id: "",
                TestBankId: "",
                QId: '',
                Marks: 0,
            };
        },
        addQuestions: function (question) {
            selectedQues.push(question)
        },
        removeQuestions: function (question) {
            selectedQues.splice(selectedQues.indexOf(question), 1);
        },
        mapQuestions: function () {
            var linkQuestions = [];
            for (let indx in selectedQues) {
                let map = action.getEmptyMapping();
                map.TestBankId = selectedTest.id;
                map.QId = selectedQues[indx].id;
                map.Marks = selectedQues[indx].marks;
                linkQuestions.push(map);
            }
            api.firePostAjax(apiUrl.mapQuestions, { linkQuestions: linkQuestions })
                .done((result) => {
                    if (result && result.Status) {
                        if (result.Message && result.Message.length > 0) {
                        }
                        else {
                            //render.fillSelectTest(result.Data);
                        }
                    }
                    else {

                    }
                })
                .fail();
        },
        onSelectQuestion: function (e) {
            var op = defaults;
            var $selection = $(this);
            var $row = $(e.target).closest('tr');
           
            var question = action.getEmptyQues();
            question.id = $row.find('.'+op.quesId).val();
            question.marks = parseInt( $row.find('.' +op.quesMarks).text(),10);
            if ($selection.is(':checked')) {
                action.addQuestions(question);
            }
            else {
                action.removeQuestions(question);
            }
            console.log(selectedQues);
        }
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
            var $tblTestRecord = $(op.tblTestRecord);
            var selectTest = '';
            $.each(data, (indx, value) => {
                selectTest += `<tr><td><span> ` + (indx + 1) + `</span> <input type='hidden' class='` + op.testId + `' value='` + value.TestBankId + `'/></td>
                                            <td><span class='` + op.testDesc + `'>` + value.Description + ' ( ' + value.TotalMarks + ' / ' + value.Duration + ' mins ) ' + `</span> </td>
                                            <td><span class='` + op.testCategory + `'>` + value.CategoryTypeDescription + `</span> </td>
                                            <td><span class='` + op.testtestDescType + `'>` + value.TestTypeDescription + `</span> </td>
                                            <td><span class='` + op.testLevel + `'>` + value.LevelTypeDescription + `</span> </td>
                                            <td><span class='` + op.testStatus + `'>` + (value.StatusId ? activeText : inactiveText) + `</span> </td></tr>`;

            });
            $tblTestRecord.find('tbody').html(selectTest);
            $tblTestRecord.DataTable();
        },
        fillSelectedTest: function (data) {
            data = data || action.resetSelectedTest();
            var op = defaults;
            $(op.selectedTestDesc).val(data.description);
            $(op.selectedTestCategory).val(data.category);
            $(op.selectedTestType).val(data.type);
            $(op.selectedTestLevel).val(data.level);
        },
        fillSelectQuestion: function (data) {
            var op = defaults;
            var $tblQuesRecord = $(op.tblQuesRecord);
            var selectQues = '';
            $.each(data, (indx, value) => {
                selectQues += `<tr><td><span> <input type="checkbox" class='` + op.selectedQues + `'/></span> <input type='hidden' class='` + op.quesId + `' value='` + value.QId + `'/></td>
                                            <td><span class='` + op.quesDesc + `'>` + value.Description + `</span> </td>
                                            <td><span class='` + op.quesCategory + `'>` + value.CategoryTypeDescription + `</span> </td>
                                            <td><span class='` + op.quesType + `'>` + value.QuesTypeDescription + `</span> </td>
                                            <td><span class='` + op.quesLevel + `'>` + value.LevelTypeDescription + `</span> </td>
                                            <td><span class='` + op.quesMarks + `'>` + value.DefaultMark + `</span> </td></tr>`;

            });
            $tblQuesRecord.find('tbody').html(selectQues);
            $tblQuesRecord.on('change','.'+ op.selectedQues, action.onSelectQuestion);
            $tblQuesRecord.DataTable({
                "order": [],
                "columnDefs": [{
                    "targets": 'no-sort',
                    "orderable": false,
                }]
            });
        },

    };
    var loader = {
        loadEvents: function () {
            var op = defaults;
            var $testContext = $(op.testContext);
            var $quesContext = $(op.quesContext);
            var $selectQuesContext = $(op.selectQuesContext);
            var $selectTestContext = $(op.selectTestContext);
            var $testDataContext = $(op.testDataContext);

            $testContext.on('click', op.openSelectTest, render.openSelectTest);
            $quesContext.on('click', op.openSelectQues, render.opernSelectQues);

            $selectQuesContext.on('click', op.closeSelectQues, render.closeSelectQues);
            $selectQuesContext.on('click', op.addQuestion, action.mapQuestions);
            $selectTestContext.on('click', op.closeSelectTest, render.closeSelectTest);

            $testDataContext.advancedTable({
                rowActiveClass: 'row-active',
                tableRowdblClick: action.setSelectedTest,
            });
        },
        loadApiData: function () {
            action.getTests();
            action.getQuestions();
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