﻿var result = (function () {
    'use strict'
    var defaults = {
        mainMessageContext: '#mainMessageContext',
        typeContextModel: '#typeContextModel',
        selecttblUserList: '#tblUserList',
        selecttblUserAnswerList: '#tblUserAnswerList',
        btnConsolidatedResult: '#btnConsolidatedResult',
        btnIndividualResult: "btnIndividualResult",
        btnchkAllUserinfo: '#chkUserinfo',
    };

    var allUserIdList = [];
    var _chartContext = "", _titleText = "";
    var _data=[];    

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

        var reInitializeCartValue = function (result) {

            var testData = result.Data;
            _chartContext = "chartContainer";
            _titleText = "Consolidated Test results";
            _data = [];
            var _labels = [];
            var _internalData = [];
            var _dataSet = [];

            type: "bar",
                showInLegend: true,
                    legendText: "Gold",
                        color: "gold",
                            dataPoints: [
                                { y: 198, label: "Italy" },
                                { y: 201, label: "China" },
                                { y: 202, label: "France" },
                                { y: 236, label: "Great Britain" },
                                { y: 395, label: "Soviet Union" },
                                { y: 957, label: "USA" }
                            ]

            $.each(testData, function (index1, value1) {
                _data.push();
            });

            renderChart(_chartContext, _titleText, _data);

            $(defaults.typeContextModel).modal("hide");
        }


        return {
            onUserListSuccess: function (result) {
                appendUser(result);
            },
            onUserListFailed: function (result) {
                alertService.showError(result.responseText, op.popupMessageContext);
            },

            onConsolidatedResultSuccess: function (result) {
                reInitializeCartValue(result);
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

    var renderChart = function (_chartContext,_titleText, _data) {
        var chart = new CanvasJS.Chart(_chartContext,
            {
                title: {
                    text: _titleText
                    //"Olympic Medals of all Times (till 2012 Olympics)"
                },
                data: _data
                //   [                  // sample data
                //    {
                //        type: "bar",    // type:"column" -> for rotate it to x -axis
                //        dataPoints: [
                //            { y: 198, label: "Italy" },
                //            { y: 201, label: "China" },
                //            { y: 202, label: "France" },
                //            { y: 236, label: "Great Britain" },
                //            { y: 395, label: "Soviet Union" },
                //            { y: 957, label: "USA" }
                //        ]
                //    },
                //    ....
                //    ....
                //    ....
                //]
            });

        chart.render();
    };

    var bindEvents = function () {
        var op = defaults;
        var $tableUserContext = $(op.typeContextModel);

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

    var init = function () {
        renderChart();
    }

    return {
        init: function (config) {
            $.extend(true, defaults, config);
            init();
            bindEvents();
            loadUserList();
        }
    }
})();