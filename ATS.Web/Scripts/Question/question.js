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
        var appendQuestion = function (result) {
            let $tableConbtainer = $(defaults.selectContainer);
            $tableConbtainer.html(result);
        };
        return {
            onQuestionAdded: function (result) {
                if (result !== "") {
                    if (result.Status) {
                        alertService.showSuccess(result.Message);
                    }
                    else {
                        alertService.showError(result.Message);
                    }
                }
            },
            onQuestionFailed: function (result) {
                alertService.showError(result.responseText);
            },
        }
    })();
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

    var createQuestion = function () {
        var flag = true;
        var op = defaults;
        var message = "";
        var QuesLangId = $(op.selectQuesLangId).val();
        var QuesExamModeId = $(op.selectQuesExamModeId).val();
        var QuesDiffiLevel = $(op.selectQuesDiffiLevel).val();
        var QuesQuesTypeId = $(op.selectQuesQuesTypeId).val();
        var QuesSubjectId = $(op.selectQuesSubjectId).val();
        var QuesText = $(op.selectQuesText).val();
        var QuesMark = $(op.selectQuesMark).val();

        if (flag) {

            var QuestionView = {
                QuesLangId: QuesLangId.trim(),
                QuesExamModeId: QuesExamModeId,
                LevelTypeId: QuesDiffiLevel,
                QuesTypeId: QuesQuesTypeId,
                CategoryTypeId: QuesSubjectId,
                Description: QuesText,
                DefaultMark: QuesMark

            };
            api.createQuestion('/Setup/CreateQuestion', { QuestionView: QuestionView })
                .done(callBacks.onQuestionAdded)
                .fail(callBacks.onQuestionFailed);

        }

    };


    var bindEvents = function () {
        var op = defaults;
        var $selectQuestionContainer = $(op.selectContainer);
        $selectQuestionContainer.on('click', op.btnCreateQuestion, function (e) {
            createQuestion();
        })

        $selectQuestionContainer.on('change', op.selectQuesQuesTypeId, function (e) {
            var Type = $(op.selectQuesQuesTypeId).val();
            if (Type == '1') {
                $(defaults.selectMCQType).show();
                $(defaults.selectTFType).hide();
                $(defaults.selectSubjectType).hide();
                emptyOption();
            }
            else if (Type == '2') {
                $(defaults.selectMCQType).hide();
                $(defaults.selectTFType).show();
                $(defaults.selectSubjectType).hide();
                emptyOption();
            }
            else {
                $(defaults.selectMCQType).hide();
                $(defaults.selectTFType).hide();
                $(defaults.selectSubjectType).show();
                emptyOption();
            }

        })
    };

    return {
        init: function (config) {

            $.extend(true, defaults, config);
            bindEvents();
            $(defaults.selectMCQType).hide();
            $(defaults.selectTFType).hide();
            $(defaults.selectSubjectType).hide();
        }

    }
})();