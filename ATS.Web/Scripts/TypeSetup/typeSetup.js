var typeSetup  = (function () {
    'use strict'
    var defaults = {
        typeName: '.typeName',
        selectParent: '.ddlParents',
        typeValue: '.typeValue',
        selectStatus: '.statusId',
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
                            $(op.tableContext).find('tbody').html(result.Data);
                            loadParentTypes();
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
        var parentKey = $(op.selectParent).val();
        var statusId = $(op.selectStatus).val();

        flag = validateRequiredField(typeName, typeValue, parentKey, statusId);

        if (flag) {

            var typeDef = {
                Description: typeName.trim(),
                Value: typeValue.trim(),
                ParentKey: parentKey.trim(),
                StatusId: statusId.trim()
            };

            api.firePostAjax('/Admin/Setup/CreateType', { typeDef: typeDef })
                .done(callBacks.onTypeCreated)
                .fail(callBacks.onTypeCreationFailed);

        }

    };

    var validateRequiredField = function (typeName, typeValue, selectParent, statusId) {

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

    var loadParentTypes = function () {
        var op = defaults;

        api.fireGetAjax('/Setup/GetParentTypes', { })
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
                            $(op.selectParent).html(items);
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

    var loadStatus = function () {
        var op = defaults;

        api.fireGetAjax('/Setup/GetStatus', {})
            .done(res => {
                if (res != null) {
                    if (res.Status) {
                        if (res.Message) {
                            $(op.errorMsg).html(res.Message);
                        }
                        else {
                            var items = "";
                            $.each(res.Data, function (index, value) {
                                items += "<option value='" + value.Value + "'>" + value.Text + "</option>";
                            });
                            $(op.selectStatus).html(items);
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

    var loadAllTypes = function () {
        
        var op = defaults;

        api.fireGetAjax('/Setup/GetAllTypes', {})
            .done(callBacks.onTypeCreated)
            .fail(callBacks.onTypeCreationFailed);
    }

    var bindEvents = function () {
        var op = defaults;
        var $typeContext = $(op.typeContext);

        $typeContext.on('click', op.btnCreateType, function (e) {
            createType();
        });

        loadDefaults();
    };

    var loadDefaults=function() {
        loadParentTypes();
        loadStatus();
        loadAllTypes();
    }

    return {
        init: function (config) {
            $.extend(true, defaults, config);
            bindEvents(); 
        }
    }
})();