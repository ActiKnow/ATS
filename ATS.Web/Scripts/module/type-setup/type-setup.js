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
        var op = defaults;

        var appendType = function (result) {
            if (result !== "") {
                var msg = " ";
                if (result.Status) {
                    if (result.Message && result.Message.length > 0) {
                        $.each(result.Message, function (index, value) {
                            msg += value.Message;
                        });
                        $(op.errorMsg).html(msg);
                    }
                    else {
                        $(op.tableContext).find('tbody').html(result.Data);
                        loadParentTypes();
                        $(op.btnClearType).trigger("click");
                    }
                }
                else {
                    $.each(result.Message, function (index, value) {
                        msg += value.Message;
                    });
                    $(op.errorMsg).html(msg);
                }
            }
        }

        return {
            onTypeCreated: function (result) {
                appendType(result);
            },
            onTypeCreationFailed: function (result) {
                $(op.errorMsg).html(result.responseText);
            },

            onTypeUpdated: function (result) {
                appendType(result);
            },
            onTypeUpdationFailed: function (result) {
                $(op.errorMsg).html(result.responseText);
            },
        }
    })();

    var createType = function () {

        var flag = true;
        var op = defaults;

        var typeName = $(op.typeName).val();
        var parentKey = $(op.selectParent).val();
        var statusId = $(op.selectStatus).val();

        flag = validateRequiredField(typeName, parentKey, statusId);

        if (flag) {

            var typeDef = {
                Description: typeName.trim(),
                ParentKey: parentKey.trim(),
                StatusId: statusId.trim()
            };

            api.firePostAjax('/Admin/Setup/CreateType', { typeDef: typeDef })
                .done(callBacks.onTypeCreated)
                .fail(callBacks.onTypeCreationFailed);

        }
    };

    var updateType = function () {

        var flag = true;
        var op = defaults;

        var typeName = $(op.typeName).val();
        var typeValue = $(op.typeValue).val();
        var parentKey = $(op.selectParent).find(":selected").val();
        var statusId = $(op.selectStatus).find(":selected").val();
        var typeId = $(op.typeId).html();

        flag = validateRequiredField(typeName, typeValue, parentKey, statusId);

        if (flag) {

            var typeDef = {
                Description: typeName.trim(),
                Value: typeValue.trim(),
                ParentKey: parentKey.trim(),
                StatusId: statusId.trim(),
                TypeId: typeId.trim(),
            };

            api.firePostAjax('/Admin/Setup/UpdateType', { typeDef: typeDef })
                .done(callBacks.onTypeUpdated)
                .fail(callBacks.onTypeUpdationFailed);
        }

    }

    var validateRequiredField = function (typeName, selectParent, statusId) {

        var flag = true;
        var message = "";

        if (!typeName || typeName.trim() == "") {
            message = "Type name is required";
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
                    var msg = " ";
                    var items = "<option value=''>-Select-</option>";
                    if (res.Status) {
                        if (res.Message && res.Message.length > 0) {
                            $.each(res.Message, function (index, value) {
                                msg += value.Message;
                            });
                            $(op.errorMsg).html(msg);
                        }
                        else {
                            $.each(res.Data, function (index, value) {
                                items += "<option value='" + value.Value + "'>" + value.Description + "</option>";
                            });
                            $(op.selectParent).html(items);
                        }
                    }
                    else {
                        $.each(result.Message, function (index, value) {
                            msg += value.Message;
                        });
                        $(op.errorMsg).html(msg);
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
                    var msg = " ";
                    if (res.Status) {
                        if (res.Message && res.Message.length > 0) {
                            $.each(res.Message, function (index, value) {
                                msg += value.Message;
                            });
                            $(op.errorMsg).html(msg);
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
                        $.each(res.Message, function (index, value) {
                            msg += value.Message;
                        });
                        $(op.errorMsg).html(msg);
                    }
                }
            })
            .fail(res => {
                $(op.errorMsg).html(res.responseText);
            });
    }

    var loadAllTypes = function () {
        
        var op = defaults;

        api.fireGetAjax('/Admin/Setup/GetAllTypes', {})
            .done(callBacks.onTypeCreated)
            .fail(callBacks.onTypeCreationFailed);
    }

    var validateType = function () {

        var op = defaults;
        $(op.btnCreateType).removeAttr("disabled"); 
        
        var typeName = $(op.typeName).val().trim();
      
        if (typeName) {
            api.fireGetAjax('/Admin/Setup/ValidateType', { typeName: typeName})
                .done(res => {
                    if (res != null) {
                        var msg = " ";
                        if (!res.Status) {
                            if (res.Message && res.Message.length > 0) {
                                $.each(result.Message, function (index, value) {
                                    msg += value.Message;
                                });
                                $(op.errorMsg).html(msg);
                                $(op.btnCreateType).attr("disabled", "disabled");
                            }                           
                        }
                        else {
                            $.each(result.Message, function (index, value) {
                                msg += value.Message;
                            });
                            $(op.errorMsg).html(msg);
                            $(op.btnCreateType).attr("disabled", "disabled");
                        }
                    }
                })
                .fail(res => {
                    $(op.errorMsg).html(res.responseText);
                });
        }
    }

    var bindEvents = function () {
        var op = defaults;
        var $typeContext = $(op.typeContext);
        var $tableContext = $(op.tableContext);

        $typeContext.on('click', op.btnCreateType, function (e) {
            createType();
        });

        $typeContext.on('focusout', op.typeName, function (e) {
            validateType();
        });

        //$typeContext.on('focusout', op.typeValue, function (e) {
        //    validateType();
        //}); 

        $typeContext.on('click', op.btnClearType, function (e) {
            $(defaults.typeName).val("").removeAttr("readonly");
            $(defaults.typeValue).val("").removeAttr("readonly");
            $(defaults.selectParent).val("");
            $(defaults.selectStatus).val("True");
            $(defaults.typeId).html("");
            $(defaults.btnUpdateType).hide();
            $(defaults.btnCreateType).show();
        });

        $tableContext.on('click', op.btnEditType, function (e) {

            $(defaults.btnUpdateType).show();
            $(defaults.btnCreateType).hide();

            var $row = $(this).closest("tr");

            $(op.typeName).val($row.find($(op.selectedItemName)).html()).attr("readonly", "readonly");
            $(op.typeValue).val($row.find($(op.selectedItemValue)).html()).attr("readonly", "readonly");
            $(op.selectParent).val($row.find($(op.selectedParentTypeId)).html());
            $(op.typeId).html($row.find($(op.selectedTypeId)).html());
            $(op.selectStatus).val($row.find($(op.selectedItemStatus)).html()); 
        });

        $typeContext.on('click', op.btnUpdateType, function (e) {
            updateType();
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