//function Validate() {
//	var formValidationObject = {
//		validationContext: '.validate-form',
//		messageContext: '#message-context',
//		validateAtOnce: false,
//		validateWithFormat: true,
//      specialFormatCheck: { validateGSTWithPAN: "" }
//		callBackFuncAfterRequiredValidation:function(){ return true; }
//	}
//	var validationStatus = validationService.validateForm(formValidationObject) 

//	if (!validationStatus) 
//		event.preventDefault();
//	
//	return validationStatus;
//}

var validationService = (function () {
    var defaults = {
        validationContext: '.validate-form',  // parent form under which all required fields are there
        messageContext: '#message-context',  // validation message element
        validatorButton: '.btn-validator',  // btn on which the validation will be provoked
        elementRequired: 'data-required',
        validateAllAtOnce: false,              // true: need to validate and highlight all required fields at once
        validateAllAtOnceMessage: "Field/s marked in red is/are required",             // Message to show when validateAllAtOnce: true
        errorFieldClass: "has-error",
        elementformatAttr: 'data-type',
        invalidFormatErrorMessage: "Field/s marked in red is/are in invalid format",
        validateWithFormat: true,
        specialFormatCheck: { validateGSTWithPAN: "", validateGSTWithState: "" }
    };

    var validateFormats = {
        number: { regExp: /\d/, isNumber: true, checkRegExpOnBlur: false },
        pin: { regExp: /^[1-9][0-9]{5}$/, isNumber: true, checkRegExpOnBlur: false },
        mobile: { regExp: /^[1-9][0-9]{9}$/, isNumber: true, checkRegExpOnBlur: true },
        multipleMobileNumbers: { regExp: /^\d{10}(,\d{10})*$/, isNumber: false, checkRegExpOnBlur: true },
        email: { regExp: /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/, isNumber: false, checkRegExpOnBlur: true },
        pan: { regExp: /[a-zA-z]{5}\d{4}[a-zA-Z]{1}/, isNumber: false, checkRegExpOnBlur: true },
        gst: { regExp: /^([0-9]){2}([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}([0-9]){1}([a-zA-Z]){1}([0-9]){1}?$/, isNumber: false, checkRegExpOnBlur: true },
        maxTwoDigit: { regExp: /^[1-9][0-9]{2}$/, isNumber: true, checkRegExpOnBlur: false },
        date: { regExp: /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/, isNumber: false, checkRegExpOnBlur: true }


    }

    var validationContext = defaults.validationContext;
    var $validationContext = $(validationContext);

    var messageContext = defaults.messageContext;
    var $messageContext = $(messageContext);

    var validatorButton = defaults.validatorButton;
    var $validatorButton = $(validatorButton);

    var errorFieldClass = defaults.errorFieldClass;
    var elementRequired = defaults.elementRequired;
    var elementFormatAttr = defaults.elementformatAttr;
    var validateAtOnce = defaults.validateAllAtOnce;
    var validateWithFormat = defaults.validateWithFormat;
    var validateAllAtOnceMessage = defaults.validateAllAtOnceMessage;
    var invalidFormatErrorMessage = defaults.invalidFormatErrorMessage;

    var configure = function (config) {
        $.extend(true, defaults, config);
        init();
    };

    var init = function () {
        validationContext = defaults.validationContext;
        $validationContext = $(validationContext);

        messageContext = defaults.messageContext;
        $messageContext = $(messageContext);

        validatorButton = defaults.validatorButton;
        $validatorButton = $(validatorButton);

        elementRequired = defaults.elementRequired;
        validateAtOnce = defaults.validateAllAtOnce;
        validateAllAtOnceMessage = defaults.validateAllAtOnceMessage;
    };

    var temporaryDefaultConfiguration = function (obj) {
        var tempDefaults = {
            messageContext: defaults.messageContext,
            validationContext: defaults.validationContext,
            validateAtOnce: defaults.validateAllAtOnce,
            validateWithFormat: defaults.validateWithFormat,
            callBackFuncAfterRequiredValidation: function () {
                return true;
            },
            specialFormatCheck: defaults.specialFormatCheck
        };

        $.extend(true, tempDefaults, obj);

        return tempDefaults
    }

    //Stop entering alphabets on key press
    $.each(validateFormats, function (index, value) {
        var type = index;
        var ele = $("[" + elementFormatAttr + "=" + type + "]");
        var formatTypeRegExp = validateFormats[type].regExp;
        if (value.isNumber) {
            ele.on("keypress", function (e) {
                return validateFormats.number.regExp.test(e.key);
            });
        }
        if (value.checkRegExpOnBlur) {
            ele.attr('data-animation', 'false');
            ele.on("blur", function (e) {
                let ele = $(this);
                let eleData = ele.val();
             
                if (eleData.length) {
                    if (!formatTypeRegExp.test(eleData.trim())) {
                        let msg = "Invalid " + type + " format!!";
                        console.log(msg);
                        ele.addClass(errorFieldClass);

                        ele.addClass('red-tooltip');
                        ele.attr('data-original-title', msg);
                      
                        ele.tooltip();
                        //ele.attr('data-tt', "tooltip");
                    }
                    else {
                        ele.removeClass(errorFieldClass);
                        ele.removeAttr('data-original-title');
                        //ele.removeClass('red-tooltip');
                        //ele.removeAttr('data-tt');
                      
                    }
                } else {
                    ele.removeClass(errorFieldClass);
                    ele.removeAttr('data-original-title');
                    //ele.removeClass('red-tooltip');
                    //ele.removeAttr('data-tt');
                    
                }
            });
        }
    });

    //Format validation function
    var validateFieldFormat = function (obj) {
        var tempDefaults = temporaryDefaultConfiguration(obj);
        var validationContext = tempDefaults.validationContext;
        var messageContext = tempDefaults.messageContext;
        var validateWithFormat = tempDefaults.validateWithFormat;
        var formatStatus = true;
        var specialFormatCheck = tempDefaults.specialFormatCheck;

        if (validateWithFormat) {
            $validationContext = $(validationContext);
            var allFormatElementList = $validationContext.find("[" + elementFormatAttr + "]")
            var invalidFormatFocusElement = [];

            allFormatElementList.each(function () {
                var ele = $(this);
                var eleValue = ele.val();
                if (eleValue != null && eleValue.trim() != "") {
                    var formatType = ele.attr(elementFormatAttr).toLowerCase();
                    var formatTypeRegExp = validateFormats[formatType].regExp;
                    if (!formatTypeRegExp.test(eleValue.trim())) {
                        console.log("Invalid " + formatType + " format!!");
                        ele.addClass(errorFieldClass);
                        formatStatus = false;

                        if (!invalidFormatFocusElement.length)
                            invalidFormatFocusElement.push(ele);
                    }
                    else if (formatType == "gst" && specialFormatCheck.validateGSTWithPAN.length) {
                        var panElement = $(specialFormatCheck.validateGSTWithPAN);
                        var panValue = panElement.val().toUpperCase();
                        if (panValue.trim().length) {
                            if (eleValue.length) {
                                var gst_pan = eleValue.substr(2, 10).toUpperCase();
                                if (panValue != gst_pan) {
                                    if (!invalidFormatFocusElement.length)
                                        invalidFormatFocusElement.push(ele);

                                    ele.addClass(errorFieldClass);
                                    formatStatus = false;
                                }
                            }
                        }
                    }
                }
            });

            if (!formatStatus) {
                alertService.showError(invalidFormatErrorMessage, messageContext);
                if (invalidFormatFocusElement.length) {
                    var invalidFormatfirstElement = invalidFormatFocusElement[0];
                    invalidFormatfirstElement.focus();
                }
            }
            return formatStatus;
        }
        else {
            return formatStatus;
        }
    }

    //Main form validate function
    var formValidation = function (validationContext, messageContext, validateAtOnce, validateWithFormat, callBackFuncAfterRequiredValidation, specialFormatCheck) {
        try {
            var isAllfieldValidated = true;
            $validationContext = $(validationContext);
            var allRequiredElements = $validationContext.find("[" + elementRequired + "]");
            var errorMessage = "";
            var allInvalidElement = [];

            $validationContext.find("*").removeClass(errorFieldClass);
            alertService.hide(messageContext);

            if (validateAtOnce)
                errorMessage = validateAllAtOnceMessage;

            allRequiredElements.each(function () {
                var ele = $(this);
                if (ele.val() == null || ele.val().trim() == "") {
                    isAllfieldValidated = false;
                    ele.addClass(errorFieldClass);

                    if (!validateAtOnce) {
                        ele.focus();
                        errorMessage = ele.attr(elementRequired);
                        return false;
                    }

                    if (validateAtOnce && !allInvalidElement.length) {
                        allInvalidElement.push(ele);
                    }
                }
                //else {
                //	ele.removeClass(errorFieldClass);
                //	alertService.hide(messageContext);
                //}
            });

            if (validateAtOnce && allInvalidElement.length) {
                var firstInvalidElement = allInvalidElement[0];
                firstInvalidElement.focus();
            }

            if (!isAllfieldValidated)
                alertService.showError(errorMessage, messageContext);
            else {
                if (callBackFuncAfterRequiredValidation())
                    isAllfieldValidated = validateFieldFormat({ validationContext: validationContext, messageContext: messageContext, validateWithFormat: validateWithFormat, specialFormatCheck: specialFormatCheck })
                else
                    isAllfieldValidated = false;
            }

            return isAllfieldValidated;
        } catch (e) {
            return false;
        }
    };

    var validateForm = function (obj) {
        var tempDefaults = temporaryDefaultConfiguration(obj);
        validationResult = formValidation(tempDefaults.validationContext, tempDefaults.messageContext, tempDefaults.validateAtOnce, tempDefaults.validateWithFormat, tempDefaults.callBackFuncAfterRequiredValidation, tempDefaults.specialFormatCheck);

        return validationResult;
    }

    //Remove validation with error message
    var removeValidationError = function (obj) {
        var tempDefaults = temporaryDefaultConfiguration(obj);
        $validationContext = $(tempDefaults.validationContext);
        $validationContext.find("*").removeClass(errorFieldClass);
        alertService.hide(tempDefaults.messageContext);
    }

    //	-----------------------USAGE-----------------------
    //var validationResult = validateSelection($("#ENTERPRISE_COMPANY"), $("#BUSINESS_LINE"), $("#countryList"))
    //	if (validationResult.isValid) {
    //		// Do your stuff
    //	} else {
    //		alertService.showError(validationResult.errors.join(''));
    //	}
    function validateSelection() {
        var elements = arguments;
        var validate = {
            errors: [],
            isValid: true
        };

        if (elements && elements.length) {
            $(elements).each((index, obj) => {
                var o = $(obj);
                if (!o.val() && !o.val().trim().length) {
                    if (o.attr("data-val-msg")) {
                        validate.errors.push(o.attr("data-val-msg"));
                    }
                    o.addClass('has-error');
                    return false;
                } else {
                    o.removeClass('has-error');
                }
            });
        }
        validate.isValid = !validate.errors.length;

        return validate;
    }

    $validatorButton.on('click', validateForm);

    return {
        configure: configure,
        validateForm: validateForm,
        removeValidationError: removeValidationError,
        validateFieldFormat: validateFieldFormat
    }
})();
