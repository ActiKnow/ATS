using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ATS.Core.Global;
using ATS.Core.Model;
using ATS.Web.Controllers;


namespace ATS.Web.Areas.Admin.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class TestAssignmentController : BaseController
    {
        // GET: Admin/TestAssignment
        public ActionResult Index()
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
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTests(bool? rawTests)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.TestBankApiConsumer.Select();
                if (result.Status && result.Data != null)
                {
                    var list = (List<TestBankModel>)result.Data;
                    if (rawTests == null || (rawTests != null && !rawTests.Value))
                    {
                        result.Data = RenderPartialViewToString("_TestList", list);
                    }
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}