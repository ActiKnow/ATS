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
    var counter = 0;

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
                            alertService.showSuccess(msg, op.msgContext);
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
        counter = 0;
        $(op.selectQuesDiffiLevel).val("");
        $(op.selectQuesQuesTypeId).val("");
        $(op.selectQuesSubjectId).val("");
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
        $(op.btnAddRemove).hide();
        $(op.selectSubjective_text).val("");
        $(op.btnUpdateQuestion).hide();
    };

    var addOption = function (description, isOption) {
        description = description == undefined ? "" : description;
        isOption = isOption == undefined ? "" : isOption;
        ++counter;
        var rowGenrate = "<div class='form-group' id='option" + counter + "'>" +
            "  <div class='input-group'>" +
            "	<div class='input-group-prepend'><span class='input-group-text'>" + counter + "</span></div>" +
            "	<input type='text' name='DynamicTextBox' class='form-control' placeholder='Option' , data-required ='Answer Description is required' , id='Option" + counter + "' value='" + description + "' data-id='" + counter + "'>" +
            "	<div class='input-group-append'><span class='input-group-text'><input name='statusRadio' type='radio' id='radiovale" + counter + "' value='" + isOption + "'  data-id='radio" + counter + "'>Is Correct</span></div>" +
            "  </div>" +
            "</div>";

        var optid = '#radiovale' + counter;

        $(defaults.selectMCQType).append(rowGenrate);
        $(optid).prop("checked", (isOption == 'true'));
    };

    var removeOption = function () {
        if (counter >= 1) {
            $('#option' + counter).remove();
            counter--;
        }
    };

    //var renderOption = function () {
    //    $(defaults.selectMCQType).html("");
    //    for (let i = 0; i < optionArray.length; i++) {
    //        $(defaults.selectMCQType).append(optionArray[i]);
    //    }
    //}

    var createQuestion = function () {
        var flag = true;
        var op = defaults;
        var message = "";
        var quesDiffiLevel = $(op.selectQuesDiffiLevel).val();
        var quesDiffiLevelValue = $(op.selectQuesDiffiLevel).find(':selected').val();
        var $quesType = $(op.selectQuesQuesTypeId);
        var quesTypeId = $quesType.val();
        var quesTypeValue = $quesType.find(':selected').val();
        var quesSubjectId = $(op.selectQuesSubjectId).val();
        var quesSubjectvalue = $(op.selectQuesSubjectId).find(':selected').val();
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

        if (!validationService.validateForm({ messageContext: defaults.msgContext })) {
            return false;
        }

        if (validateRequiredField(optionValue, ansText)) {
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

    var updateQuestion = function () {
        var flag = true;
        var op = defaults;
        var message = "";
        var quesDiffiLevel = $(op.selectQuesDiffiLevel).val();
        var quesDiffiLevelValue = $(op.selectQuesDiffiLevel).find(':selected').val();
        var $quesType = $(op.selectQuesQuesTypeId);
        var quesTypeId = $quesType.val();
        var quesTypeValue = $quesType.find(':selected').val();
        var quesSubjectId = $(op.selectQuesSubjectId).val();
        var quesSubjectvalue = $(op.selectQuesSubjectId).find(':selected').val();
        var quesText = $(op.selectQuesText).val();
        var quesMark = $(op.selectQuesMark).val();
        var QId = $(op.Qid).val();
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
        if (!validationService.validateForm({ messageContext: defaults.msgContext })) {
            return false;
        }

        if (validateRequiredField(optionValue, ansText)) {
            var QuestionView = {
                LevelTypeValue: quesDiffiLevelValue,
                QuesTypeValue: quesTypeValue,
                CategoryTypeValue: quesSubjectvalue,
                Description: quesText,
                DefaultMark: quesMark,
                AnsText: ansText,
                QId: QId
            }
            QuestionView.options = optionValue;
            api.createQuestion('/Setup/UpdateQuestion', { QuestionView: QuestionView })
                .done(callBacks.onQuestionAdded)
                .fail(callBacks.onQuestionFailed);
        }
    };

    var loadQuestionTypes = function () {

        var op = defaults;
        var previousValue = $(op.selectQuesQuesTypeId).attr('value');
        api.fireGetAjax('/Setup/GetQuestionTypes', {})
            .done(res => {
                if (res != null) {
                    var msg = " ";
                    var items = "<option value=''>-Select-</option>";
                    if (res.Status) {
                        if (res.Message && res.Message.length > 0) {
                            $.each(res.Message, function (index, value) {
                                msg += value;
                            });
                            alertService.showError(msg, op.msgContext);
                        }
                        else {
                            $.each(res.Data, function (index, value) {
                                items += "<option value=" + value.Value + ">" + value.Description + "</option>";
                            });
                            if (previousValue && previousValue != 0) {
                                $(op.selectQuesQuesTypeId).html(items).val(previousValue);
                                if (previousValue == questionTypes.option) {
                                    $(defaults.selectMCQType).show();
                                    $(defaults.selectTFType).hide();
                                    $(defaults.selectSubjectType).hide();
                                    optionArray.splice(0, optionArray.length)
                                    $(defaults.btnAddRemove).show();
                                }
                                else if (previousValue == questionTypes.bool) {
                                    $(defaults.selectMCQType).hide();
                                    $(defaults.selectTFType).show();
                                    $(defaults.selectSubjectType).hide();
                                }
                                else {
                                    $(defaults.selectMCQType).hide();
                                    $(defaults.selectTFType).hide();
                                    $(defaults.selectSubjectType).show();
                                    $(op.selectSubjective_text).attr('data-required', "Answer Description is required");
                                }
                            }
                            else {
                                $(op.selectQuesQuesTypeId).html(items);
                            }
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
        var previousValue = $(op.selectQuesDiffiLevel).attr('value');

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
                                items += "<option value=" + value.Value + ">" + value.Description + "</option>";
                            });
                            if (previousValue && previousValue != 0) {
                                $(op.selectQuesDiffiLevel).html(items).val(previousValue);
                            }
                            else {
                                $(op.selectQuesDiffiLevel).html(items);
                            }
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
        var previousValue = $(op.selectQuesSubjectId).attr('value');

        api.fireGetAjax('/Setup/GetCategoryTypes', {})
            .done(res => {
                if (res != null) {
                    var msg = " ";
                    var items = "<option value=''>-Select-</option>";
                    if (res.Status) {
                        if (res.Message && res.Message.length > 0) {
                            $.each(res.Message, function (index, value) {
                                msg += value;
                            });
                            alertService.showError(msg, op.msgContext);
                        }
                        else {
                            $.each(res.Data, function (index, value) {
                                items += "<option value=" + value.Value + ">" + value.Description + "</option>";
                            });
                            if (previousValue && previousValue != 0) {
                                $(op.selectQuesSubjectId).html(items).val(previousValue);
                            }
                            else {
                                $(op.selectQuesSubjectId).html(items);
                            }
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

    var validateRequiredField = function (optionValue, ansText) {
        var flag = true;
        var message = "";
        var quesTypeValue = $(defaults.selectQuesQuesTypeId).find(':selected').val();
        if (quesTypeValue == questionTypes.bool) {
            if (!ansText || ansText.trim() == "") {
                message = "Answer Description is required";
            }
        }
        else if (quesTypeValue == questionTypes.option) {
            var count = 0;
            for (var i = 0; i < optionValue.length; ++i) {
                if (optionValue[i].IsAnswer) {
                    count++;
                }

            }
            if (count == 0) {
                message = "Answer Description is required";
            }
        }
        if (message != "") {
            alertService.showError(message, defaults.msgContext);
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

        $selectQuestionContainer.on('click', op.btnUpdateQuestion, function (e) {
            updateQuestion();
        })

        $selectQuestionContainer.on('click', op.btnBack, function (e) {
            document.location = '@Url.Action("QuestionList","Setup")';
        })

        $selectQuestionContainer.on('change', op.selectQuesQuesTypeId, function (e) {

            var $type = $(this);
            var Type = $type.find(":selected").val();

            $(defaults.selectMCQType).hide();
            $(defaults.selectTFType).hide();
            $(defaults.selectSubjectType).hide();
            $(defaults.btnAddRemove).hide();
            $(defaults.selectMCQType).empty();

            if (Type == questionTypes.option) {
                counter = 0;
                addOption();
                $(defaults.selectMCQType).show();
                $(defaults.btnAddRemove).show();
                $(op.selectSubjective_text).removeAttr('data-required');
            }
            else if (Type == questionTypes.bool) {
                $(defaults.selectTFType).show();
                $(op.selectSubjective_text).removeAttr('data-required');
            }
            else if (Type == questionTypes.text) {
                $(defaults.selectSubjectType).show();
                $(op.selectSubjective_text).attr('data-required', "Answer Description is required");
            }
        })
        $selectQuestionContainer.on('click', op.btnAdd, function (e) {
            if (counter < 8)
                addOption();
        })

        $selectQuestionContainer.on('click', op.btnRemove, function (e) {
            removeOption();
        })

        var valueArray = $(op.optionval).map(function () {
            return this.value;
        }).get();

        var valueOptIsAns = $(op.optionIsAns).map(function () {
            return this.value;
        }).get();

        var optCount = $(defaults.optionCount).val();
        if (optCount && optCount != "0") {
            for (let x = 1; x <= optCount; x++) {
                var $opData = $(op.optionData).find(":nth-child(" + x + ")");
                var desc = $opData.attr("data-description");
                var selectedValue = $opData.attr("data-selected");
                var optval = selectedValue.toLowerCase();
                addOption(desc, optval);
            }
        }
    };
    return {
        init: function (config) {

            $.extend(true, defaults, config);
            bindEvents();
            $(defaults.selectMCQType).hide();
            $(defaults.selectTFType).hide();
            $(defaults.selectSubjectType).hide();
            loadQuestionTypes();
            loadLabelTypes();
            loadCategoryTypes();
        }
    }
})();