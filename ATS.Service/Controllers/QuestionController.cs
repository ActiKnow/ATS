using ATS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATS.Repository.Model;
using ATS.Core.Model;
using ATS.Core.Helper;
using ATS.Bll;

namespace ATS.Service.Controllers
{
    public class QuestionController : ApiController
    {
        private ApiResult apiResult;
        private QuestionBankBo questionBankBo;
        public QuestionController()
        {
            apiResult = new ApiResult(false, new List<string>());
            questionBankBo = new QuestionBankBo();
        }

        [HttpPost]
        [Route("api/Question/Create")]
        public IHttpActionResult Create(QuestionBankModel newQues)
        {
            try
            {
                apiResult = questionBankBo.Create(newQues);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message);
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/Question/Update")]
        public IHttpActionResult Update(QuestionBankModel newQues)
        {
            try
            {
                apiResult = questionBankBo.Update(newQues);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message);
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/Question/Delete")]
        public IHttpActionResult Delete(QuestionBank newQues)
        {
            try
            {
                apiResult = questionBankBo.Delete(newQues);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message);
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/Question/Select")]
        public IHttpActionResult Select(SimpleQueryModel query)
        {
            try
            {
                apiResult = questionBankBo.Select(query);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message);
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }

        [HttpGet]
        [Route("api/Question/Retrieve/{questionId}")]
        public IHttpActionResult Retrieve(Guid questionId)
        {
            try
            {
                apiResult = questionBankBo.GetById(questionId);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message);
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }
    }
}
