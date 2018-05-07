using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATS.Core.Model;
using ATS.Repository.Interface;
using ATS.Repository.DAO;
using ATS.Core.CommonModel;

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
        public IHttpActionResult ValidateUser(UserCredential userCredential)
        {
            ApiResult apiResult = null;

            var userId = userRepository.ValidateUser(userCredential);
            if (userId!= Guid.Empty)
            {
                UserInfo userInfo = new UserInfo();
                userInfo.Email = userCredential.EmailId;
                userInfo.UserId = userId;

                userInfo = userRepository.Retrieve(userInfo);

                apiResult = new ApiResult("", true, userInfo);
            }
            else
            {
                apiResult = new ApiResult("Username & Password is incorrect.", false);
            }
            
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/User/Create")]
        public IHttpActionResult Create( UserInfo userCredential)
        {
            var result = userRepository.Create(userCredential);
            return Ok(result);
        }
    }
}
