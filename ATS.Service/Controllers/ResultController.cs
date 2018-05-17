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
        ApiResult apiResult;

        public ResultController()
        {
            resultBo = new ResultBo();
            apiResult = new ApiResult(false, new List<string>());
        }

        // GET: Result
        [HttpGet]
        [Route("api/Result/Retrieve")]
        public IHttpActionResult Retrieve(List<UserInfoModel> userInfoModel)
        {
            try
            {
                apiResult = resultBo.Retrieve(userInfoModel);
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