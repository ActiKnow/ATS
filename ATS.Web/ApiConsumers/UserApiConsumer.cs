using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATS.Core.Model;


namespace ATS.Web.ApiConsumers
{
    public class UserApiConsumer
    {
        public static ApiResult ValidateUser(UserCredentialModel userCredential)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/User/Validate";
                apiResult = ConsumerMethods.Post<UserInfoModel>(url,userCredential);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

        public static ApiResult RegisterUser(UserInfoModel userInfoModel)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/User/Create";
                apiResult = ConsumerMethods.Post<UserInfoModel>(url, userInfoModel);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

       
    }
}