var testBankSetup = (function () {
    'use strict';
    var defaults = {};
    const parentType = AppConstant.PARENT;
    var apiUrl = {
        getTestType: "/Setup/GetAllSubTypes/" ,
        getLevelType: "/Setup/GetAllSubTypes/" ,
        getCategoryType: "/Setup/GetAllSubTypes/"
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
    var action = {
        getTestType: function () {
            api.fireGetAjax(apiUrl.getTestType, { parentTypeValue: parentType})
                .done((result) => {
                    if (result && result.Status) {
                        if (result.Message && result.Message.length > 0) {
                        }
                        else {
                            render.fillSelector($(defaults.selectTestType), result.Data, "--select--", 'Value', 'Description');
                        }
                    }
                    else {

                    }
                })
                .fail();
        },
        getLevelType: function () { },
        getCategoryType: function () { },

    };
    var render = {
        fillSelector: function ($selector, data, defaultOption, value = 'Value', text = 'Text') {
            let options = "";
            if (defaultOption) {
                options = '<option>' + defaultOption + '</option>';
            }
            $.each(data, (indx, value)=>{
                options += '<option value="' + data[indx][value] + '">' + data[indx][text] + '</option>';
            });
            if ($selector) {
                $selector.html(options);
            }
        },
    };
    var loader = {
        loadSelector: function () {
            action.getTestType();
            action.getLevelType();
            action.getCategoryType();
        },

    };
    var setup = function () {
        for (let indx in loader) {
            if (typeof loader[indx] == 'function') {
                loader[indx]();
            }
        }
    };
    var init = function (settings) {

        $.extend(true, defaults, settings);
        setup();
    };
    return { init: init };
})();