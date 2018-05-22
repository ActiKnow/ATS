using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATS.Bll;
using ATS.Core.Model;
using ATS.Repository.Model;

namespace ATS.Service.Controllers
{
    public class FeedbackController : ApiController
    {
        private FeedbackBo feedbackBo = null;
        public FeedbackController()
        {
            feedbackBo = new FeedbackBo();
        }

        [HttpPost]
        [Route("api/Feedback/Create")]
        public IHttpActionResult Create(UserFeedback input)
        {
            ApiResult apiResult = null;
            try
            {
                apiResult = feedbackBo.Create(input);
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/Feedback/Delete")]
        public IHttpActionResult Delete(List<Guid> Ids)
        {
            ApiResult apiResult = null;
            try
            {
                apiResult = feedbackBo.Delete(Ids);
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Ok(apiResult);
        }
                
        [HttpPost]
        [Route("api/Feedback/Count")]
        public IHttpActionResult Count(SimpleQueryModel qry)
        {
            ApiResult apiResult = null;
            try
            {
                apiResult = feedbackBo.Count(qry);
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Ok(apiResult);
        }

        [HttpGet]
        [Route("api/Feedback/Retrieve/{Id}")]
        public IHttpActionResult Retrieve(Guid Id)
        {
            ApiResult apiResult = null;
            try
            {
                apiResult = feedbackBo.GetById(Id);
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/Feedback/Select")]
        public IHttpActionResult Select(SimpleQueryModel simpleQueryModel)
        {
            ApiResult apiResult = null;
            try
            {
                apiResult = feedbackBo.Select(simpleQueryModel);
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Ok(apiResult);
        }
    }
}
