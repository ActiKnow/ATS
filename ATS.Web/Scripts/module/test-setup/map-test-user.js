var mapTestUser = (function () {
    'use strict';
    var defaults = {};
    var selectedUser = {};
    const activeText = AppConstant.ACTIVE, inactiveText = AppConstant.INACTIVE;
    var selectedUserList = [];
    var _prevItemRow = null;

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

    var action = {
        
        setSelectedUser: function ($row) {
            var op = defaults;
            selectedUser = action.resetSelectedUser();
            selectedUser.userId = $row.find(op.selectedUserId).val();
            selectedUser.userName = $row.find(op.selectedFirstLastName).text();
            selectedUser.emailId = $row.find(op.selectedEmail).text();
            selectedUser.userType = $row.find(op.selectedRoleType).text();
            selectedUser.mobile = $row.find(op.selectedMobile).text();
            render.fillSelectedUser(selectedUser);
            render.closeSelectUser();
            loadMappedTestList();
        },
         resetSelectedUser: function () {
            return {
                userId: "",
                userName: "",
                emailId: '',
                userType: '',
                mobile: ''
            };
        },
        fillMappedTest: function (data) {
            var op = defaults;
            var count = 0;
            var $tblAssignedList = $(op.selecttblAssignedList);
            var selectQues = '';
            var listTest = '';
            var rowContents = [];

            $.each(data, (indx, value) => {

                //if (testQuestions && (testQuestions.indexOf(value.QId) == -1)) {
                //string.Format("{0} ( {1} / {2} mins )",
                count++;
                listTest += "<tr>" +
                    "<td><span >" + count + "</span></td>" +
                    "<td><span class=''>" + (value.Description + value.TotalMarks + value.Duration) + "</span></td>" +
                    "<td><span class=''>" + value.CategoryTypeDescription + "</span></td>" +
                    "<td><span class=''>" + value.TestTypeDescription + "</span></td>" +
                    "<td><span class=''>" + value.LevelTypeDescription + "</span><input type = 'hidden' class='userId' value = '" + value.TestBankId + "' /></td > " +
                    "<td><button class='btn-primary btn-xs deleteMappedTest'><i class='fa fa-remove' aria-hidden='true'></i></button></td>"+


                    "</tr>";

            });
            $(defaults.selecttblAssignedList).find('tbody').html(listTest);
            $(defaults.selecttblAssignedList).DataTable();
            // $(defaults.modalSelectTestContext).modal('hide');
        },
         
    };


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
        var appendUnmappedTest = function (result) {
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
                }
                else {
                    $.each(result.Message, function (index, value) {
                        msg += value;
                    });
                    alertService.showError(msg, op.popupTestMessageContext);
                }
            }
        }
        var appendMappedTest = function (result) {
            if (result !== "") {
                var msg = " ";
                if (result.Status) {
                    //if (result.Message && result.Message.length > 0) {
                        //$.each(result.Message, function (index, value) {
                        //    msg += value;
                        //});

                        //alertService.showSuccess(msg, op.popupTestMessageContext);
                    action.fillMappedTest(result.Data);
                    
                    //}                   
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
                    $(op.modalSelectTestContext).hide();
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
            onUnmappedListSuccess: function (result) {
                appendUnmappedTest(result);
            },
            onUnmappedListFailed: function (result) {
                alertService.showError(result.responseText, op.popupMessageContext);
            },
            onTestAssignSuccess: function (result) {
                appendAssignList(result);
                loadMappedTestList();
            },
            onTestAssignFailed: function (result) {
                alertService.showError(result.responseText, op.popupMessageContext);
            },
            onMappedListSuccess: function (result) {
                appendMappedTest(result);
            },
            onMappedListFailed: function (result) {
                alertService.showError(result.responseText, op.popupMessageContext);
            },
        }
    })();
    var loadUserList = function () {
        var op = defaults;
        api.fireGetAjax('/Admin/TestAssignment/GetAllUsers', {})
            .done(callBacks.onUserListSuccess)
            .fail(callBacks.onUserListFailed);
    };
    var loadUnmappedTestList = function () {
        var op = defaults;
        var userId = $(op.userId).val();
        api.fireGetAjax('/Admin/TestAssignment/SelectUnmappedTest', { userId: userId })
            .done(callBacks.onUnmappedListSuccess)
            .fail(callBacks.onUnmappedListFailed);
    };
    var loadMappedTestList = function () {
        var op = defaults;
        var userId = $(op.userId).val();
        api.fireGetAjax('/Admin/TestAssignment/SelectMappedTest', { userId: userId })
            .done(callBacks.onMappedListSuccess)
            .fail(callBacks.onMappedListFailed);
    };
    var assignTestList = function () {
        var op = defaults;
        var $selectTable = $(defaults.selecttblTestList);
        var $tblBody = $selectTable.find('tbody');
        var testAssignmentModel = [];

        $tblBody.find(':checkbox:checked').each(function () {
            var testBankId = $(this).val();
            var userId = $(op.userId).val();
            var item = {
                UserId: userId,
                TestBankId: testBankId,
                StatusId: true,
            };          
            testAssignmentModel.push(item);
        });
        
        api.firePostAjax('/Admin/TestAssignment/AssignTest', { testAssignmentModel: testAssignmentModel })
            .done(callBacks.onTestAssignSuccess)
            .fail(callBacks.onTestAssignFailed);
    };
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
            
            loadUnmappedTestList();
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
        openMappedList: function () {
            var op = defaults;

            loadMappedTestList();
         
        },

        fillSelectedUser: function (data) {
            data = data || action.resetSelectedUser();
            var op = defaults;
            $(op.userId).val(data.userId);
            $(op.userName).val(data.userName);
            $(op.mobile).val(data.mobile);
            $(op.userType).val(data.userType);
            $(op.emailId).val(data.emailId);
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
            $assignContext.on('click', op.openTestList, render.openTestList);
            $selectUserContext.on('click', op.closeSelectUser, render.closeSelectUser);
            $selectTestContext.on('click', op.closeSelectTest, render.closeSelectTest);
            //$selectTestContext.on('click', op.btnSubmitTestList, render.addTestList);
            //$assignContext.on('click', op.btnAssignTestList, render.assignTestList);
            $selectTestContext.on('click', op.btnAssignTestList, function (e) {
                assignTestList();
            });
            $(op.tableContextUser).advancedTable({
                rowActiveClass: 'row-active',
                tableRowdblClick: action.setSelectedUser,
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