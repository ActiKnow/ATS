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
            QuestionBankModel questionBankModel = new QuestionBankModel();

            return View(questionBankModel);
        }

        [HttpPost]
        public ActionResult Question(Guid selectedId)

        {
            ApiResult result = null;
            QuestionBankModel questionBankModel = new QuestionBankModel();
            SimpleQueryModel query = new SimpleQueryModel();
            query.ModelName = nameof(QuestionBankModel);
            query[nameof(QuestionBankModel.QId)] = selectedId;
            result = ApiConsumers.QuestionApiConsumer.Select(query);
            if (result.Status && result.Data != null)
            {
                var list = (List<QuestionBankModel>)result.Data;
                questionBankModel = list.FirstOrDefault();
            }
            return View(questionBankModel);
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
                result = new ApiResult(false, new List<string> {   ex.GetBaseException().Message  });
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
                typeDef.CreatedBy = Session[Constants.USERID].ToString();
                typeDef.IsEditable = true;
                result = ApiConsumers.TypeApiConsumer.CreateType(typeDef);

                if (result.Status && result.Data != null)
                {
                    var list = (List<TypeDefModel>)result.Data;

                    result.Data = RenderPartialViewToString("_TypeList", list);
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> {  ex.GetBaseException().Message  });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateType(TypeDefModel typeDef)
        {
            ApiResult result = null;
            try
            {
                typeDef.LastUpdatedBy =Convert.ToString(Session[Constants.USERID]);
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
                result = new ApiResult(false, new List<string> {  ex.GetBaseException().Message  });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult DeleteType(TypeDefModel typeDef)
        //{
        //    ApiResult result = null;
        //    try
        //    {
        //        result = ApiConsumers.TypeApiConsumer.DeleteType(typeDef);
        //    }
        //    catch (Exception ex)
        //    {
        //        result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult RetrieveType(TypeDefModel typeDef)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.TypeApiConsumer.RetrieveType(typeDef.Value);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetParentTypes()
        {
            ApiResult result = null;
            try
            {
                SimpleQueryModel query = new SimpleQueryModel();
                query.ModelName = nameof(TypeDefModel);
                query[nameof(TypeDefModel.ParentKey)] = 0;

                result = ApiConsumers.TypeApiConsumer.SelectTypes(query);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
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
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllTypes()
        {
            ApiResult result = null;
            try
            {
                //SimpleQueryModel query = new SimpleQueryModel();
                //query.ModelName = nameof(TypeDefModel);
                //query[nameof(TypeDefModel.ParentKey),QueryType.And,QueryType.NotEqual] =Constants.PARENT;
                //query[nameof(TypeDefModel.ParentKey), QueryType.And, QueryType.NotEqual] = CommonType.ROLE;
                //query[nameof(TypeDefModel.ParentKey), QueryType.And, QueryType.NotEqual] = CommonType.QUESTION;

                result = ApiConsumers.TypeApiConsumer.SelectTypes(null);

                if (result.Status && result.Data != null)
                {
                    var list = (List<TypeDefModel>)result.Data;

                    result.Data = RenderPartialViewToString("_TypeList", list);
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ValidateType(string typeName)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.TypeApiConsumer.ValidateType(typeName);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
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
                result = ApiConsumers.QuestionApiConsumer.Select();
                if (result.Status && result.Data != null)
                {
                    var list = (List<QuestionBankModel>)result.Data;

                    result.Data = RenderPartialViewToString("_QuestionList", list);
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
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
                    result1 = ApiConsumers.QuestionApiConsumer.Select();
                    if (result1.Status && result1.Data != null)
                    {
                        var list = (List<QuestionBankModel>)result1.Data;

                        result.Data = RenderPartialViewToString("_QuestionList", list);
                    }
                    else
                    {
                        result.Status = result1.Status;
                        result.Message = result1.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult EditQuestion(Guid qId)
        {

            ApiResult result = null;
            SimpleQueryModel query = new SimpleQueryModel();
            query.ModelName = nameof(QuestionBankModel);
            query[nameof(QuestionBankModel.QId)] = qId;
            result = ApiConsumers.QuestionApiConsumer.Select(query);
            if (result.Status && result.Data != null)
            {
                var list = (List<QuestionBankModel>)result.Data;

            }
            return RedirectToAction("Question", qId);
        }
        public ActionResult GetQuestionTypes()
        {
            ApiResult result = null;
            try
            {
                SimpleQueryModel query = new SimpleQueryModel();
                query.ModelName = nameof(TypeDefModel);
                query[nameof(TypeDefModel.ParentKey)] =CommonType.QUESTION;

                result = ApiConsumers.TypeApiConsumer.SelectTypes(query);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLevelTypes()
        {
            ApiResult result = null;
            try
            {
                SimpleQueryModel query = new SimpleQueryModel();
                query.ModelName = nameof(TypeDefModel);
                query[nameof(TypeDefModel.ParentKey)] = CommonType.LEVEL;

                result = ApiConsumers.TypeApiConsumer.SelectTypes(query);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCategoryTypes()
        {
            ApiResult result = null;
            try
            {
                SimpleQueryModel query = new SimpleQueryModel();
                query.ModelName = nameof(TypeDefModel);
                query[nameof(TypeDefModel.ParentKey)] = CommonType.CATEGORY;

                result = ApiConsumers.TypeApiConsumer.SelectTypes(query);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false,  new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllSubTypes(int parentTypeValue)
        {
            ApiResult result = null;
            try
            {
                SimpleQueryModel query = new SimpleQueryModel { ModelName = nameof(TypeDefModel) };
                query[nameof(TypeDefModel.ParentKey)] = parentTypeValue;
                result = ApiConsumers.TypeApiConsumer.SelectTypes(query);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
     
        #region AnswerSetup
        public ActionResult AnswerSetup()
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

        public ActionResult GetAnswerUsers(List<UserInfoModel> allUserIdList)
        {
            return View();
        }
        #endregion
    }
}