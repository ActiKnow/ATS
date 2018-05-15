var userApproval = (function () {
    'use strict'
    var defaults = {
        firstName: '.firstName',
        lastName: '.lastName',
        mobile: '.mobile',
        email: '.email',
        //password: '.password',
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
                var msg = "";
                if (result.Status) {
                    if (result.Message && result.Message.length>0) {
                        $.each(result.Message, function (index, value) {
                            msg += value;
                        });
                        alertService.showSuccess(msg, op.msgContext);
                    }

                    $(op.tableContext).find('tbody').html(result.Data);
                    $(op.tableId).DataTable();
                }
                else {
                    $.each(result.Message, function (index, value) {
                        msg += value;
                    });
                    alertService.showError(msg, op.msgContext);
                }
            }
        }

        return {
            onGetUser: function (result) {
                appendUser(result);
            },
            onGetUserFailed: function (result) {
                alertService.showError(result.responseText, op.msgContext);
            },
            onUserDeleted: function (result) {
                appendUser(result);
            },
            onUserDeletionFailed: function (result) {
                alertService.showSuccess(result.responseText, op.msgContext);
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

        $(op.tableContext).on('click', op.btnEditUser, function (e) {

            var $row = $(this).closest("tr");
            var id = $row.find($(op.userId)).val();
            
            $(op.selectedUserId).val(id);
            $(op.submitForm).submit();
        });

        $(op.tableContext).on('click', op.btnRemoveUser, function (e) {

            var $row = $(this).closest("tr");

           // var UserCredentials = [];

            var userID =  $row.find($(op.userId)).val();
            var RoleID =  $row.find($(op.roleTypeId)).val();
            var email =   $row.find($(op.email)).html();
            var id = $row.find($(op.hiddenId)).val();

            var UserCredentials = {
                UserId: userID.trim(),
                Id: id.trim(),
                StatusId: false,
            };

            var userInfoModel = {
                UserId: userID.trim(),
                StatusId: false,
                UserCredentials: UserCredentials,
                //RoleTypeId: RoleID.trim(),
                //Email: email.trim(),
                
            };

            api.firePostAjax('/Admin/UserSetup/DisableUser', { userInfoModel: userInfoModel })
                .done(callBacks.onUserDeleted)
                .fail(callBacks.onUserDeletionFailed);
           
        });
    };

    return {
        init: function (config) {
            $.extend(true, defaults, config);
            bindEvents();
            loadUsersList();
        }
    }
})();