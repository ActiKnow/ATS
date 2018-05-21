using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ATS.Core.Global;
using ATS.Core.Helper;
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

        //[HttpPost]
        //public ActionResult GetResultUsers(List<Guid> userId)
        //{
        //    List<TestBankModel> testBankList = new List<TestBankModel>();
        //    ApiResult result = null;

        //    try
        //    {
        //        result = ApiConsumers.ResultApiConsumer.RetrieveResult(userId);

        //        if (result.Status && result.Data != null)
        //        {
        //            testBankList = (List<TestBankModel>)result.Data;

        //            result.Data = RenderPartialViewToString("", testBankList);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);

        //}
        [HttpPost]
        public ActionResult GetConsolidatedTestResults(List<Guid> allUserIdList)
        {
            List<TestBankModel> testBankList = new List<TestBankModel>();
            ApiResult result = null;

            try
            {
                result = ApiConsumers.ResultApiConsumer.RetrieveResult(allUserIdList);
                result.Data = BindChartData(result);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetIndividualTestResults(List<Guid> allUserIdList)
        {
            List<TestBankModel> testBankList = new List<TestBankModel>();
            ApiResult result = null;

            try
            {
                result = ApiConsumers.ResultApiConsumer.RetrieveResult(allUserIdList);
                result.Data = BindIndividualChartData(result);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private List<LineChart> BindChartData(ApiResult result)
        {
            var data = (List<TestAssignmentModel>)result.Data;

            List<LineChart> charts = new List<LineChart>();
            List<DataPoints> tempDataPoints = new List<DataPoints>();
            
            var groupTestList = data.GroupBy(x => x.TestBankId).ToList();
            var userList = data.GroupBy(x => x.UserId).ToList();

            foreach (var group in groupTestList)
            {
                var test = group.FirstOrDefault();
                tempDataPoints.Add(new DataPoints { y = 0, label = test.TestBankName });
            }

            foreach (var group in userList)
            {
                var userInfo = group.FirstOrDefault().UserInfo;

                var list = tempDataPoints.ToList();
                LineChart user = new LineChart()
                {
                    type = "column",
                    showInLegend = true,
                    legendText = userInfo.FName + " " + userInfo.LName,
                };
             
                foreach (var dPoints in tempDataPoints)
                {
                    user.dataPoints.Add(new DataPoints { y = dPoints.y, label = dPoints.label });
                }
                foreach (var item in group)
                {
                   var point= user.dataPoints.Where(x => x.label == item.TestBankName).FirstOrDefault();
                    if (point != null)
                    {
                        point.y = item.MarksObtained;
                    }
                }
                charts.Add(user);
            }
            return charts;
        }

        private List<LineChart> BindIndividualChartData(ApiResult result)
        {
            var data = (List<TestAssignmentModel>)result.Data;

            List<LineChart> charts = new List<LineChart>();
            LineChart chart = new LineChart();
            List<DataPoints> dataPoints = null;
            DataPoints points;

            var userList = data.GroupBy(x => x.UserId);

            foreach (var group in userList)
            {

                var key = group.Key;
                var userInfo = group.FirstOrDefault().UserInfo;
                var user = new LineChart()
                {
                    type = "column",
                    showInLegend = true,
                    legendText = userInfo.FName + " " + userInfo.LName,
                };
                dataPoints = new List<DataPoints>();
                foreach (var item in group)
                {
                    points = new DataPoints();
                    points.y = item.MarksObtained;
                    points.label = item.TestBankName;
                    dataPoints.Add(points);
                }
                user.dataPoints = dataPoints;
                charts.Add(user);
            }

            return charts;
        }
    }
}