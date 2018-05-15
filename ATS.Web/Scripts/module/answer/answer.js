var answer = (function () {
    'use strict'
    var defaults = {
        selectContainer: '#questionContainer',
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

        var appendUser = function (result) {
            if (result !== "") {
                var msg = " ";
                if (result.Status) {
                    if (result.Message && result.Message.length > 0) {
                        $.each(result.Message, function (index, value) {
                            msg += value;
                        });
                        $(op.errorMsg).html(msg);
                    }
                    else {
                        $(op.selecttblUserList).find('tbody').html(result.Data);
                    }
                }
                else {
                    $.each(result.Message, function (index, value) {
                        msg += value;
                    });
                    $(op.errorMsg).html(msg);
                }
            }
        }
        return {
            onUserList: function (result) {
                appendUser(result);
            },
            onUserListFailed: function (result) {
                $(op.errorMsg).html(result.responseText);
            },
        }
    })();
    var loadUserList = function () {
        var op = defaults;
        api.fireGetAjax('/Admin/Setup/GetAllUsers', {})
            .done(callBacks.onUserList)
            .fail(callBacks.onUserListFailed);
    }
    var userResult = function () {
        var $selectTable = $(defaults.selecttblUserList);
        var allUserIdList = [];
        $selectTable.find(':checkbox:checked').each((index, element) => {
            var recID = element.dataset.recid;
            if (recID) {
                allUserIdList.push({
                    UserId: recID,
                   
                });
            }
        })
        if (allUserIdList.length === 0) {
            $(defaults.errorMsg).html("Please select at least one record to proceed");
        }
        else {
            api.firePostAjax('/Admin/Setup/GetAnswerUsers', { allUserIdList: allUserIdList })
                .done(callBacks.onAnswerSuccess)
                .fail(callBacks.onAnswerFailed)
        }
    }

    var bindEvents = function () {
        var op = defaults;
        var $tableUserContext = $(op.tableContext);

        $tableUserContext.on('click', op.btnchkAllUserinfo, function (event) {
            if (this.checked) {
                $(op.selecttblUserList).find(':checkbox').each(function () {
                    this.checked = true;
                });
            }
            else {
                $(op.selecttblUserList).find(':checkbox').each(function () {
                    this.checked = false;
                });
            }
        });
        $tableUserContext.on('click', op.btnView, function (event) {
            userResult();
        });
       };
    return {
        init: function (config) {

            $.extend(true, defaults, config);
            bindEvents();
            loadUserList();
        }
    }
})();