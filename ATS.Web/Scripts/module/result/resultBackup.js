var result = (function () {
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

    var data = {
        labels: ["January", "February", "March", "April", "May"],     // shown on the x-axis
        datasets: [              // this data will populate in Line Chart , Bar Chart ,Radar Chart
            {
                label: "My First dataset",
                fillColor: "rgba(220,220,220,0.2)",
                strokeColor: "rgba(220,220,220,1)",
                pointColor: "rgba(220,220,220,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(220,220,220,1)",
                data: [65, 59, 80, 81, 56]        // shown on y-axis
            },
            {
                label: "My Second dataset",
                fillColor: "rgba(151,187,205,0.2)",
                strokeColor: "rgba(151,187,205,1)",
                pointColor: "rgba(151,187,205,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(151,187,205,1)",
                data: [28, 48, 40, 19, 86]
            }
        ]
    };
    var pdata = [                      // this data will populate in Polar Chart , Doughnut Chart , Pie Chart
        {
            value: 300,
            color: "#F7464A",
            highlight: "#FF5A5E",
            label: "Red"
        },
        {
            value: 50,
            color: "#46BFBD",
            highlight: "#5AD3D1",
            label: "Green"
        },
        {
            value: 100,
            color: "#FDB45C",
            highlight: "#FFC870",
            label: "Yellow"
        }
    ]

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
            var _labels = [];
            var _internalData = [];
            var _dataSet = [];

            $.each(testData, function (index1, value1) {
                _labels.push(value1.Description);
                $.each(value1.TestAssignments, function (index2, value2) {
                    _internalData.push(value2.UserId);
                });
                var _d = {
                    label: "My First dataset",
                    fillColor: "rgba(220,220,220,0.2)",
                    strokeColor: "rgba(220,220,220,1)",
                    pointColor: "rgba(220,220,220,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(220,220,220,1)",
                    data: _internalData        // shown on y-axis
                }
                _dataSet.push(_d);
            });

            data = {                
                labels: _labels,     // shown on the x-axis
                datasets: _dataSet
            };

            $(defaults.typeContextModel).modal("hide");
            renderChart();
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

    var renderChart = function () {
        var ctxl = $("#lineChartDemo").get(0).getContext("2d");
        var lineChart = new Chart(ctxl).Line(data);

        var ctxb = $("#barChartDemo").get(0).getContext("2d");
        var barChart = new Chart(ctxb).Bar(data);

        var ctxr = $("#radarChartDemo").get(0).getContext("2d");
        var radarChart = new Chart(ctxr).Radar(data);

        var ctxpo = $("#polarChartDemo").get(0).getContext("2d");
        var polarChart = new Chart(ctxpo).PolarArea(pdata);

        var ctxp = $("#pieChartDemo").get(0).getContext("2d");
        var pieChart = new Chart(ctxp).Pie(pdata);

        var ctxd = $("#doughnutChartDemo").get(0).getContext("2d");
        var doughnutChart = new Chart(ctxd).Doughnut(pdata);
    }

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