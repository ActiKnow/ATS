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
        btnCreateQuestion: '#button_create_question'

    };
    var questionTypes = {
        option: "Option",
        bool: "Bool",
        text: "Text"
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
                    if (result.Status) {
                        $(op.errorMsg).html(result.Message);
                    }
                    else {
                        $(op.errorMsg).html(result.Message);
                    }
                }
            },
            onQuestionFailed: function (result) {
                clear();
                $(op.errorMsg).html(result.Message);
            },
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

    var addQuestion = function () {

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
        var QuesDiffiLevel = $(op.selectQuesDiffiLevel).val();
        var QuesQuesTypeId = $(op.selectQuesQuesTypeId).val();
        var QuesSubjectId = $(op.selectQuesSubjectId).val();
        var QuesText = $(op.selectQuesText).val();
        var QuesMark = $(op.selectQuesMark).val();

        var Arr = [];

        var optionValue = [];
        if (QuesQuesTypeId == questionTypes.option) {
            $("input[name=DynamicTextBox]").each(function () {
                var $option = $(this);
                var id = $option.data("id");
                var $radio = $("input[data-id=radio" + id + "]");
                var isAnswer = $radio.is(':checked');
                // var isAns = $('input[name=statusRadio]:checked').val();
                // if (isAns==)
                optionValue.push({ Id: "", KeyId: "", Description: $(this).val(), IsAnswer: isAnswer });
            });
        }

        if (QuesQuesTypeId == questionTypes.bool) {
            $("input[name=Booltextbox]").each(function () {
                var $option = $(this);
                var id = $option.data("id");
                var $radio = $("input[data-id=" + id + "]");
                var isAnswer = $radio.is(':checked');
                // var isAns = $('input[name=statusRadio]:checked').val();
                // if (isAns==)
                optionValue.push({ Id: "", KeyId: "", Description: $(this).val(), IsAnswer: isAnswer });
            });
        }

        if (QuesQuesTypeId == questionTypes.text) {
            var ansText = $(op.selectSubjective_text).val();
        }

        var QuestionView = {
            LevelTypeId: QuesDiffiLevel,
            QuesTypeId: QuesQuesTypeId,
            CategoryTypeId: QuesSubjectId,
            Description: QuesText,
            DefaultMark: QuesMark,
            AnsText: ansText
        }
        QuestionView.options = optionValue;
        api.createQuestion('/Setup/CreateQuestion', { QuestionView: QuestionView })
            .done(callBacks.onQuestionAdded)
            .fail(callBacks.onQuestionFailed);
    };


    var bindEvents = function () {
        var op = defaults;
        var $selectQuestionContainer = $(op.selectContainer);
        $selectQuestionContainer.on('click', op.btnCreateQuestion, function (e) {
            createQuestion();
        })

        $selectQuestionContainer.on('change', op.selectQuesQuesTypeId, function (e) {
            var Type = $(op.selectQuesQuesTypeId).val();
            if (Type == questionTypes.option) {
                $(defaults.selectMCQType).show();
                $(defaults.selectTFType).hide();
                $(defaults.selectSubjectType).hide();
                optionArray.splice(0, optionArray.length)
                counter = 1;
                addQuestion();
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
            addQuestion();
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
        }

    }
})();