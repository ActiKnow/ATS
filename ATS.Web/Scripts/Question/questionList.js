var questionList = (function () {
    'use strict'
    var defaults = {
        selectContainer: '#questionContainer',
        

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
                if (result.Status) {
                    if (result.Message) {
                        $(op.errorMsg).html(result.Message);
                    }
                    else {
                        $(op.tableContext).find('tbody').html(result.Data);
                    }
                }
                else {
                    $(op.errorMsg).html(result.Message);
                }
            }
        }

        return {
            onQuestionList: function (result) {
                appendType(result);
            },
            onQuestionListFailed: function (result) {
                $(op.errorMsg).html(result.responseText);
            },
        }
    })();
    var loadQuestionList = function () {

        var op = defaults;

        api.fireGetAjax('/Admin/Setup/GetQuestionList', {})
            .done(callBacks.onQuestionList)
            .fail(callBacks.onQuestionListFailed);
    }
    var bindEvents = function () {
     

    };
    return {
        init: function (config) {

            $.extend(true, defaults, config);
            bindEvents();
            loadQuestionList();
        }
    }
})();