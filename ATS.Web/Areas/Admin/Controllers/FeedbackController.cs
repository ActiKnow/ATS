using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Core.Global;
using ATS.Core.Model;
using ATS.Web.Controllers;

namespace ATS.Web.Areas.Admin.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class FeedbackController : BaseController
    {
        // GET: Admin/Feedback
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Retrieve(Guid id)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.FeedbackApiConsumer.Retrieve(id);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Select()
        {
            ApiResult result = null;
            try
            {
                SimpleQueryModel query = new SimpleQueryModel();
                query.ModelName = nameof(UserFeedbackModel);
                query[nameof(UserFeedbackModel.StatusId), QueryType.And, QueryType.NotEqual] = CommonType.DELETED;

                result = ApiConsumers.FeedbackApiConsumer.Select(query);

                if (result != null)
                {
                    if(result.Status && result.Data != null)
                    {
                        result.Data = RenderPartialViewToString("_feedbacks", result.Data);
                    }
                }
                else
                {
                    result = new ApiResult(false, new List<string> { "No record found." });
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(List<Guid> Ids)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.FeedbackApiConsumer.Delete(Ids);
                if (result != null)
                {
                    if (result.Status && result.Data != null)
                    {
                        result.Data = RenderPartialViewToString("_feedbacks", result.Data);
                    }
                }
                else
                {
                    result = new ApiResult(false, new List<string> { "No record found." });
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result);
        }

        [HttpGet]
        public ActionResult InboxCount()
        {
            ApiResult result = null;
            try
            {
                SimpleQueryModel query = new SimpleQueryModel();
                query.ModelName = nameof(UserFeedbackModel);
                query[nameof(UserFeedbackModel.ReadStatus)]=false;

                result = ApiConsumers.FeedbackApiConsumer.Count(query);                
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TotalCount()
        {
            ApiResult result = null;
            try
            {
                SimpleQueryModel query = new SimpleQueryModel();
                query.ModelName = nameof(UserFeedbackModel);
                query[nameof(UserFeedbackModel.StatusId), QueryType.And, QueryType.NotEqual] = CommonType.DELETED;

                result = ApiConsumers.FeedbackApiConsumer.Count(query);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}