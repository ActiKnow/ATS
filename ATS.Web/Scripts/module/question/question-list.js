var questionList = (function () {
    'use strict'
    var defaults = {
        selectContainer: '#questionContainer',
        sampleTable: '#sampleTable',
        mainMessageContext:'#mainMessageContext'
    };
    var questionTypes = {
        option: "Option",
        bool: "Bool",
        text: "Text"
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
    var callBacks = (function () {
        var op = defaults;

        var appendType = function (result) {
            if (result !== "") {
                var msg = " ";
                if (result.Status) {
                    if (result.Message && result.Message.length>0) {
                        $.each(result.Message, function (index, value) {
                            msg += value;
                        });
                        alertService.showSuccess(msg, mainMessageContext);
                    }
                    $(op.tableContext).find('tbody').html(result.Data);
                    $(op.sampleTable).DataTable();
                }
                else {
                    $.each(result.Message, function (index, value) {
                        msg += value;
                    });
                    alertService.showError(msg, mainMessageContext);
                }
            }
        }
        return {
            onQuestionList: function (result) {
                appendType(result);
            },
            onQuestionListFailed: function (result) {
                alertService.showError(result.responseText, mainMessageContext);
            },
            onDeleteQuestion: function (result) {
                appendType(result);
            },
            onDeleteQuestionFailed: function (result) {
                alertService.showError(result.responseText, mainMessageContext);
            },
        }
    })();
    var loadQuestionList = function () {
        var op = defaults;
        api.fireGetAjax('/Admin/Setup/GetQuestionList', {})
            .done(callBacks.onQuestionList)
            .fail(callBacks.onQuestionListFailed);
    }
    var deleteQuestion = function (qId) {
        if (qId != null) {
            var op = defaults;
            api.firePostAjax('/Admin/Setup/DeleteQuestion', { qId: qId })
                .done(callBacks.onDeleteQuestion)
                .fail(callBacks.onDeleteQuestionFailed);
        }

    };
    var editQuestion = function (qId) {
        if (qId != null) {
            var op = defaults;
            $(op.selectedId).val(qId);
            $(op.formSubmit).submit();
        }
    };
    var bindEvents = function () {
        var op = defaults;
        var $tableContext = $(op.tableContext);
        $tableContext.on('click', op.btnRemoveType, function (e) {
            var $currentRow = $(e.target).closest('tr');
            var qId = $currentRow.find(op.Qid).html();
            deleteQuestion(qId);
        });
        $tableContext.on('click', op.btnEditType, function (e) {
            var $currentRow = $(e.target).closest('tr');
            var qId = $currentRow.find(op.Qid).html();
            editQuestion(qId);
        });
    };
    return {
        init: function (config) {

            $.extend(true, defaults, config);
            bindEvents();
            loadQuestionList();
        }
    }
})();