using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Core.Model;

namespace ATS.Web.Areas.Admin
{
    public class UserSetupController : Controller
    {
        // GET: Admin/UserSetup
        public ActionResult Index()
        {
            return View();
        }
        [ActionName("UserSetup")]
        public ActionResult UserSetup()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetRoleTypes()
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.CommonApiConsumer.SelectTypes(true);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CreateUser(UserInfoModel userInfoModel)
        {
            ApiResult result = null;
            try
            {
                userInfoModel.CreatedDate = DateTime.Now;
                userInfoModel.CreatedBy = Session[Constants.USERID].ToString();
                userInfoModel.UserCredentials[0].CreatedDate= DateTime.Now;
                userInfoModel.UserCredentials[0].CreatedBy = Session[Constants.USERID].ToString();

                result = ApiConsumers.UserApiConsumer.RegisterUser(userInfoModel);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
          
        }

        [HttpGet]
        public ActionResult CreateRandomPassword(int PasswordLength)
        {
            ApiResult result = null;
            try
            {
                string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
                Random randNum = new Random();
                char[] chars = new char[PasswordLength];
                int allowedCharCount = _allowedChars.Length;
                for (int i = 0; i < PasswordLength; i++)
                {
                    chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
                }
                string pwd = new string(chars);
                result = new ApiResult(true, "", pwd);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}