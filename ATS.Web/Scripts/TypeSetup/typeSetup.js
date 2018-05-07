var typeSetup = (function () {
    'use strict'
    var defaults = {
        typeName: '.typeName',
        parentId: '.parentId',
        typeValue: '.typeValue',
        statusId: '.statusId',
        btnCreateType: '#btnCreateType',
        errorMsg: '.errorMsg',
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
                return fireAjax(url, data,"POST");
            },

            fireGetAjax: function (url, data) {
                return fireAjax(url, data,"GET");
            },
        }
    }());

    var callBacks = (function () {
        var op=defaults;
        return {
            onTypeCreated: function (result) {
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
            onTypeCreationFailed: function (result) {
                $(op.errorMsg).html(result.responseText);
            },
        }
    })();

    var createType = function () {

        var flag = true;

        var op = defaults;

        var typeName = $(op.typeName).val();
        var typeValue = $(op.typeValue).val();
        var parentId = $(op.parentId).val();
        var statusId = $(op.statusId).val();

        flag = validateRequiredField(typeName, typeValue, parentId, statusId);

        if (flag) {

            var typeDef = {
                Description: typeName.trim(),
                Value: typeValue.trim(),
                ParentKey: parentId.trim(),
                StatusId: statusId.trim()
            };

            api.createType('/Setup/CreateType', { typeDef: typeDef })
                .done(callBacks.onTypeCreated)
                .fail(callBacks.onTypeCreationFailed);

        }

    };

    var validateRequiredField = function (typeName, typeValue, parentId, statusId) {

        var flag = true;
        var message = "";

        if (!typeName || typeName.trim() == "") {
            message = "Type name is required";
        }
        else if (!typeValue || typeValue.trim() == "") {
            message = "Type value is required";
        }
        else if (!statusId || statusId.trim() == "") {
            message = "Please select status";
        }

        if (message != "")
        {
            $(defaults.errorMsg).html(message);
            flag = false;
        }

        return flag;
    }


    var bindEvents = function () {
        var op = defaults;
        var $selectQuestionContainer = $(op.selectContainer);

        $selectQuestionContainer.on('click', op.btnCreateQuestion, function (e) {
            createQuestion();
        })
    };

    return {
        init: function (config) {

            $.extend(true, defaults, config);
            bindEvents();
        }

    }
})();