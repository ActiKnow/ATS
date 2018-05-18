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
    public class ResultSetupController : BaseController
    {
        // GET: Admin/ResultSetup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Result()
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

                    result.Data = RenderPartialViewToString("_AllUsersList", userList);
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetConsolidatedTestResults(List<Guid> allUserIdList)
        {            
            List<TestBankModel> testBankList = new List<TestBankModel>();
            ApiResult result = null;

            try
            {
                result = ApiConsumers.ResultApiConsumer.RetrieveResult(allUserIdList);

                if (result.Status && result.Data != null)
                {
                    testBankList = (List<TestBankModel>)result.Data;

                    result.Data = RenderPartialViewToString("", testBankList);
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetIndividualTestResults(List<UserInfoModel> allUserIdList)
        {
            return Json("");

        }
    }
}