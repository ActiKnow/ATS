using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Core.Model;
using ATS.Web.Controllers;

namespace ATS.Web.Areas.Admin
{
    public class SetupController : BaseController
    {
        // GET: Admin/Setup
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TypeSetup()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Question()
        {

            return View();
        }

        public ActionResult CreateQuestion(QuestionBankModel QuestionView)
        {
            return View();
        }

        [ActionName("UserSetup")]
        public ActionResult UserCreation(QuestionBankModel QuestionView)
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateType(TypeDefModel typeDef)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.TypeApiConsumer.CreateType(typeDef);

                if(result.Status && result.Data != null)
                {
                    var list = (List<TypeDefModel>)result.Data;
                   // result.Data = RenderPartialViewToString(new { }, list);
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateType(TypeDefModel typeDef)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.TypeApiConsumer.UpdateType(typeDef);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteType(TypeDefModel typeDef)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.TypeApiConsumer.DeleteType(typeDef);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RetrieveType(TypeDefModel typeDef)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.TypeApiConsumer.RetrieveType(typeDef);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetParentTypes()
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.CommonApiConsumer.GetParentTypes();
            }
            catch(Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}