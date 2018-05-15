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
    

    var bindEvents = function () {
        var op = defaults;
        var $tableContext = $(op.tableContext);

        $tableContext.on('click', op.btnchkAllUserinfo, function (event) {
            if (this.checked) {
                $(op.selecttblUserList).prop(':checkbox').each(function () {
                    this.checked = true;
                });
            }
            else {
                $(op.selecttblUserList).prop(':checkbox').each(function () {
                    this.checked = false;
                });
            }
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