using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Core.Global;
using ATS.Core.Model;

namespace ATS.Web.Areas.Admin.Controllers
{
    public class FeedbackController : Controller
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
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(List<Guid> Ids)
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.FeedbackApiConsumer.Delete(Ids);
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Count()
        {
            ApiResult result = null;
            try
            {
                result = ApiConsumers.FeedbackApiConsumer.Count();                
            }
            catch (Exception ex)
            {
                result = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}