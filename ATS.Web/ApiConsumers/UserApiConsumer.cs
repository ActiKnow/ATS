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
        public static ApiResult RetrieveUser(UserInfoModel userInfoModel)   // Getting UserInfo by using ID
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/User/Retrieve";
                apiResult = ConsumerMethods.Post<UserInfoModel>(url, userInfoModel);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

        public static ApiResult SelectUsers()   // Getting Users List by using query
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/User/Select";
                apiResult = ConsumerMethods.Get<List<UserInfoModel>>(url);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult DisableUser(UserInfoModel userInfoModel)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/User/Disable";
                apiResult = ConsumerMethods.Post<List<UserInfoModel>>(url, userInfoModel);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult UpdateUser(UserInfoModel userInfoModel)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/User/Update";
                apiResult = ConsumerMethods.Post<List<UserInfoModel>>(url, userInfoModel);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
    }
}