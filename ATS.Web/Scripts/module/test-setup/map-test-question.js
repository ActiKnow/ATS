
var mapTestQuestion = (function () {
    'use strict';
    var defaults = {};
    const activeText = AppConstant.ACTIVE, inactiveText = AppConstant.INACTIVE;
    var selectedTest = {};
    var selectedQues = [];
    var testQuestions = [];
    var dataTable = {
        testQuestions: null,
        selectQuestions: null,
    }
    var apiUrl = {
        getTests: '/Admin/TestSetup/GetTests/',
        getQuestions: '/Admin/Setup/GetQuestionList/',
        mapQuestions: '/Admin/TestSetup/LinkTestQuestion/',
        unlinkQuestions: '/Admin/TestSetup/UnlinkTestQuestion/',
        getTestQuestions: '/Admin/TestSetup/GetTestQuestions/',
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
        getTestQuestions: function () {

            api.fireGetAjax(apiUrl.getTestQuestions, { testBankId: selectedTest.id })
                .done((result) => {
                    if (result && result.Status) {
                        if (result.Message && result.Message.length > 0) {
                        }
                        else {
                            render.fillTestQuestions(result.Data);
                        }
                    }
                    else {
                        render.fillTestQuestions();
                    }
                })
                .fail()
                .always((result) => { action.getQuestions(); });
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
            action.getTestQuestions();
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
        addSelectedQuestions: function (question) {
            selectedQues.push(question)
        },
        removeSelectedQuestions: function (question) {
            selectedQues.splice(selectedQues.indexOf(question), 1);
        },
        mapQuestions: function () {
            var op = defaults;
            var linkQuestions = [];
            for (let indx in selectedQues) {
                let map = action.getEmptyMapping();
                map.TestBankId = selectedTest.id;
                map.QId = selectedQues[indx].id;
                map.Marks = selectedQues[indx].marks;
                linkQuestions.push(map);
            }
            if (linkQuestions.length <= 0) {
                alertService.showError("Question is required", op.quesMessageContext);
                return false;
            }
            api.firePostAjax(apiUrl.mapQuestions, { linkQuestions: linkQuestions })
                .done((result) => {
                    if (result && result.Status) {
                        if (result.Message && result.Message.length > 0) {
                            alertService.showAllSuccess(result.Message, op.mainMessageContext);
                        }
                        render.closeSelectQues();
                        action.getTestQuestions();
                    }
                    else {
                        alertService.showAllErrors(result.Message, op.quesMessageContext);
                    }
                })
                .fail((result) => { alertService.showAllErrors(result.responseText, op.quesMessageContext); });
        },
        unlinkQuestions: function (testId, qId) {
            var op = defaults;
            var unlinkQuestions = [];
            unlinkQuestions.push({ TestBankId: testId, QId: qId })
            api.firePostAjax(apiUrl.unlinkQuestions, { unlinkQuestions: unlinkQuestions })
                .done((result) => {
                    if (result && result.Status) {
                        if (result.Message && result.Message.length > 0) {
                            alertService.showAllSuccess(result.Message, op.mainMessageContext);
                        }
                        action.getTestQuestions();
                    }
                    else {
                        alertService.showAllErrors(result.Message, op.mainMessageContext);
                    }
                })
                .fail((result) => { alertService.showAllErrors(result.responseText, op.mainMessageContext); });
        },
        setSelectQuestion: function ($row, isChecked) {
            var op = defaults;
            var question = action.getEmptyQues();
            question.id = $row.find('.' + op.quesId).val();
            question.marks = parseInt($row.find('.' + op.quesMarks).val(), 10);
            if (isChecked) {
                action.addSelectedQuestions(question);
            }
            else {
                action.removeSelectedQuestions(question);
            }
        },
        onSelectQuestion: function (e) {
            var op = defaults;
            var $selection = $(this);
            var $row = $(e.target).closest('tr');
            var isSelected = $selection.is(':checked');
            var $selectAllQues = $(op.selectAllQuestions);
            var $quesMarks = $row.find('.'+op.quesMarks);
            $quesMarks.prop('readonly', !isSelected)

            action.setSelectQuestion($row, isSelected);
            if (isSelected && dataTable.selectQuestions) {
                var pageLen = dataTable.selectQuestions.page.len();
                var totalLen = dataTable.selectQuestions.page.info().recordsTotal;
                $selectAllQues.prop('checked', (selectedQues.length >= pageLen || selectedQues.length >= totalLen));
            } else {
                $selectAllQues.prop('checked', false);
            }
        },
        onDeleteQuestion: function (e) {
            var op = defaults;
            var $selection = $(this);
            var $row = $(e.target).closest('tr');
            var question = action.getEmptyQues();
            question.id = $row.find('.' + op.quesId).val();
            dialogService.confirm("Confirmation", "Are you sure to unlink question ?", function () {
                action.unlinkQuestions(selectedTest.id, question.id);
            }, function () {
                alertService.hide();
            });

        },
        onSelectAllQuestions: function (e) {
            var op = defaults;
            var $allSelect = $(this);
            var isSelectAll = $allSelect.is(':checked');
            var $questionRows = $(op.tblQuesRecord + ' tbody tr');
            selectedQues = [];
            $.each($questionRows, (indx, row) => {
                var $row = $(row);
                var $selection = $row.find('.' + op.selectedQues);
                $selection.prop('checked', isSelectAll);
                if (isSelectAll) {
                    action.setSelectQuestion($row, isSelectAll);
                }
            });

        },
        onMarksChange: function (e) {
            var op = defaults;
            var $marks = $(this);
            var marks = $marks.val();
            var $row = $(e.target).closest('tr');
            var qId = $row.find('.' + op.quesId).val();
            if (selectedQues && selectedQues.filter(x => x.id == qId).length > 0) {
                var ques = selectedQues.filter(x => x.id == qId);
                $.each(ques, (index, value) => { value.marks = marks; });
            }
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
            alertService.hide(op.quesMessageContext);
            var $quesModal = $(op.modalSelectQuesContext);
            $quesModal.modal('show');
            selectedQues = [];
        },
        closeSelectTest: function () {
            var op = defaults;
            var $testModal = $(op.modalSelectTestContext);
            $testModal.modal('hide');
        },
        closeSelectQues: function () {
            var op = defaults;
            alertService.hide(op.quesMessageContext);
            var $quesModal = $(op.modalSelectQuesContext);
            $quesModal.modal('hide');
            selectedQues = [];
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
            var rowContents = [];
            if (dataTable.selectQuestions) {
                dataTable.selectQuestions.clear();
            }
            else {
                dataTable.selectQuestions = $tblQuesRecord.DataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }]
                });
            }
            $.each(data, (indx, value) => {
                if (testQuestions && (testQuestions.indexOf(value.QId) == -1)) {


                    selectQues += `<tr><td><span> <input type="checkbox" class='` + op.selectedQues + `'/></span> <input type='hidden' class='` + op.quesId + `' value='` + value.QId + `'/></td>
                                            <td><span class='` + op.quesDesc + `'>` + value.Description + `</span> </td>
                                            <td><span class='` + op.quesCategory + `'>` + value.CategoryTypeDescription + `</span> </td>
                                            <td><span class='` + op.quesType + `'>` + value.QuesTypeDescription + `</span> </td>
                                            <td><span class='` + op.quesLevel + `'>` + value.LevelTypeDescription + `</span> </td>
                                            <td><input type='number' readonly='readonly' class='` + op.quesMarks + `' value='` + value.DefaultMark + `'/> </td></tr>`;
                    rowContents = [];
                    rowContents.push(`<span> <input type="checkbox" class='` + op.selectedQues + `'/></span> <input type='hidden' class='` + op.quesId + `' value='` + value.QId + `'/>`);
                    rowContents.push(`<span class='` + op.quesDesc + `'>` + value.Description + `</span>`);
                    rowContents.push(`<span class='` + op.quesCategory + `'>` + value.CategoryTypeDescription + `</span>`);
                    rowContents.push(`<span class='` + op.quesType + `'>` + value.QuesTypeDescription + `</span> `);
                    rowContents.push(`<span class='` + op.quesLevel + `'>` + value.LevelTypeDescription + `</span>`);
                    rowContents.push(`<input type='number' readonly='readonly' class='` + op.quesMarks + `' value='` + value.DefaultMark + `'/> `);
                    if (dataTable.selectQuestions) {
                        dataTable.selectQuestions.row.add(rowContents);
                    }
                }

            });

            if (dataTable.selectQuestions) {
                dataTable.selectQuestions.search('').draw();
            }
            else {
                $tblQuesRecord.find('tbody').html(selectQues);
            }
            $tblQuesRecord.off('change', '.' + op.selectedQues, action.onSelectQuestion);
            $tblQuesRecord.on('change', '.' + op.selectedQues, action.onSelectQuestion);
            $tblQuesRecord.off('change', '.' + op.quesMarks, action.onMarksChange);
            $tblQuesRecord.on('change', '.' + op.quesMarks, action.onMarksChange);
        },
        fillTestQuestions: function (data) {
            var op = defaults;
            var $tblQuesRecord = $(op.testQuestions);
            var selectQues = '';
            var rowContents = [];

            if (dataTable.testQuestions) {
                dataTable.testQuestions.clear();
            }
            else {
                dataTable.testQuestions = $tblQuesRecord.DataTable();
            }
            testQuestions = [];
            $.each(data, (indx, value) => {
                selectQues += `<tr><td><span> ` + (indx + 1) + `</span> <input type='hidden' class='` + op.quesId + `' value='` + value.QId + `'/></td>
                                            <td><span class='` + op.quesDesc + `'>` + value.Description + `</span> </td>
                                            <td><span class='` + op.quesCategory + `'>` + value.CategoryTypeDescription + `</span> </td>
                                            <td><span class='` + op.quesType + `'>` + value.QuesTypeDescription + `</span> </td>
                                            <td><span class='` + op.quesLevel + `'>` + value.LevelTypeDescription + `</span> </td>
                                            <td><span class='` + op.quesMarks + `'>` + value.DefaultMark + `</span> </td>
                                            <td>  <button class="btn-primary btn-xs ` + op.deleteQuestion + `"><i class="fa fa-remove" aria-hidden="true"></i></button></td></tr >`;
                testQuestions.push(value.QId);
                rowContents = [];
                rowContents.push(`<span> ` + (indx + 1) + `</span> <input type='hidden' class='` + op.quesId + `' value='` + value.QId + `' />`);
                rowContents.push(`<span class='` + op.quesDesc + `'>` + value.Description + `</span>`);
                rowContents.push(`<span class='` + op.quesCategory + `'>` + value.CategoryTypeDescription + `</span>`);
                rowContents.push(`<span class='` + op.quesType + `'>` + value.QuesTypeDescription + `</span> `);
                rowContents.push(`<span class='` + op.quesLevel + `'>` + value.LevelTypeDescription + `</span>`);
                rowContents.push(`<span class='` + op.quesMarks + `'>` + value.DefaultMark + `</span> `);
                rowContents.push(` <button class="btn-primary btn-xs ` + op.deleteQuestion + `"><i class="fa fa-remove" aria-hidden="true"></i></button>`);
                if (dataTable.testQuestions) {
                    dataTable.testQuestions.row.add(rowContents);
                }
            });

            if (dataTable.testQuestions) {
                dataTable.testQuestions.search('').draw();
            }
            else {
                $tblQuesRecord.find('tbody').html(selectQues);
            }
            $tblQuesRecord.off('click', '.' + op.deleteQuestion, action.onDeleteQuestion);
            $tblQuesRecord.on('click', '.' + op.deleteQuestion, action.onDeleteQuestion);
        }
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
            $selectQuesContext.on('change', op.selectAllQuestions, action.onSelectAllQuestions);
            $selectTestContext.on('click', op.closeSelectTest, render.closeSelectTest);

            $testDataContext.advancedTable({
                rowActiveClass: 'row-active',
                tableRowdblClick: action.setSelectedTest,
            });
        },
        loadApiData: function () {
            action.getTests();
            //action.getQuestions();
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