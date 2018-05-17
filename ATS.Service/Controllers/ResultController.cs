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
    public class ResultController : ApiController
    {
        private ResultBo resultBo;
        private TestBankBo testBankBo;
        ApiResult apiResult;

        public ResultController()
        {
            resultBo = new ResultBo();
            testBankBo = new TestBankBo();
            apiResult = new ApiResult(false, new List<string>());
        }

        // GET: Result
        [HttpPost]
        [Route("api/Result/Retrieve")]
        public IHttpActionResult Retrieve(List<Guid> userId)
        {
            try
            {
                apiResult = resultBo.Retrieve(userId);
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