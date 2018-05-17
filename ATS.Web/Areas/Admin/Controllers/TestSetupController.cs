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
    public class TestSetupController : BaseController
    {
        // GET: Admin/TestSetup
        public ActionResult Index()
        {
            return View();
        }

        #region TestSetup
        public ActionResult TestSetup(Guid? testId)
        {
            ApiResult result = null;
            TestBankModel testModel = new TestBankModel { TestBankId = testId ?? Guid.Empty };
            try
            {

                result = ApiConsumers.TestBankApiConsumer.RetrieveTest(testModel);
                if (result != null && result.Status && result.Data != null)
                {
                    testModel = result.Data as TestBankModel;
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return View(testModel);
        }
        [HttpGet]
        public ActionResult GetTests()
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.TestBankApiConsumer.Select();
                if (result.Status && result.Data != null)
                {
                    var list = (List<TestBankModel>)result.Data;

                    result.Data = RenderPartialViewToString("_TestList", list);
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CreateTest(TestBankModel test)
        {
            ApiResult result = null;
            try
            {
                test.StatusId = true;
                test.CreatedBy = Convert.ToString(Session[Constants.USERID]);
                result = ApiConsumers.TestBankApiConsumer.CreateTest(test);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result);
        }
        [HttpPost]
        public ActionResult UpdateTest(TestBankModel test)
        {
            ApiResult result = null;
            try
            {
               
                test.LastUpdatedBy = Convert.ToString(Session[Constants.USERID]);
                test.LastUpdatedDate = DateTime.Now;
               result = ApiConsumers.TestBankApiConsumer.UpdateTest(test);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result);
        }
        [HttpPost]
        public ActionResult LinkTestQuestion(List<TestQuestionMapModel> linkQuestions)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.TestBankApiConsumer.TestQuestionsLink(linkQuestions);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result);
        }
        [HttpPost]
        public ActionResult UnlinkTestQuestion(List<TestQuestionMapModel> unlinkQuestions)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.TestBankApiConsumer.TestQuestionsUnlink(unlinkQuestions);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result);
        }
        public ActionResult MapTestQuestion()
        {
            return View();
        }
        #endregion
    }
}