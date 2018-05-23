var result = (function () {
    'use strict'
    var defaults = {
        mainMessageContext: '#mainMessageContext',
        userContextModel: '#userContextModel',
        selecttblUserList: '#tblUserList',        
        btnchkAllUserinfo: '#chkUserinfo',
        consolidatedChartContainer: '#consolidatedChartContainer',
        individualChartContainer: '#IndividualChartContainer',
        individualButton: '.individualButton',
        consolidatedButton: '.consolidatedButton',
        chartContext: '.chartContext',
        popupMessageContext: '#popupMessageContext'
    }; 

    var allUserIdList = [];

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

        var reInitializeConsolidatedChartValue = function (result) {
            var op = defaults;
            alertService.hide(result.mainMessageContext);

            if (result.Status) {
                if (result.Message && result.Message.length > 0) {
                    alertService.showAllErrors(result.Message, result.mainMessageContext);
                }
                else {
                    var $chartContext = $(defaults.consolidatedChartContainer);
                    $chartContext.empty();
                    var _newChartContext = "";
                    var _labelAngle = "0";
                    var _data = [];
                    var _titleText = "Consolidated result";
                    var _yAxixTitle = "Marks"
                    var _chartName = "chart";

                    _data = result.Data;

                    _newChartContext = "consolidatedChartContext" + 0;
                    $chartContext.append("<div id='" + _newChartContext + "'  style='min-height: 400px; width: 100 %;'></div><br>");

                    renderChart(_chartName, _newChartContext, _yAxixTitle, _titleText, _labelAngle, 100, _data);

                    $(defaults.userContextModel).modal("hide");
                }
            }
        }

        var reInitializeIndividualChartValue = function (result) {
            var op = defaults;

            alertService.hide(result.mainMessageContext);
            if (result.Status) {
                if (result.Message && result.Message.length > 0) {
                    alertService.showAllErrors(result.Message,result.mainMessageContext);
                }
                else {
                    var $chartContext = $(defaults.individualChartContainer);
                    $chartContext.empty();
                    var _newChartContext = "";
                    var _labelAngle = "0";
                    var _data = {};
                    var _titleText = "Individual result";
                    var _yAxixTitle = "Marks"
                    var _chartName = "";
                    for (let i = 0; i < result.Data.length; i++) {
                        _chartName = "chart" + i;
                        _newChartContext = "individualChartContext" + i;
                        $chartContext.append("<div id='" + _newChartContext + "'  style='min-height: 300px; width: 100 %;'></div><br>");
                        _data = result.Data[i];
                        renderChart(_chartName, _newChartContext, _yAxixTitle, _titleText, _labelAngle, 100, [_data]);
                    }

                    $(defaults.userContextModel).modal("hide");
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

            onConsolidatedResultSuccess: function (result) {
                reInitializeConsolidatedChartValue(result);
            },

            onConsolidatedResultFailed: function (result) {
                alertService.showAllErrors(result.responseText, op.mainMessageContext);
            },

            onIndividualResultSuccess: function (result) {
                reInitializeIndividualChartValue(result)
            },

            onIndividualResultFailed: function (result) {
                alertService.showAllErrors(result.responseText, op.mainMessageContext);
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

        if (allUserIdList.length === 0) {
            alertService.showError("Please select at least one user to proceed", defaults.mainMessageContext);
            $(defaults.userContextModel).modal("hide");
            $(defaults.individualChartContainer).empty();
            $(defaults.consolidatedChartContainer).empty();
        }
    }

    var GetConsolidatedTestResults = function () {
        alertService.hide(defaults.mainMessageContext);
        addUserList();

        if (allUserIdList.length >= 0) {
            api.firePostAjax('/Admin/ResultSetup/GetConsolidatedTestResults', { allUserIdList: allUserIdList })
                .done(callBacks.onConsolidatedResultSuccess)
                .fail(callBacks.onConsolidatedResultFailed)
        }
    }

    var GetIndividualTestResults = function () {
        alertService.hide(defaults.mainMessageContext);
        addUserList();

        if (allUserIdList.length >= 0) {
            api.firePostAjax('/Admin/ResultSetup/GetIndividualTestResults', { allUserIdList: allUserIdList })
                .done(callBacks.onIndividualResultSuccess)
                .fail(callBacks.onIndividualResultFailed)
        }
    }

    var renderChart = function (_chartName, _chartContext,_yAxixTitle,_title, _labelAngle,_maximum, _data) {
        _chartName = new CanvasJS.Chart(_chartContext,
            {
                title: {
                    text: _title
                },
                axisX: {
                    labelAngle: _labelAngle
                },
                axisY: {
                    title: _yAxixTitle,
                    maximum: _maximum
                },
                data:_data
            });

        _chartName.render();
    };

    var bindEvents = function () {
        var op = defaults;
        var $userContextModel = $(op.userContextModel);
        var $chartContext = $(op.chartContext);

        $userContextModel.on('click', op.btnchkAllUserinfo, function (event) {
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

        $userContextModel.on('click', op.consolidatedButton, function (event) {
            GetConsolidatedTestResults();
           $(op.individualChartContainer).hide();
           $(op.consolidatedChartContainer).show();
        });

        $chartContext.on('click', op.consolidatedButton, function (event) {
            GetConsolidatedTestResults();
            $(op.individualChartContainer).hide();
            $(op.consolidatedChartContainer).show();
        });

        $chartContext.on('click', op.individualButton, function (event) {
            GetIndividualTestResults();
            $(op.consolidatedChartContainer).hide();
            $(op.individualChartContainer).show();
        });
    };
   
    return {
        init: function (config) {
            $.extend(true, defaults, config);
            bindEvents();
            loadUserList();           
        }
    }
})();