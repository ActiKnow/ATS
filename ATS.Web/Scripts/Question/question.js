var question = (function () {
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
        var QuesLangId = $(op.selectQuesLangId).val().trim();
        var QuesExamModeId = $(op.selectQuesExamModeId).val().trim();
        var QuesDiffiLevel = $(op.selectQuesDiffiLevel).val().trim();
        var QuesQuesTypeId = $(op.selectQuesQuesTypeId).val().trim();
        var QuesSubjectId = $(op.selectQuesSubjectId).val().trim();
        var QuesText = $(op.selectQuesText).val().trim();
        var QuesMark = $(op.selectQuesMark).val().trim();

        if (flag) {
            if (validateCustomerGroup()) {
                var QuestionView = {
                    QuesLangId: customerGroup.trim(),
                    QuesExamModeId: description.trim(),
                    SHORT_NAME: shortname.trim(),
                    ACCOUNT_CODE: accountcode.trim(),
                    SUB_ACCOUNT_CODE: subaccountcode.trim(),
                    SORT_SEQNO: sortseqno.trim(),
                    STATUS: status,
                    PAN_MANDATORY: panMandatory,
                };
                api.addCustomersGroup('/Accounts/AddCustomerGroup', { CustomerGroupView: CustomerGroupView })
                    .done(callBacks.onCustomerGroupAdded)
                    .fail(callBacks.onCustomerGroupAddFailed);
            }
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