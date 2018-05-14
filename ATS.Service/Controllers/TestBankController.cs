using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATS.Repository.Interface;
using ATS.Core.Model;
using ATS.Core.Helper;
using ATS.Bll;
using ATS.Repository.Model;

namespace ATS.Service.Controllers
{
    public class TestBankController : ApiController
    {       
        private TestBankBo testBankBo;
        private TestQuestionMapBo testQuestionMapBo;
        ApiResult apiResult;
        public TestBankController()
        {
            testBankBo = new TestBankBo();
            testQuestionMapBo = new TestQuestionMapBo();
            apiResult = new ApiResult(false, new List<string>());
        }

        [HttpPost]
        [Route("api/TestBank/Create")]
        public IHttpActionResult Create(TestBank testBank)
        {
            try
            {
                apiResult = testBankBo.Create(testBank);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message)  ;
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/TestBank/Update")]
        public IHttpActionResult Update(TestBank testBank)
        {
            try
            {
                apiResult = testBankBo.Update(testBank);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message)  ;
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }

        [HttpDelete]
        [Route("api/TestBank/Delete")]
        public IHttpActionResult Delete(TestBank testBank)
        {
            try
            {
                apiResult = testBankBo.Delete(testBank);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message)  ;
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/TestBank/Retrieve")]
        public IHttpActionResult Retrieve(TestBank testBank)
        {
            try
            {
                apiResult = testBankBo.GetById(testBank.TestBankId);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message)  ;
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }


        [HttpGet]
        [Route("api/TestBank/Select")]
        public IHttpActionResult Select(SimpleQueryModel query)
        {
            try
            {                
                apiResult = testBankBo.Select(query);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message)  ;
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/TestBank/Questions/Select")]
        public IHttpActionResult QuestionsSelect(SimpleQueryModel query)
        {
            try
            {
                apiResult = testQuestionMapBo.Select(query);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message)  ;
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/TestBank/Link/Questions")]
        public IHttpActionResult MapQuestion(List<TestQuestionMapping> inputs)
        {
            try
            {
                apiResult = testQuestionMapBo.Create(inputs);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message)  ;
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/TestBank/Unlink/Questions")]
        public IHttpActionResult DeleteMapQuestion(List<TestQuestionMapping> inputs)
        {
            try
            {
                apiResult = testQuestionMapBo.Delete(inputs);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message)  ;
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }
    }
}
