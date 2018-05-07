﻿var question = (function () {
    'use strict'
    var defaults = {

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
    var callBack = (function () {
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
            onQuestionFailed: function (res) {
                alertService.showError(result.Message);
            },
        }
    })();

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
                QuesDiffiLevel: QuesDiffiLevel,
                QuesQuesTypeId: QuesQuesTypeId,
                QuesSubjectId: QuesSubjectId,
                QuesText: QuesText,
                QuesMark: QuesMark

            };
            api.createQuestion('/Setup/CreateQuestion', { QuestionView: QuestionView })
                .done(callBacks.onQuestionAdded)
                .fail(callBacks.onQuestionFailed);

        }

    };


    var bindEvents = function () {
        var op = defaults;
        var $selectQuestionContainer = defaults.selectContainer;
        $selectQuestionContainer.on('click', op.btnCreateQuestion, function (e) {
            createQuestion();
        })
    };

    return {
        init: function (config) {

            $.extend(true, defaults, config);
            bindEvents();
        }

    }
})();