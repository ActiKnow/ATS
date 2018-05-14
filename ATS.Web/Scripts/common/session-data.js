var sessionData = (function () {
    var defaults = {
        companyID: '#enterprise_company_id',
        companyName:'#enterprise_company_name'
    };
    var getSelectedCompanyDetail = (function () {
        var $companyID = $(defaults.companyID);
        var $companyName = $(defaults.companyName);
        var companyID = '', companyName = '';
        if ($companyID) {
            companyID = $companyID.val();
        }
        if ($companyName) {
            companyName = $companyName.val();
        }
        return {
            id: companyID,
            name: companyName
        };
    })();

    var init = function (config) {
        $.extend(true, defaults, config);

    }
    return {
        company: getSelectedCompanyDetail
    };

})();