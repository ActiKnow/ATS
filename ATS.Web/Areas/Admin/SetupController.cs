﻿using System;
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

namespace ATS.Web.Areas.Admin
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
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

            ApiResult result = null;
            try
            {
                result = ApiConsumers.QuestionApiConsumer.CreateQuestion(QuestionView);

            }
            catch (Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
                    
                    result.Data = RenderPartialViewToString("_TypeList", list);
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
                typeDef.LastUpdatedBy = Session[Constants.USERID].ToString();
                typeDef.LastUpdatedDate = DateTime.Now;

                result = ApiConsumers.TypeApiConsumer.UpdateType(typeDef);
                if (result.Status && result.Data != null)
                {
                    var list = (List<TypeDefModel>)result.Data;

                    result.Data = RenderPartialViewToString("_TypeList", list);
                }
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
                //result = ApiConsumers.TypeApiConsumer.RetrieveType(typeDef);
                SimpleQueryModel query = new SimpleQueryModel();
                query.ModelName = nameof(TypeDefModel);
                query[nameof(TypeDefModel.TypeId)] = typeDef.TypeId;

                result = ApiConsumers.TypeApiConsumer.SelectType(query);
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
                result = ApiConsumers.CommonApiConsumer.SelectTypes(true);
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

        [HttpGet]
        public ActionResult GetAllTypes()
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.CommonApiConsumer.SelectTypes(false);
                if (result.Status && result.Data != null)
                {
                    var list = (List<TypeDefModel>)result.Data;

                    result.Data = RenderPartialViewToString("_TypeList", list);
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ValidateType(string typeName, int typeValue)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.TypeApiConsumer.ValidateType(typeName, typeValue);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


       public ActionResult QuestionList()
        {
            List<QuestionBankModel> quesList = new List<QuestionBankModel>();
            return View(quesList);
        }

        public ActionResult GetQuestionList()
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.QuestionApiConsumer.SelectList();
                if (result.Status && result.Data != null)
                {
                    var list = (List<QuestionBankModel>)result.Data;

                    result.Data = RenderPartialViewToString("_QuestionList", list);
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteQuestion(Guid qId)
        {
            QuestionBankModel questionBankModel = new QuestionBankModel();
            questionBankModel.QId = qId;
            ApiResult result = null;
            ApiResult result1 = null;
            try
            {
                result = ApiConsumers.QuestionApiConsumer.DeleteQuestion(questionBankModel);
                if (result.Status)
                {
                    result1 = ApiConsumers.QuestionApiConsumer.SelectList();
                    if (result1.Status && result1.Data != null)
                    {
                        var list = (List<QuestionBankModel>)result1.Data;

                        result1.Data = RenderPartialViewToString("_QuestionList", list);
                    }
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Json(result1, JsonRequestBehavior.AllowGet);

        }
        public ActionResult EditQuestion()
        {
            return View();
        }

        public ActionResult GetQuestionTypes()
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
        public ActionResult GetLabelTypes()
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
        public ActionResult GetCategoryTypes()
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


    }
}