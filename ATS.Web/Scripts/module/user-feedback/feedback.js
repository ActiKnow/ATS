var userFeedback = (function () {
    'use strict'
    var defaults = {
        mailboxContext: '#mailboxContext',
        tblMailBoxContext: '#tblMailBoxContext',
        inboxUnreadCount: '#inboxUnreadCount',
        deletedCount: '#deletedCount',
        selectedFeedBackId: '.selectedFeedBackId',
        checkBoxAll: '.checkBoxAll',
        deleteSelected: "#deleteSelected",
        replySelected: "#replySelected",
        shareSelected: "#shareSelected",
        refreshFeedback: "#refreshFeedback",
        mainMessageContext: '#mainMessageContext',
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
    })();

    var getAllCheckedIds = function () {

        var allSelectedIds = [];

        var $selectTable = $(defaults.tblMailBoxContext);
        var $tblBody = $selectTable.find('tbody');

        $tblBody.find(':checkbox:checked').each(function () {
            var recID = $(this).val();

            if (recID) {
                allSelectedIds.push(recID);
            }
        });

        return allSelectedIds;
    }

    var getInboxCount = function () {
        var op = defaults;
        alertService.hide(op.mainMessageContext);

        api.fireGetAjax('/Feedback/InboxCount', {})
            .done(res => {
                if (res) {
                    if (res.Status) {
                        $(op.inboxUnreadCount).html(res.Data);
                    }
                    if (res.Message && res.Message.length > 0) {
                        alertService.showAllErrors(res.Message, op.mainMessageContext);
                    }
                }
            })
            .fail(res => {
                alertService.showAllErrors(res.responseText, op.mainMessageContext);
            });
    }

    var getTotalCount = function () {
        var op = defaults;
        alertService.hide(op.mainMessageContext);

        api.fireGetAjax('/Feedback/TotalCount', {})
            .done(res => {
                if (res) {
                    if (res.Status) {
                        $(op.totalInboxCount).html(res.Data);
                    }
                    if (res.Message && res.Message.length > 0) {
                        alertService.showAllErrors(res.Message, op.mainMessageContext);
                    }
                }
            })
            .fail(res => {
                alertService.showAllErrors(res.responseText, op.mainMessageContext);
            });
    }

    var selectFeedbacks = function () {
        var op = defaults;
        alertService.hide(op.mainMessageContext);

        api.fireGetAjax('/Admin/Feedback/Select', {})
            .done(res => {
                if (res) {
                    if (res.Status) {
                        $(op.tblMailBoxContext).find("tbody").html(res.Data);
                    }
                    if (res.Message && res.Message.length > 0) {
                        alertService.showAllErrors(res.Message, op.mainMessageContext);
                    }
                }
            })
            .fail(res => {
                alertService.showAllErrors(res.responseText, op.mainMessageContext);
            });
    }

    var retrieveFeedback = function () {
        var op = defaults;
        alertService.hide(op.mainMessageContext);

        var id = "";
        
        api.fireGetAjax('/Feedback/Retrieve', { id })
            .done(res => {
                if (res) {
                    if (res.Status) {
                        $(op.tblMailBoxContext).find("tbody").html(res.Data);
                    }
                    if (res.Message && res.Message.length > 0) {
                        alertService.showAllErrors(res.Message, op.mainMessageContext);
                    }
                }
            })
            .fail(res => {
                alertService.showAllErrors(res.responseText, op.mainMessageContext);
            });
    }

    var deleteFeedbacks = function () {
        var op = defaults;
        alertService.hide(op.mainMessageContext);

        var Ids = getAllCheckedIds();

        if (Ids != null && Ids.length > 0) {
            api.firePostAjax('/Feedback/Delete', { Ids })
                .done(res => {
                    if (res) {
                        if (res.Status) {
                            $(op.tblMailBoxContext).find("tbody").html(res.Data);
                        }
                        if (res.Message && res.Message.length > 0) {
                            alertService.showAllErrors(res.Message, op.mainMessageContext);
                        }
                    }
                })
                .fail(res => {
                    alertService.showAllErrors(res.responseText, op.mainMessageContext);
                });
        }
        else {
            alertService.showError("Select feedbacks to delete", op.mainMessageContext);
        }
    }

    var bindEvents = function () {
        var op = defaults;   
        var $inboxContext = $(op.inboxContext);
        var $mailboxContext = $(op.mailboxContext);

        $inboxContext.on("click", op.inboxUnreadCount, function () {

        });

        $mailboxContext.on("click", op.deleteSelected, function () {
            deleteFeedbacks();
        });

        $mailboxContext.on("click", op.refreshFeedback, function () {
            getInboxCount();
            getTotalCount();
            selectFeedbacks();
        });

        $mailboxContext.advancedTable({
            tableEnterPress: function (row) {
                retrieveFeedback(row);
            },

            tableRowdblClick: function (row) {
                retrieveFeedback(row);
            },
        });        
    };

    var refreshFeedbacks = function () {
        $(defaults.refreshFeedback).trigger("click");
    };

    var init=function (config) {
        $.extend(true, defaults, config);
        bindEvents();
        setTimeout(refreshFeedbacks, 1000);
    }

    return {
        init:init
    }
})();