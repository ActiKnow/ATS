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
using ATS.Core.Helper;

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

        [HttpPost]
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
        [Route("api/Question/Select")]
        public IHttpActionResult Select(SimpleQueryModel query)
        {
            ApiResult apiResult = new ApiResult(false, "Record not found");
            try
            {
                SimpleQueryBuilder<QuestionBankModel> simpleQry = new SimpleQueryBuilder<QuestionBankModel>();
                List<QuestionBankModel> ques  = questionRepo.Select(simpleQry.GetQuery(query).Compile());

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
