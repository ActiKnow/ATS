var mapTestUser = (function () {
    'use strict';
    var defaults = {};
    const activeText = AppConstant.ACTIVE, inactiveText = AppConstant.INACTIVE;
    var selectedUserList = [];
    var _prevItemRow = null;

    var apiUrl = {
        getTests: '/Admin/TestAssignment/GetTests/'
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
                        alertService.showSuccess(msg, op.popupUserMessageContext);
                    }
                    $(op.selecttblUserList).find('tbody').html(result.Data);
                    $(op.selecttblUserList).DataTable();
                }
                else {
                    $.each(result.Message, function (index, value) {
                        msg += value;
                    });
                    alertService.showError(msg, op.popupUserMessageContext);
                }
            }
        }
        var appendTest = function (result) {
            if (result !== "") {
                var msg = " ";
                if (result.Status) {
                    if (result.Message && result.Message.length > 0) {
                        $.each(result.Message, function (index, value) {
                            msg += value;
                        });
                        alertService.showSuccess(msg, op.popupTestMessageContext);
                    }
                    $(op.selecttblTestList).find('tbody').html(result.Data);
                    $(op.selecttblTestList).DataTable();
                    $(op.modalSelectTestContext).show();
                }
                else {
                    $.each(result.Message, function (index, value) {
                        msg += value;
                    });
                    alertService.showError(msg, op.popupTestMessageContext);
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
            onTestListSuccess: function (result) {
                appendTest(result);
            },
            onTestListFailed: function (result) {
                alertService.showError(result.responseText, op.popupMessageContext);
            },
        }
    })();
    var loadUserList = function () {
        var op = defaults;
        api.fireGetAjax('/Admin/TestAssignment/GetAllUsers', {})
            .done(callBacks.onUserListSuccess)
            .fail(callBacks.onUserListFailed);
    }

    var loadTestList = function () {
        var op = defaults;
        api.fireGetAjax('/Admin/TestAssignment/GetTests', { rawTests: true })
            .done(callBacks.onTestListSuccess)
            .fail(callBacks.onTestListFailed);
    }

    var addUserList = function () {

        allUserIdList = [];
        var listUser = "";
        var $selectTable = $(defaults.selecttblUserList);
        var $tblBody = $selectTable.find('tbody');
        $tblBody.find(':checkbox:checked').each(function () {
            var recID = $(this).val();

            if (recID) {
                allUserIdList.push(recID);
            }

             listUser += "<tr>" +
                 "<td><span class='FirstLastName'>" + FName + "</span></td>" +
                 "<td><span class='Email'>" + Email + "</span></td>" +
                 "<td><span class='RoleType'>" + RoleDescription + "</span></td>" +
                 "<td><span class='Mobile'>" + Mobile + "</span>< input type = 'hidden' class='userId' value = '" + recID + "' />< input type='hidden' class='roleTypeId' value='" + RoleTypeValue + "' /></td > " +
                 "</tr>";

        });

        $(defaults.tblSelectedUsersList).find('tbody').html(listUser);
         //$(op.tblSelectedUsersList).DataTable();
    }
    var addTestList = function () {

        allTestBankIdList = [];
        var $selectTable = $(defaults.selecttblTestList);
        var $tblBody = $selectTable.find('tbody');
        $tblBody.find(':checkbox:checked').each(function () {
            var recID = $(this).val();

            if (recID) {
                allTestBankIdList.push(recID);
            }
        });
    }
    var render = {
        openUserList: function () {
            var op = defaults;
            var $userModal = $(op.modalSelectUserContext);
            loadUserList();
            $userModal.modal('show');
        },
        openTestList: function () {
            var op = defaults;
            var $testModal = $(op.modalSelectTestContext);
            loadTestList();
            $testModal.modal('show');
        },
        closeSelectUser: function () {
            var op = defaults;
            var $userModal = $(op.modalSelectUserContext);
            $userModal.modal('hide');
        },
        closeSelectTest: function () {
            var op = defaults;
            var $testModal = $(op.modalSelectTestContext);
            $testModal.modal('hide');
        },
    };
    var loader = {
        loadEvents: function () {
            var op = defaults;
            var $testContext = $(op.testContext);
            var $userContext = $(op.userContext);
            var $selectUserContext = $(op.modalSelectUserContext);
            var $selectTestContext = $(op.modalSelectTestContext);

            $selectUserContext.on('click', op.btnchkAllUserinfo, function (event) {
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

            $userContext.on('click', op.openUserList, render.openUserList);
            $testContext.on('click', op.openTestList, render.openTestList);
            $selectUserContext.on('click', op.closeSelectUser, render.closeSelectUser);
            $selectTestContext.on('click', op.closeSelectTest, render.closeSelectTest);

            //$testDataContext.advancedTable({
            //    rowActiveClass:'btn-primary',
            //} );
        },
    };
    var setup = function () {
        for (let index in loader) {
            if (typeof loader[index] == 'function') {
                loader[index]();
            }
        }
    };
    var init = function (config) {
        $.extend(true, defaults, config);
        setup();
    };
    return {
        init: init
    };
})();