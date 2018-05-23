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
            TempData["ModelName"] = userList;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SelectMappedTest(Guid userId)
        {
            ApiResult result = null;
            try
            {
               result = ApiConsumers.TestBankApiConsumer.SelectMapped(userId);
                if (result.Status && result.Data != null)
                {
                    var list = (List<TestBankModel>)result.Data;
                    if (list!=null && list.Count>0)
                    {
                       // result.Data = RenderPartialViewToString("_TestList", list);
                    }
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SelectUnmappedTest(Guid userId)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.TestBankApiConsumer.SelectUnMapped(userId);
                if (result.Status && result.Data != null)
                {
                    var list = (List<TestBankModel>)result.Data;
                    if (list != null && list.Count > 0)
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

        [HttpPost]
        public ActionResult AssignTest(List<TestAssignmentModel> testAssignmentModel)
        {
            ApiResult result = null;
            try
            {
                foreach (var index in testAssignmentModel)
                {
                index.StatusId = true;
                index.CreatedBy = Convert.ToString(Session[Constants.USERID]);
                }
                result = ApiConsumers.TestBankApiConsumer.AssignTest(testAssignmentModel);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result);
        }
    }
    
}