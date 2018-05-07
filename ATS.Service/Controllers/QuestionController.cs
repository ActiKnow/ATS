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
            questionRepo.Create(newQues);
            return Ok("");
        }

        [HttpPost]
        [Route("api/Question/Update")]
        public IHttpActionResult Update(QuestionBankModel newQues)
        {
            questionRepo.Update(newQues);
            return Ok("");
        }

        [HttpDelete]
        [Route("api/Question/Delete")]
        public IHttpActionResult Delete(QuestionBankModel newQues)
        {
            questionRepo.Delete(newQues);
            return Ok("");
        }
    }
}
