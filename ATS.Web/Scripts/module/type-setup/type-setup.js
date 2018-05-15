var typeSetup  = (function () {
    'use strict'
    var defaults = {
        typeName: '.typeName',
        selectParent: '.ddlParents',
        typeValue: '.typeValue',
        selectStatus: '.statusId',
        btnCreateType: '#btnCreateType',
        errorMsg: '.errorMsg',
        popupMessageContext: '#popupMessageContext',
        mainMessageContext: '#mainMessageContext'
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
                        successMessageShow(result.Message, op.mainMessageContext);                        
                    }
                    $(op.modelTypeContext).modal('hide');
                    $(op.tableContext).find('tbody').html(result.Data);
                    loadParentTypes();
                    $(op.btnClearType).trigger("click");
                }
                else {
                    errorMessageShow(result.Message, op.popupMessageContext);
                }
            }
        }

        return {
            onTypeCreated: function (result) {
                appendType(result);
            },
            onTypeCreationFailed: function (result) {
                alertService.showError(result.responseText, defaults.mainMessageContext);
            },

            onTypeUpdated: function (result) {
                appendType(result);
            },
            onTypeUpdationFailed: function (result) {
                alertService.showError(result.responseText, defaults.mainMessageContext);
            },
        }
    })();

    var createType = function () {

        var flag = true;
        var op = defaults;

        var typeName = $(op.typeName).val();
        var parentKey = $(op.selectParent).find(":selected").val();
        var statusId = $(op.selectStatus).find(":selected").val();

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
        var parentKey = $(op.selectParent).find(":selected").val();
        var statusId = $(op.selectStatus).find(":selected").val();
        var typeId = $(op.typeId).html();
        var typeValue = $(op.typeValue).html();

        flag = validateRequiredField(typeName, parentKey, statusId);

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

        alertService.hide(defaults.popupMessageContext);

        var flag = true;
        var message = "";

        if (!typeName || typeName.trim() == "") {
            message = "Type name is required";
        }
        else if (!statusId || statusId.trim() == "") {
            message = "Type status is required";
        }
        else if (!selectParent || selectParent.trim() == "") {
            message = "Parent type is required";
        }

        if (message != "")
        {
            alertService.showError(message, defaults.popupMessageContext);
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
                            errorMessageShow(res.Message, op.mainMessageContext);
                        }
                        else {
                            $.each(res.Data, function (index, value) {
                                items += "<option value='" + value.Value + "'>" + value.Description + "</option>";
                            });
                            $(op.selectParent).html(items);
                        }
                    }
                    else {
                        errorMessageShow(res.Message, op.mainMessageContext);
                    }
                }
            })
            .fail(res => {
                errorMessageShow(res.responseText, op.mainMessageContext);
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
                            errorMessageShow(res.Message, op.mainMessageContext); 
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
                        errorMessageShow(res.Message, op.mainMessageContext);
                    }
                }
            })
            .fail(res => {
                errorMessageShow(res.responseText, op.mainMessageContext);
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
                                errorMessageShow(res.Message, op.popupMessageContext); 
                                $(op.btnCreateType).attr("disabled", "disabled");
                            }                           
                        }
                        else {
                            errorMessageShow(res.Message, op.popupMessageContext);
                            $(op.btnCreateType).attr("disabled", "disabled");
                            $(op.btnCreateType).attr("disabled", "disabled");
                        }
                    }
                })
                .fail(res => {
                    alertService.showError(res.responseText, op.popupMessageContext); 
                });
        }
    }

    var errorMessageShow = function (messageList, messageContext){
        var msg = " ";
        $.each(messageList, function (index, value) {
            msg += value;
        });
        alertService.showError(msg, messageContext);  
    }
    var successMessageShow = function (messageList,messageContext) {
        var msg = " ";
        $.each(messageList, function (index, value) {
            msg += value;
        });
        alertService.showSuccess(msg, messageContext);  
    }

    var bindEvents = function () {
        var op = defaults;
        var $modelTypeContext = $(op.modelTypeContext);
        var $tableContext = $(op.tableContext);

        $modelTypeContext.on('click', op.btnCreateType, function (e) {
            createType();
        });

        $modelTypeContext.on('focusout', op.typeName, function (e) {
            validateType();
        });

        $modelTypeContext.on('click', op.btnClearType, function (e) {
            $(defaults.typeName).val("").removeAttr("readonly");           
            $(defaults.selectParent).val("");
            $(defaults.selectStatus).val("True");
            $(defaults.typeId).html("");
            $(defaults.typeValue).html("");
            $(defaults.btnUpdateType).hide();
            $(defaults.btnCreateType).show();
            alertService.hide(defaults.popupMessageContext);
        });

        $tableContext.on('click', op.btnEditType, function (e) {
            alertService.hide(op.popupMessageContext);
            alertService.hide(op.mainMessageContext);
            $(defaults.btnUpdateType).show();
            $(defaults.btnCreateType).hide();

            var $row = $(this).closest("tr");

            $(op.typeName).val($row.find($(op.selectedItemName)).html()).attr("readonly", "readonly");
            $(op.typeValue).html($row.find($(op.selectedItemValue)).html());
            $(op.selectParent).val($row.find($(op.selectedParentTypeId)).html());
            $(op.typeId).html($row.find($(op.selectedTypeId)).html());
            $(op.selectStatus).val($row.find($(op.selectedItemStatus)).html()); 
            $modelTypeContext.modal('show');
        });

        $tableContext.on('click', op.btnCreate, function (e) {
            alertService.hide(op.popupMessageContext);
            alertService.hide(op.mainMessageContext);
            $(op.btnClearType).trigger("click");

            $modelTypeContext.modal('show');
        });

        $modelTypeContext.on('click', op.btnUpdateType, function (e) {
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