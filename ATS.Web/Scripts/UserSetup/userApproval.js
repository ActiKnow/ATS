﻿var userApproval = (function () {
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
            onUserDeleted: function (result) {
                appendUser(result);
            },
            onUserDeletionFailed: function (result) {
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

    var deleteUser = function () {

        var UserCredentials = [];
        var op = defaults;

        var userID = $(op.userId).val();
        var RoleID = $(op.roleTypeId).val();      
        var email = $(op.email).val();

        var item = {
            UserId: userID.trim(),   
            EmailId: email.trim(),   
            RoleTypeId: RoleID.trim(),   
        };
        UserCredentials.push(item);

        var userInfoModel = {
            UserId: userID.trim(),   
            RoleTypeId: RoleID.trim(),            
            Email: email.trim(),    
            UserCredentials: UserCredentials,
        };

            api.firePostAjax('/Admin/UserSetup/DeleteUser', { typeDef: typeDef })
                .done(callBacks.onUserDeleted)
                .fail(callBacks.onUserDeletionFailed);

    }

    var updateUser = function () {

        //var UserCredentials = [];
        //var op = defaults;

        //var userID = $(op.userId).val();
        //var RoleID = $(op.roleTypeId).val();
        //var email = $(op.email).val();

        //var item = {
        //    UserId: userID.trim(),
        //    EmailId: email.trim(),
        //    RoleTypeId: RoleID.trim(),
        //};
        //UserCredentials.push(item);

        //var userInfoModel = {
        //    UserId: userID.trim(),
        //    RoleTypeId: RoleID.trim(),
        //    Email: email.trim(),
        //    UserCredentials: UserCredentials,
        //};    
        alert("Edit btn click");       
    }

    var bindEvents = function () {
        var op = defaults;

        $(op.tableContext).on('click', op.btnEditUser, function (e) {

            var $row = $(this).closest("tr");
            var id = $row.find($(op.userId)).val();
             
            $(op.selectedUserId).val(id);
            $(op.submitForm).submit();
            //var url = '@Url.Action("UserSetup","UserSetup")';
            //window.location.href = url + '?userId=' + 11;
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