using ATS.Bll;
using ATS.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ATS.Service.Controllers
{
    public class UserTestHistoryController : ApiController
    {
        private TestHistoryBo testBankHistoryBo;
        ApiResult apiResult;
        public UserTestHistoryController()
        {
            testBankHistoryBo = new TestHistoryBo();
            apiResult = new ApiResult(false, new List<string>());
        }

        [HttpPost]
        [Route("api/UserTestHistory/Create")]
        public IHttpActionResult Create(UserTestHistoryModel userTestHistory)
        {
            try
            {
                apiResult = testBankHistoryBo.Create(userTestHistory);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message);
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/UserTestHistory/Update")]
        public IHttpActionResult Update(UserTestHistoryModel userTestHistory)
        {
            try
            {
                apiResult = testBankHistoryBo.Update(userTestHistory);
            }
            catch (Exception ex)
            {
                apiResult.Message.Add(ex.GetBaseException().Message);
                apiResult.Status = false;
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/UserTestHistory/Select")]
        public IHttpActionResult Select(SimpleQueryModel query)
        {
            try
            {
                apiResult = testBankHistoryBo.Select(query);
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
