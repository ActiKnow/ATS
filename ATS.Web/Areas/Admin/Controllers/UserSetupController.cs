using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Core.Global;
using ATS.Core.Model;
using ATS.Web.Controllers;

namespace ATS.Web.Areas.Admin.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class UserSetupController : BaseController
    {
        // GET: Admin/UserSetup
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserSetup()
        {
            UserInfoModel userInfo;            
            userInfo = new UserInfoModel();
            ViewBag.userSetupRoleType = RoleTypeList();

            return View(userInfo);
        }

        [HttpPost]
        public ActionResult UserSetup(Guid userID)
        {
            UserInfoModel userInfo = new UserInfoModel();
            userInfo.UserId = userID;           
            ApiResult result = null;
            try
            {
                result = ApiConsumers.UserApiConsumer.RetrieveUser(userInfo);

                if (result.Status && result.Data != null)
                {
                    userInfo = (UserInfoModel)result.Data;
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
            }

            return View(userInfo);
        }

        [HttpGet]
        public ActionResult GetRoleTypes()
        {
            ApiResult result = null;
            try
            {
                SimpleQueryModel query = new SimpleQueryModel();
                query.ModelName = nameof(TypeDefModel);
                query[nameof(TypeDefModel.ParentKey)] = CommonType.ROLE;

                result = ApiConsumers.TypeApiConsumer.SelectTypes(query);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
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
                userInfoModel.UserCredentials.CreatedDate = DateTime.Now;
                userInfoModel.UserCredentials.CreatedBy = Session[Constants.USERID].ToString();

                result = ApiConsumers.UserApiConsumer.RegisterUser(userInfoModel);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
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
                result = new ApiResult(true, null, pwd);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [ActionName("UserApproval")]
        public ActionResult UserApproval()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            List<UserInfoModel> userList = new List<UserInfoModel>();
            ApiResult result = null;

            try
            {
                result = ApiConsumers.UserApiConsumer.SelectUsers();

                if (result.Status && result.Data != null)
                {
                    userList = (List<UserInfoModel>)result.Data;

                    result.Data = RenderPartialViewToString("_UsersList", userList);
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DisableUser(UserInfoModel userInfoModel)
        {
            List<UserInfoModel> userList = new List<UserInfoModel>();
            ApiResult result = null;
            try
            {

               result = ApiConsumers.UserApiConsumer.DisableUser(userInfoModel);

                if (result.Status)
                {
                    //result = ApiConsumers.UserApiConsumer.SelectUsers();

                    if (result.Status && result.Data != null)
                    {
                        userList = (List<UserInfoModel>)result.Data;

                        result.Data = RenderPartialViewToString("_UsersList", userList);
                    }
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateUser(UserInfoModel userInfoModel)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.UserApiConsumer.UpdateUser(userInfoModel);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private SelectList RoleTypeList()
        {
            List<ATS.Core.Model.TypeDefModel> roleTypeDef = null;
            ApiResult result = null;
            SelectList selectList = null;
            try
            {
                SimpleQueryModel query = new SimpleQueryModel();
                query.ModelName = nameof(TypeDefModel);
                query[nameof(TypeDefModel.ParentKey)] = CommonType.ROLE;

                result = ApiConsumers.TypeApiConsumer.SelectTypes(query);

                if (result != null)
                {
                    if (result.Status && result.Data != null)
                    {
                        roleTypeDef = (List<ATS.Core.Model.TypeDefModel>)result.Data;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message.Add(ex.GetBaseException().Message);
            }

            if (roleTypeDef == null)
            {
                roleTypeDef = new List<ATS.Core.Model.TypeDefModel>(1);
            }

            return selectList = new SelectList(roleTypeDef, "TypeId", "Description");
        }

        [HttpGet]
        public ActionResult GetStatus()
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.CommonApiConsumer.GetStatus();
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}