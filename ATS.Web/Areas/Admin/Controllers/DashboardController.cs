using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Core.Model;
using ATS.Web.ApiConsumers;
using ATS.Web.Controllers;

namespace ATS.Web.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CountUsers()
        {
            ApiResult apiResult = null;
            try
            {
                apiResult = UserApiConsumer.CountUsers();
            }
            catch(Exception ex)
            {
                apiResult = new ApiResult();
                apiResult.Status = false;
                apiResult.Message.Add(ex.GetBaseException().Message);
            }
            return Json(apiResult,JsonRequestBehavior.AllowGet);
        }
    }
}