var question = (function () {
    'use strict'
    var defaults = {
        selectContainer: '#questionContainer',
        selectQuesLangId: '#question_language_id_1',
        selectQuesExamModeId: '#question_exam_mode_id',
        selectQuesDiffiLevel: '#question_difficulty_level',
        selectQuesQuesTypeId: '#question_question_type_id',
        selectQuesSubjectId: '#question_subject_id',
        selectQuesText: '#question_text',
        selectQuesMark: '#question_mark',
        btnCreateQuestion: '#button_create_question',
        msgContext: '#msgContext',

    };
    var questionTypes = {
        option: AppConstant.OPTION,
        bool: AppConstant.BOOL,
        text: AppConstant.TEXT
    };
    var optionArray = [];
    var counter = 1;
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
            createQuestion: function (url, data) {
                return fireAjax(url, data);
            },
        }
    }());
    var callBacks = (function () {
        var op = defaults;
        var appendQuestion = function (result) {
            let $tableConbtainer = $(defaults.selectContainer);
            $tableConbtainer.html(result);
        };
        return {
            onQuestionAdded: function (result) {
                clear();
                if (result !== "") {
                    var msg = "";
                    if (result.Status) {
                        if (result.Message && result.Message.length > 0) {
                            $.each(result.Message, function (index, value) {
                                msg += value;
                            });
                            alertService.showSuccess(msg,op.msgContext);
                        }
                    }
                    else {
                        $.each(result.Message, function (index, value) {
                            msg += value;
                        });
                        alertService.showError(msg, op.msgContext);
                    }
                }
            },
            onQuestionFailed: function (result) {
                clear();
                alertService.showError(result.responseText, op.msgContext);
            }
        }        
    })();
    var clear = function () {
        var op = defaults;
        $(op.selectQuesDiffiLevel).val("easy");
        $(op.selectQuesQuesTypeId).val("-1");
        $(op.selectQuesSubjectId).val("-1");
        $(op.selectQuesText).val("");
        $(op.selectQuesMark).val("");
        $(op.selectMCQType).html("");
        $(op.selectTFType).hide();
        $(op.selectboolradio).prop('checked', false);
        $(op.selectOption1).val("");
        $(op.selectOption2).val("");
        $(op.selectOption3).val("");
        $(op.selectOption4).val("");
        $(op.selectTrue).val("");
        $(op.selectFalse).val("");
        $(op.selectSubjective_text).val("");
    };
    var emptyOption = function () {

        var op = defaults;
        $(op.selectOption1).val("");
        $(op.selectOption2).val("");
        $(op.selectOption3).val("");
        $(op.selectOption4).val("");
        $(op.selectTrue).val("");
        $(op.selectFalse).val("");
        $(op.selectSubjective_text).val("");
    };
    var addOption = function () {

        var rowGenrate = "<div class='form-group row'>" +
            "		<div class='col-md-1'>" +
            "		</div>" +
            "		<div class='col-md-1'>" + counter + "</div>" +
            "		<div class='col-md-7'>" +
            "			<input type='text' name='DynamicTextBox' class='form-control input-sm' placeholder='Option' id='Option" + counter + "' value='' data-id='" + counter + "'>" +
            "		</div>" +
            "		<div class='col-md-3'>" +
            "			<input name='statusRadio' type='radio' value=" + counter + " data-id='radio" + counter + "'>" +
            "			<span>Is Correct</span>" +
            "   	</div>" +
            "</div>";

        optionArray.push(rowGenrate);
        renderOption(optionArray);
        counter++;
    };
    var removeQuestion = function () {
        optionArray.pop();

        renderOption(optionArray);
        if (counter > 1)
            counter--;
    };
    var renderOption = function () {
        $(defaults.selectMCQType).html("");
        for (let i = 0; i < optionArray.length; i++) {
            $(defaults.selectMCQType).append(optionArray[i]);
        }
    }
    var createQuestion = function () {
        var flag = true;
        var op = defaults;
        var message = "";
        var quesDiffiLevel = $(op.selectQuesDiffiLevel).val();
        var quesDiffiLevelValue = $(op.selectQuesDiffiLevel).find(':selected').attr('data-id');
        var $quesType = $(op.selectQuesQuesTypeId);
        var quesTypeId = $quesType.val();
        var quesTypeValue = $quesType.find(':selected').attr('data-id');
        var quesSubjectId = $(op.selectQuesSubjectId).val();
        var quesSubjectvalue = $(op.selectQuesSubjectId).find(':selected').attr('data-id');
        var quesText = $(op.selectQuesText).val();
        var quesMark = $(op.selectQuesMark).val();
        var ansText = "";
        var optionValue = [];
        if (quesTypeValue == questionTypes.option) {
            $("input[name=DynamicTextBox]").each(function () {
                var $option = $(this);
                var id = $option.data("id");
                var $radio = $("input[data-id=radio" + id + "]");
                var isAnswer = $radio.is(':checked');
                optionValue.push({ Id: "", KeyId: "", Description: $(this).val(), IsAnswer: isAnswer });
            });
        }

        if (quesTypeValue == questionTypes.bool) {

            var $radio = $(op.selectboolradio + ':checked');
            ansText = $radio.val();
        }

        if (quesTypeValue == questionTypes.text) {
             ansText = $(op.selectSubjective_text).val();
        }
        if (validateRequiredField(quesDiffiLevel, quesTypeId, quesSubjectId, quesText, quesMark)) {
            var QuestionView = {
                LevelTypeValue: quesDiffiLevelValue,
                QuesTypeValue: quesTypeValue,
                CategoryTypeValue: quesSubjectvalue,
                Description: quesText,
                DefaultMark: quesMark,
                AnsText: ansText
            }
            QuestionView.options = optionValue;
            api.createQuestion('/Setup/CreateQuestion', { QuestionView: QuestionView })
                .done(callBacks.onQuestionAdded)
                .fail(callBacks.onQuestionFailed);
        }
    };
    var loadQuestionTypes = function () {
        var op = defaults;
        api.fireGetAjax('/Setup/GetQuestionTypes', {})
            .done(res => {
                if (res != null) {
                    var msg = " ";
                    var items = "<option value=''>-Select-</option>";
                    if (res.Status) {
                        if (res.Message && res.Message.length>0) {
                            $.each(res.Message, function (index, value) {
                                msg += value;
                            });
                            alertService.showError(msg, op.msgContext);
                        }
                        else {
                            $.each(res.Data, function (index, value) {
                                items += "<option value='" + value.TypeId + "' data-id='" + value.Value + "'>" + value.Description + "</option>";
                            });
                            $(op.selectQuesQuesTypeId).html(items);
                        }
                    }
                    else {
                        $.each(res.Message, function (index, value) {
                            msg += value;
                        });
                        alertService.showError(msg, op.msgContext);
                    }
                }
            })
            .fail(res => {
                alertService.showError(res.responseText, op.msgContext);
            });
    }
    var loadLabelTypes = function () {
        var op = defaults;

        api.fireGetAjax('/Setup/GetLevelTypes', {})
            .done(res => {
                if (res != null) {
                    var msg = " ";
                    var items = "<option value=''>-Select-</option>";
                    if (res.Status) {
                        if (res.Message && res.Message.Count) {
                            $.each(res.Message, function (index, value) {
                                msg += value;
                            });
                            alertService.showError(msg, op.msgContext);
                        }
                        else {
                            $.each(res.Data, function (index, value) {
                                items += "<option value='" + value.TypeId + "' data-id='" + value.Value + "'>" + value.Description + "</option>";
                            });
                            $(op.selectQuesDiffiLevel).html(items);
                        }
                    }
                    else {
                        $.each(res.Message, function (index, value) {
                            msg += value;
                        });
                        alertService.showError(msg, op.msgContext);
                    }
                }
            })
            .fail(res => {
                alertService.showError(res.responseText, op.msgContext);
            });
    }
    var loadCategoryTypes = function () {
        var op = defaults;

    api.fireGetAjax('/Setup/GetCategoryTypes', {})
        .done(res => {
            if (res != null) {
                var msg = " ";
                var items = "<option value=''>-Select-</option>";
                if (res.Status) {
                    if (res.Message && res.Message.length>0) {
                        $.each(res.Message, function (index, value) {
                            msg += value;
                        });
                        alertService.showError(msg, op.msgContext);
                    }
                    else {
                        $.each(res.Data, function (index, value) {
                            items += "<option value='" + value.TypeId + "' data-id='" + value.Value + "'>" + value.Description + "</option>";
                        });
                        $(op.selectQuesSubjectId).html(items);
                    }
                }
                else {
                    $.each(res.Message, function (index, value) {
                        msg += value;
                    });
                    alertService.showError(msg, op.msgContext);
                }
            }
        })
        .fail(res => {
            alertService.showError(res.responseText, op.msgContext);
        });
    }
    var validateRequiredField = function (quesDiffiLevel, quesTypeId, quesSubjectId, quesText, quesMark) {

        var flag = true;
        var message = "";

        if (!quesDiffiLevel || quesDiffiLevel == "") {
            message = "Difficulty Level is required";
        }
        else if (!quesTypeId || quesTypeId == "") {
            message = "Question Type is required";
        }
        else if (!quesSubjectId || quesSubjectId == "") {
            message = "Subject is required";
        }
        else if (!quesText || quesText.trim() == "") {
            message = "Question Description is required";
        }
        else if (!quesMark || quesMark.trim() == "") {
            message = "Mark value is required";
        }

        if (message != "") {
            alertService.showError(message, defaults.messageContext);           
            flag = false;
        }

        return flag;
    }
    var bindEvents = function () {
        var op = defaults;
        var $selectQuestionContainer = $(op.selectContainer);
        $selectQuestionContainer.on('click', op.btnCreateQuestion, function (e) {
            createQuestion();
        })

        $selectQuestionContainer.on('change', op.selectQuesQuesTypeId, function (e) {
            //var Type = $(op.selectQuesQuesTypeId).val();
            var $type = $(this);
            var Type = $type.find(":selected").attr('data-id');
            if (Type == questionTypes.option) {
                $(defaults.selectMCQType).show();
                $(defaults.selectTFType).hide();
                $(defaults.selectSubjectType).hide();
                optionArray.splice(0, optionArray.length)
                counter = 1;
                addOption();
                $(defaults.btnAdd).show();
                $(defaults.btnRemove).show();
            }
            else if (Type == questionTypes.bool) {
                $(defaults.selectMCQType).hide();
                $(defaults.selectTFType).show();
                $(defaults.selectSubjectType).hide();
                emptyOption();
                $(defaults.btnAdd).hide();
                $(defaults.btnRemove).hide();
            }
            else {
                $(defaults.selectMCQType).hide();
                $(defaults.selectTFType).hide();
                $(defaults.selectSubjectType).show();
                emptyOption();
                $(defaults.btnAdd).hide();
                $(defaults.btnRemove).hide();
            }

        })

        $selectQuestionContainer.on('click', op.btnAdd, function (e) {
            if (counter < 8)
                addOption();
        })

        $selectQuestionContainer.on('click', op.btnRemove, function (e) {
            removeQuestion();
        })


    };
    return {
        init: function (config) {

            $.extend(true, defaults, config);
            bindEvents();
            $(defaults.selectMCQType).hide();
            $(defaults.selectTFType).hide();
            $(defaults.selectSubjectType).hide();
            $(defaults.btnAdd).hide();
            $(defaults.btnRemove).hide();
            loadQuestionTypes();
            loadLabelTypes();
            loadCategoryTypes();
        }
    }
})();