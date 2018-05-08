﻿var userSetup = (function () {
    'use strict'
    var defaults = {
        firstName: '.firstName',
        lastName: '.lastName',
        mobile: '.mobile',
        email: '.email',
        password: '#password-field',
        btnRefresh: '#refresh',
        ddlRoleType: '#roleType',
        errorMsg: '.errorMsg',
        successMsg: '.successMsg',
        btnCreateUser: '#createUser',
        btnCancelUser: '#cancelUser',
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
        return {
            onUserCreated: function (result) {
                if (result !== "") {
                    if (result.Status) {
                        if (result.Message) {
                            $(op.errorMsg).html(result.Message);
                        }
                        else {
                            $(op.tableContext).html(result.Data);
                        }
                    }
                    else {
                        $(op.errorMsg).html(result.Message);
                    }
                }
            },
            onUserCreationFailed: function (result) {
                $(op.errorMsg).html(result.responseText);
            },
        }
    })();

    var createUser = function () {

        var flag = true;

        var op = defaults;

        var firstName = $(op.firstName).val();
        var lastName = $(op.lastName).val();
        var mobile = $(op.mobile).val();
        var password = $(op.password).val();
        var email = $(op.email).val();
        var roleType = $(op.ddlRoleType).val();

        flag = validateRequiredField(firstName, mobile, password, email, roleType);

        if (flag) {
            var UserCredentials = {
                CurrPassword: password,
                EmailId: email,
                RoleTypeId: roleType,
            };
            var userInfoModel = {
                FName: typeName.trim(),
                LName: typeValue.trim(),
                Mobile: parentKey.trim(),
                Email: email.trim(),
                RoleTypeId: roleType.trim(),
                UserTypeId: roleType.trim(),
                Password: password.trim(),
                UserCredentials: UserCredentials,
            };

            api.firePostAjax('/Setup/CreateType', { userInfoModel: userInfoModel })
                .done(callBacks.onTypeCreated)
                .fail(callBacks.onTypeCreationFailed);

        }


    };

    var validateRequiredField = function (firstName, mobile, password, email, roleType) {

        var flag = true;
        var message = "";

        if (!firstName || firstName.trim() == "") {
            message = "First name is required";
        }
        else if (!mobile || mobile.trim() == "") {
            message = "Mobile is required";
        }
        else if (!password || password.trim() == "") {
            message = "Password is required";
        }
        else if (!email || email.trim() == "") {
            message = "Email is required";
        }
        else if (!roleType || roleType.trim() == "") {
            message = "Please select role type";
        }

        if (message != "") {
            $(defaults.errorMsg).html(message);
            flag = false;
        }

        return flag;
    }

    var loadRoleTypes = function () {
        var op = defaults;

        api.fireGetAjax('/UserSetup/GetRoleTypes', {})
            .done(res => {
                if (res != null) {
                    var items = "<option value=''>-Select-</option>";
                    if (res.Status) {
                        if (res.Message) {
                            $(op.errorMsg).html(res.Message);
                        }
                        else {
                            $.each(res.Data, function (index, value) {
                                items += "<option value='" + value.TypeId + "'>" + value.Description + "</option>";
                            });
                            $(op.ddlRoleType).html(items);
                        }
                    }
                    else {
                        $(op.errorMsg).html(res.Message);
                    }
                }
            })
            .fail(res => {
                $(op.errorMsg).html(res.responseText);
            });
    }


    var bindEvents = function () {
        var op = defaults;

        $(".toggle-password").click(function () {

            $(this).toggleClass("fa-eye fa-eye-slash");
            var input = $($(this).attr("toggle"));
            if (input.attr("type") == "password") {
                input.attr("type", "text");
            } else {
                input.attr("type", "password");
            }
        });
        var $userContext = $(op.userSetupContext);

        $userContext.on('click', op.btnCreateUser, function (e) {
            createUser();
        })
    };

    return {
        init: function (config) {
            $.extend(true, defaults, config);
            bindEvents();
            loadRoleTypes();

        }

    }
})();