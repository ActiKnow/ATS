var userSetup = (function () {
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
                            $(op.successMsg).show();
                            $(op.successMsg).html(result.Message);
                            resetFields();
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
        var UserCredentials = [];
        var op = defaults;

        var firstName = $(op.firstName).val();
        var lastName = $(op.lastName).val();
        var mobile = $(op.mobile).val();
        var password = $(op.password).val();
        var email = $(op.email).val();
        var roleType = $(op.ddlRoleType).find(":selected").val();

        flag = validateRequiredField(firstName, mobile, password, email, roleType);

        if (flag) {
            var item = {
                CurrPassword: password,
                EmailId: email,
                RoleTypeId: roleType,
            };
            UserCredentials.push(item);
            var userInfoModel = {
                FName: firstName.trim(),
                LName: lastName.trim(),
                Mobile: mobile.trim(),
                Email: email.trim(),
                RoleTypeId: roleType.trim(),
                Password: password.trim(),
                UserCredentials: UserCredentials,
            };

            api.firePostAjax('/UserSetup/CreateUser', { userInfoModel: userInfoModel })
                .done(callBacks.onUserCreated)
                .fail(callBacks.onUserCreationFailed);
        }       

    };

    var validateRequiredField = function (firstName, mobile, password, email, roleType) {

        var flag = true;
        var message = "";
        $(defaults.successMsg).hide();
       
        if (!firstName || firstName.trim() == "") {
            message = "First name is required";
        }
        else if (!mobile || mobile.trim() == "") {
            message = "Mobile is required";
        }
        else if (!email || email.trim() == "") {
            message = "Email is required";
        }              
        else if (!roleType || roleType.trim() == "") {
            message = "Please select role type";
        }
        else if (!password || password.trim() == "") {
            message = "Password is required";
        }
        if (message != "") {
            $(defaults.errorMsg).show();
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
    var getPassword = function () {
        
        var op = defaults;

        api.fireGetAjax('/UserSetup/CreateRandomPassword', { PasswordLength:6})
            .done(result => {
                if (result != null) {

                    if (result.Status) {
                        
                        $(op.password).val(result.Data);
                    }
                    else {
                        $(op.errorMsg).html(result.Message);
                    }
                }
            })
            .fail(result => {
                $(op.errorMsg).html(result.responseText);
            });
    }
    var resetFields = function () {
        var op = defaults;
        $(op.firstName).val("");
        $(op.lastName).val("");
        $(op.email).val("");
        $(op.mobile).val("");
        $(op.password).val("");
        $(op.ddlRoleType).val("");
    };   
    var bindEvents = function () {
        var op = defaults;

        $(op.togglePassword).click(function () {

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
        });
        $userContext.on('click', op.btnRefresh, function (e) {
            $(op.password).val("");
            getPassword();
        });
        $userContext.on('click', op.btnCancelUser, function (e) {
            resetFields();
        });
        $userContext.on('change', op.ddlRoleType, function (e) {          
            getPassword();
        });
    };

    return {
        init: function (config) {
            $.extend(true, defaults, config);
            bindEvents();
            loadRoleTypes();     
            $(defaults.successMsg).hide();
            $(defaults.errorMsg).hide();
        }
    }
})();