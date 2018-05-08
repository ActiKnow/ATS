using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATS.Repository.DAO;
using ATS.Repository.Interface;
using ATS.Core.Model;

namespace ATS.Service.Controllers
{
    public class TestBankController : ApiController
    {
        private ITestRepository repository = null;
        private IMapQuestionRepository mapQuesRepository = null;
        private IQuestionRepository quesRepository = null;
        public TestBankController()
        {
            repository = new TestBankRepository();
            mapQuesRepository = new MapQuestionRepository();
            quesRepository = new QuestionRepository();
        }

        [HttpPost]
        [Route("api/TestBank/Create")]
        public IHttpActionResult Create(TestBankModel testBank)
        {
            ApiResult apiResult = new ApiResult(false, "Not Created");
            try
            {
                if (repository.Create(testBank))
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
        [Route("api/TestBank/Update")]
        public IHttpActionResult Update(TestBankModel testBank)
        {
            ApiResult apiResult = new ApiResult(false, "Not Updated");
            try
            {
                if (repository.Update(testBank))
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
        [Route("api/TestBank/Delete")]
        public IHttpActionResult Delete(TestBankModel testBank)
        {
            ApiResult apiResult = new ApiResult(false, "Not Deleted");
            try
            {
                if (repository.Delete(testBank))
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
        [Route("api/TestBank/Retrieve")]
        public IHttpActionResult Retrieve(TestBankModel testBank)
        {
            ApiResult apiResult = new ApiResult(false, "Record not found");
            try
            {
                TestBankModel data = repository.Retrieve(testBank);
                if (data != null)
                {
                    apiResult = new ApiResult(true, "", data);
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
        [Route("api/TestBank/Select")]
        public IHttpActionResult Select()
        {
            ApiResult apiResult = new ApiResult(false, "Record not found");
            try
            {
                List<TestBankModel> data = repository.Select(null);
                if (data != null)
                {
                    apiResult = new ApiResult(true, "", data);
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
        [Route("api/TestBank/Questions/Select/{testBankId}")]
        public IHttpActionResult QuestionsSelect(Guid testBankId)
        {
            ApiResult apiResult = new ApiResult(false, "Record not found");
            try
            {
                List<TestQuestionMapModel> dataMaps = mapQuesRepository.Select(x=>x.TestBankId == testBankId);
                List<QuestionBankModel> questions = null;
                if (dataMaps != null)
                {
                    questions = new List<QuestionBankModel>();
                    foreach (var map in dataMaps)
                    {
                        var quesFound = quesRepository.Select(x => x.QId == map.QId).FirstOrDefault();
                        if (quesFound != null)
                        {
                            quesFound.MappedQuestion = map;
                            questions.Add(quesFound);
                        }
                    }
                }
                if (questions != null)
                {
                    apiResult = new ApiResult(true, "", questions);
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
        [Route("api/TestBank/Link/Questions")]
        public IHttpActionResult MapQuestion(List<TestQuestionMapModel> inputs)
        {
            ApiResult apiResult = new ApiResult(false, "Record not Mapped");
            try
            {
                if (inputs != null && mapQuesRepository.Create(inputs))
                {
                    apiResult = new ApiResult(true, "Records Mapped");
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
        [Route("api/TestBank/Unlink/Questions")]
        public IHttpActionResult DeleteMapQuestion(List<TestQuestionMapModel> inputs)
        {
            ApiResult apiResult = new ApiResult(false, "Record not Found");
            try
            {
                if (inputs != null &&  mapQuesRepository.Delete(inputs))
                {
                    apiResult = new ApiResult(true, "Operation successful ");
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
