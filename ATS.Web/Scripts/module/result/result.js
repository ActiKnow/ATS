var result = (function () {
    'use strict'
    var defaults = {
        mainMessageContext: '#mainMessageContext',
        tableContext: '#typeContextModel',
        selecttblUserList: '#tblUserList',
        selecttblUserAnswerList: '#tblUserAnswerList',
        btnConsolidatedResult: '#btnConsolidatedResult',
        btnIndividualResult: "btnIndividualResult",
        btnchkAllUserinfo: '#chkUserinfo',
    };

    var allUserIdList = [];

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
                        alertService.showSuccess(msg, op.popupMessageContext);
                    }
                    $(op.selecttblUserList).find('tbody').html(result.Data);
                    $(op.selecttblUserList).DataTable();
                }
                else {
                    $.each(result.Message, function (index, value) {
                        msg += value;
                    });
                    alertService.showError(msg, op.popupMessageContext);
                }
            }
        }
        return {
            onUserListSuccess: function (result) {
                appendUser(result);
            },
            onUserListFailed: function (result) {
                alertService.showError(result.responseText, op.popupMessageContext);
            },

            onConsolidatedResultSuccess: function (result) {

            },

            onConsolidatedResultFailed: function (result) {

            },

            onIndividualResultSuccess: function (result) {

            },

            onIndividualResultFailed: function (result) {

            }

        }
    })();

    var loadUserList = function () {
        var op = defaults;
        api.fireGetAjax('/Admin/ResultSetup/GetAllUsers', {})
            .done(callBacks.onUserListSuccess)
            .fail(callBacks.onUserListFailed);
    }

    var addUserList = function () {

        allUserIdList = [];
        var $selectTable = $(defaults.selecttblUserList);
        var $tblBody = $selectTable.find('tbody');
        $tblBody.find(':checkbox:checked').each(function () {
            var recID = $(this).val();
          
            if (recID) {
                allUserIdList.push(recID);
            }
        });
    }

    var GetConsolidatedTestResults = function () {

        addUserList();

        if (allUserIdList.length === 0) {
            $(defaults.errorMsg).html("Please select at least one record to proceed");
        }
        else {
            api.firePostAjax('/Admin/ResultSetup/GetConsolidatedTestResults', { allUserIdList: allUserIdList })
                .done(callBacks.onConsolidatedResultSuccess)
                .fail(callBacks.onConsolidatedResultFailed)
        }
    }

    var GetIndividualTestResults = function () {

        addUserList();

        if (allUserIdList.length === 0) {
            $(defaults.errorMsg).html("Please select at least one record to proceed");
        }
        else {
            api.firePostAjax('/Admin/Result/GetIndividualTestResults', { allUserIdList: allUserIdList })
                .done(callBacks.onIndividualResultSuccess)
                .fail(callBacks.onIndividualResultFailed)
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
        $tableUserContext.on('click', op.btnConsolidatedResult, function (event) {
            GetConsolidatedTestResults();
        });

        $tableUserContext.on('click', op.btnIndividualResult, function (event) {
            GetIndividualTestResults();
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