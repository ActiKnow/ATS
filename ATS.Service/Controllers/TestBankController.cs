using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATS.Core.Model;
using ATS.Core.Helper;
using ATS.Bll;

namespace ATS.Service.Controllers
{
    public class TestBankController : ApiController
    {       
        private TestBankBo testBankBo;
        private TestQuestionMapBo testQuestionMapBo;
        private TestAssignmentBo testAssignmentBo;
        ApiResult apiResult;
        public TestBankController()
        {
            testBankBo = new TestBankBo();
            testQuestionMapBo = new TestQuestionMapBo();
            testAssignmentBo = new TestAssignmentBo();
            apiResult = new ApiResult(false, new List<string>());
        }

        [HttpPost]
        [Route("api/TestBank/Create")]
        public IHttpActionResult Create(TestBankModel testBankModel)
        {
            try
            {
                apiResult = testBankBo.Create(testBankModel);
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
        public IHttpActionResult Update(TestBankModel testBankModel)
        {
            try
            {
                apiResult = testBankBo.Update(testBankModel);
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
        public IHttpActionResult Delete(TestBankModel testBankModel)
        {
            try
            {
                apiResult = testBankBo.Delete(testBankModel);
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
        public IHttpActionResult Retrieve(TestBankModel testBankModel)
        {
            try
            {
                apiResult = testBankBo.GetById(testBankModel.TestBankId);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message)  ;
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }


        [HttpPost]
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
        public IHttpActionResult MapQuestion(List<TestQuestionMapModel> inputs)
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
        public IHttpActionResult DeleteMapQuestion(List<TestQuestionMapModel> inputs)
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

        [HttpPost]
        [Route("api/TestBank/Assign")]
        public IHttpActionResult Assign(List<TestAssignmentModel> testAssignmentModel)
        {
            try
            {
                apiResult = testAssignmentBo.Create(testAssignmentModel);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message);
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }

        [HttpGet]
        [Route("api/TestBank/SelectMapped/{userId}")]
        public IHttpActionResult SelectMapped(Guid userId)
        {
            try
            {
                apiResult = testBankBo.SelectMapped(userId);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message);
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }
        [HttpGet]
        [Route("api/TestBank/SelectUnmapped/{userId}")]
        public IHttpActionResult SelectUnmapped(Guid userId)
        {
            try
            {
                apiResult = testBankBo.SelectUnmapped(userId);
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
