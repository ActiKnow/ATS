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
                result = ApiConsumers.CommonApi.GetParentTypes();
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
                result = ApiConsumers.UserApiConsumer.RegisterUser(userInfoModel);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
          
        }

    }
}