using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATS.Repository.DAO;
using ATS.Repository.Interface;
using ATS.Core.Model;
using ATS.Core.Helper;

namespace ATS.Service.Controllers
{
    public class UserController : ApiController
    {
        private IUserRepository userRepository = null;
        public UserController()
        {
            userRepository = new UserRepository();
        }

        [HttpPost]
        [Route("api/User/Validate")]
        public IHttpActionResult ValidateUser(UserCredentialModel userCredential)
        {
            ApiResult apiResult = null;
            try
            {
                var userId = userRepository.ValidateUser(userCredential);
                if (userId != Guid.Empty)
                {
                    UserInfoModel userInfo = new UserInfoModel();
                    userInfo.Email = userCredential.EmailId;
                    userInfo.UserId = userId;

                    userInfo = userRepository.Retrieve(userInfo);

                    apiResult = new ApiResult(true, "", userInfo);
                }
                else
                {
                    apiResult = new ApiResult(false, "Username & Password is incorrect.");
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
        [Route("api/User/Create")]
        public IHttpActionResult Create(UserInfoModel userCredential)
        {
            ApiResult apiResult = new ApiResult(false, "Not Created");
            try
            {
                if (userRepository.Create(userCredential))
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

        [HttpGet]
        [Route("api/User/Select")]
        public IHttpActionResult Select(SimpleQueryModel qry)
        {
            ApiResult apiResult = new ApiResult(false, "Records not Found");
            try
            {
                SimpleQueryBuilder<UserInfoModel> simpleQry = new SimpleQueryBuilder<UserInfoModel>();
                List<UserInfoModel> user = userRepository.Select(simpleQry.GetQuery(query: qry).Compile());
                if (user != null)
                {
                    apiResult = new ApiResult(true, "", user);
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
        [Route("api/User/Retrieve")]
        public IHttpActionResult Retrieve(UserInfoModel userInfoModel)
        {
            ApiResult apiResult = new ApiResult(false, "Record not found");
            try
            {
                UserInfoModel data = userRepository.Retrieve(userInfoModel);
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
        [HttpPost]
        [Route("api/User/Delete")]
        public IHttpActionResult Delete(UserInfoModel userInfoModel)
        {
            ApiResult apiResult = new ApiResult(false, "Not Deleted");
            try
            {
                if (userRepository.Delete(userInfoModel))
                {
                    apiResult = new ApiResult(true, "Record Deleted");
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
        [Route("api/User/Update")]
        public IHttpActionResult Update(UserInfoModel userInfoModel)
        {
            ApiResult apiResult = new ApiResult(false, "Not Updated");
            try
            {
                if (userRepository.Update(userInfoModel))
                {
                    apiResult = new ApiResult(true, "Record Updated");
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
