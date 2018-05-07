using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATS.Repository.Model;
using ATS.Repository.DAO;
using ATS.Repository.Interface;

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
        public IHttpActionResult Create(TestBank testBank)
        {
            var result = repository.Create(testBank);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/TestBank/Update")]
        public IHttpActionResult Update(TestBank testBank)
        {
            var result = repository.Update(testBank);
            return Ok(result);
        }

        [HttpDelete]
        [Route("api/TestBank/Delete")]
        public IHttpActionResult Delete(TestBank testBank)
        {
            var result = repository.Delete(testBank);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/TestBank/Retrieve")]
        public IHttpActionResult Retrieve(TestBank testBank)
        {
            var result = repository.Retrieve(testBank);
            return Ok(result);
        }


        [HttpPost]
        [Route("api/TestBank/Select")]
        public IHttpActionResult Select(params object[] inputs)
        {
            var result = repository.Select(inputs);
            return Ok(result);
        }
    }
}
