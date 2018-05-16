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
    public class UserController : ApiController
    {
        private UserInfoBo userInfoBo = null;
        public UserController()
        {
            //userRepository = new UserRepository();
            userInfoBo = new UserInfoBo();
        }

        [HttpPost]
        [Route("api/User/Validate")]
        public IHttpActionResult ValidateUser(UserCredentialModel input)
        {
            ApiResult apiResult = null;
            try
            {
                apiResult = userInfoBo.Validate(input);
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/User/Create")]
        public IHttpActionResult Create(UserInfoModel input)
        {
            ApiResult apiResult = null;
            try
            {
                apiResult = userInfoBo.Create(input);
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Ok(apiResult);
        }

        [HttpGet]
        [Route("api/User/Select")]
        public IHttpActionResult Select(SimpleQueryModel qry)
        {
            ApiResult apiResult = null;
            try
            {              
                apiResult = userInfoBo.Select(qry);
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/User/Retrieve")]
        public IHttpActionResult Retrieve(UserInfoModel userInfoModel)
        {
            ApiResult apiResult = null;
            try
            {
                apiResult = userInfoBo.GetById(userInfoModel.UserId);
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/User/Disable")]
        public IHttpActionResult Disable(UserInfoModel input)
        {
            ApiResult apiResult = null;
            try
            {
                apiResult = userInfoBo.Disable(input);
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/User/Update")]
        public IHttpActionResult Update(UserInfoModel input)
        {
            ApiResult apiResult = null;
            try
            {
                apiResult = userInfoBo.Update(input);
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
            }
            return Ok(apiResult);
        }
    }
}
