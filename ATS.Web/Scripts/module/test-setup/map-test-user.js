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
                   // $(op.modalSelectTestContext).show();
                }
                else {
                    $.each(result.Message, function (index, value) {
                        msg += value;
                    });
                    alertService.showError(msg, op.popupTestMessageContext);
                }
            }
        }
        
        var appendAssignList = function (result) {
            if (result !== "") {
                var msg = " ";
                if (result.Status) {
                    if (result.Message && result.Message.length > 0) {
                        $.each(result.Message, function (index, value) {
                            msg += value;
                        });
                        alertService.showSuccess(msg, op.mainMessageContext);
                    }
                    //$(op.selecttblTestList).find('tbody').html(result.Data);
                    //$(op.selecttblTestList).DataTable();
                    // $(op.modalSelectTestContext).show();
                }
                else {
                    $.each(result.Message, function (index, value) {
                        msg += value;
                    });
                    alertService.showError(msg, op.mainMessageContext);
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
            onTestAssignSuccess: function (result) {
                appendAssignList(result);
            },
            onTestAssignFailed: function (result) {
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
    var assignTestList = function () {
        var op = defaults;
        var userId = [];
        var testBankId = [];
        var $selectTable = $(defaults.selecttblUserList);
        var $tblBody = $selectTable.find('tbody');

        $tblBody.find('tr').each(function () {



        });




        alert();
        api.fireGetAjax('/Admin/TestAssignment/AssignTest', { testAssignmentModel: testAssignmentModel })
            .done(callBacks.onTestAssignSuccess)
            .fail(callBacks.onTestAssignFailed);
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

        addUserList: function () {

            var allUserIdList = [];
            var count = 0;
            var listUser = "";
            var $selectTable = $(defaults.selecttblUserList);
            var $tblBody = $selectTable.find('tbody');
            $tblBody.find(':checkbox:checked').each(function () {

                var recID = $(this).val();
                var data = $(this).parents('tr:eq(0)');
                var FName = $(data).find('td:eq(1)').text();
                var Email = $(data).find('td:eq(2)').text();
                var RoleDescription = $(data).find('td:eq(3)').text();
                var Mobile = $(data).find('td:eq(4)').text();
                var UserId = $(data).find('td:eq(4)').find($('.userId')).val();
                var RoleTypeValue = $(data).find('td:eq(4)').find($('.roleTypeId')).val();
                if (recID) {
                    allUserIdList.push(recID);
                }
                count++;
                listUser += "<tr>" +
                    "<td><span >" + count + "</span></td>" +
                    "<td><span class='FirstLastName'>" + FName + "</span></td>" +
                    "<td><span class='Email'>" + Email + "</span></td>" +
                    "<td><span class='RoleType'>" + RoleDescription + "</span></td>" +
                    "<td><span class='Mobile'>" + Mobile + "</span><input type = 'hidden' class='userId' value = '" + recID + "' /><input type='hidden' class='roleTypeId' value='" + RoleTypeValue + "' /></td > " +
                    "</tr>";

            });

            $(defaults.tblSelectedUsersList).find('tbody').html(listUser);
            $(defaults.tblSelectedUsersList).DataTable();
            $(defaults.modalSelectUserContext).modal('hide');
        },
        addTestList: function () {

            var allTestBankIdList = [];
            var count = 0;
            var listTest = "";
            var $selectTable = $(defaults.selecttblTestList);
            var $tblBody = $selectTable.find('tbody');
            $tblBody.find(':checkbox:checked').each(function () {
                var data = $(this).parents('tr:eq(0)');
                var TestDescription = $(data).find('td:eq(1)').text();
                var Category = $(data).find('td:eq(2)').text();
                var Type = $(data).find('td:eq(3)').text();
                var Level = $(data).find('td:eq(4)').text();
                var TestId = $(data).find('td:eq(4)').find($('.test-id')).val();
               
                var recID = $(this).val();
                if (recID) {
                    allTestBankIdList.push(recID);
                }
                count++;
                listTest += "<tr>" +
                    "<td><span >" + count + "</span></td>" +
                    "<td><span class='FirstLastName'>" + TestDescription + "</span></td>" +
                    "<td><span class='Email'>" + Category + "</span></td>" +
                    "<td><span class='RoleType'>" + Type + "</span></td>" +
                    "<td><span class='Mobile'>" + Level + "</span><input type = 'hidden' class='userId' value = '" + TestId + "' /></td > " +
                    "</tr>";
            });
            $(defaults.tblSelectedTestsList).find('tbody').html(listTest);
            $(defaults.tblSelectedTestsList).DataTable();
            $(defaults.modalSelectTestContext).modal('hide');
        },

        

    };
    var loader = {
        loadEvents: function () {
            var op = defaults;
            var $testContext = $(op.testContext);
            var $userContext = $(op.userContext);
            var $assignContext = $(op.assignContext);
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

            $selectTestContext.on('click', op.btnchkAllTestinfo, function (event) {
                if (this.checked) {
                    $(op.selecttblTestList).find(':checkbox').each(function () {
                        this.checked = true;
                    });
                }
                else {
                    $(op.selecttblTestList).find(':checkbox').each(function () {
                        this.checked = false;
                    });
                }
            });


            $userContext.on('click', op.openUserList, render.openUserList);
            $testContext.on('click', op.openTestList, render.openTestList);
            $selectUserContext.on('click', op.closeSelectUser, render.closeSelectUser);
            $selectTestContext.on('click', op.closeSelectTest, render.closeSelectTest);
            $selectUserContext.on('click', op.btnSubmitUsersList, render.addUserList);
            $selectTestContext.on('click', op.btnSubmitTestList, render.addTestList);
            //$assignContext.on('click', op.btnAssignTestList, render.assignTestList);
            $assignContext.on('click', op.btnAssignTestList, function (e) {
                assignTestList();
            });

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