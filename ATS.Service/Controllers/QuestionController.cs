using ATS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATS.Repository.DAO;
using ATS.Repository.Model;
using ATS.Core.Model;

namespace ATS.Service.Controllers
{
    public class QuestionController : ApiController
    {
        private IQuestionRepository questionRepo = null;
        public QuestionController()
        {
            questionRepo = new QuestionRepository();
        }

        [HttpPost]
        [Route("api/Question/Create")]
        public IHttpActionResult Create(QuestionBankModel newQues)
        {
            ApiResult apiResult = new ApiResult(false, "Not Created");
            try
            {
                if (questionRepo.Create(newQues))
                {
                    apiResult = new ApiResult(true, "Record Created");
                }
            }
            catch (Exception ex)
            {
                string error = ex.GetBaseException().Message;
                apiResult = new ApiResult(false, error);
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/Question/Update")]
        public IHttpActionResult Update(QuestionBankModel newQues)
        {
            ApiResult apiResult = new ApiResult(false, "Not Updated");
            try
            {
                if (questionRepo.Update(newQues))
                {
                    apiResult = new ApiResult(true);
                }
            }
            catch (Exception ex)
            {
                string error = ex.GetBaseException().Message;
                apiResult = new ApiResult(false, error);
            }
            return Ok(apiResult);
        }

        [HttpDelete]
        [Route("api/Question/Delete")]
        public IHttpActionResult Delete(QuestionBankModel newQues)
        {
            ApiResult apiResult = new ApiResult(false, "Not Deleted");
            try
            {
                if (questionRepo.Delete(newQues))
                {
                    apiResult = new ApiResult(true);
                }
            }
            catch (Exception ex)
            {
                string error = ex.GetBaseException().Message;
                apiResult = new ApiResult(false, error);
            }
            return Ok(apiResult);
        }

        [HttpGet]
        [Route("api/Question/Select/{questionId?}")]
        public IHttpActionResult Select(Guid? questionId = null)
        {
            ApiResult apiResult = new ApiResult(false, "Record not found");
            try
            {
                List<QuestionBankModel> ques = null;
                if (questionId != null)
                {
                    ques = questionRepo.Select(x => x.QId == questionId);
                }
                else
                {
                    ques = questionRepo.Select(null);
                }
                if (ques != null && ques.Count > 0)
                {
                    apiResult = new ApiResult(true, "", ques);
                }
            }
            catch (Exception ex)
            {
                string error = ex.GetBaseException().Message;
                apiResult = new ApiResult(false, error);
            }
           return Ok(apiResult);
        }
    }
}
