var userApproval = (function () {
    'use strict'
    var defaults = {
        firstName: '.firstName',
        lastName: '.lastName',
        mobile: '.mobile',
        email: '.email',
        password: '.password',
        createdOn: '.createdOn',
        ddlRoleType: '.roleType',
        errorMsg: '.errorMsg',
        successMsg: '.successMsg',
        btnCreateUser: '.edit',
        btnCancelUser: '.delete',

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
            onGetUser: function (result) {
                appendUser(result);
            },
            onGetUserFailed: function (result) {
                $(op.errorMsg).html(result.responseText);
            },
        }
    })();

    var loadUsersList = function () {
        var op = defaults;

        api.fireGetAjax('/Admin/UserSetup/GetAllUsers', {})
            .done(callBacks.onGetUser)
            .fail(callBacks.onGetUserFailed);      
    }
    var bindEvents = function () {
        var op = defaults;
    };

    return {
        init: function (config) {
            $.extend(true, defaults, config);
            bindEvents();
            loadUsersList();
        }

    }
})();