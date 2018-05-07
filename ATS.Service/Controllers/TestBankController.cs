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
        public TestBankController()
        {
            repository = new TestBankRepository();
        }

        [HttpPost]
        [Route("api/TestBank/Create")]
        public IHttpActionResult Create(TestBankModel testBank)
        {
            var result = repository.Create(testBank);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/TestBank/Update")]
        public IHttpActionResult Update(TestBankModel testBank)
        {
            var result = repository.Update(testBank);
            return Ok(result);
        }

        [HttpDelete]
        [Route("api/TestBank/Delete")]
        public IHttpActionResult Delete(TestBankModel testBank)
        {
            var result = repository.Delete(testBank);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/TestBank/Retrieve")]
        public IHttpActionResult Retrieve(TestBankModel testBank)
        {
            var result = repository.Retrieve(testBank);
            return Ok(result);
        }


        [HttpPost]
        [Route("api/TestBank/Select")]
        public IHttpActionResult Select(params object[] inputs)
        {
            var result = repository.Select(null);
            return Ok(result);
        }
    }
}
