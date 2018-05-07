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
            ApiResult apiResult = new ApiResult(false,"Not Created");
            if (questionRepo.Create(newQues))
            {
                apiResult = new ApiResult(true);
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/Question/Update")]
        public IHttpActionResult Update(QuestionBankModel newQues)
        {
            ApiResult apiResult = new ApiResult(false, "Not Updated");
            if (questionRepo.Update(newQues))
            {
                apiResult = new ApiResult(true);
            }
            return Ok(apiResult);
        }

        [HttpDelete]
        [Route("api/Question/Delete")]
        public IHttpActionResult Delete(QuestionBankModel newQues)
        {
            ApiResult apiResult = new ApiResult(false, "Not Deleted");
            if (questionRepo.Delete(newQues))
            {
                apiResult = new ApiResult(true);
            }
            return Ok(apiResult);
        }
    }
}
