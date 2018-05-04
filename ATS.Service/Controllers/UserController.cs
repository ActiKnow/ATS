using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATS.Core.Model;
using ATS.Repository.Interface;
using ATS.Repository.Repository;

namespace ATS.Service.Controllers
{
    public class UserController : ApiController
    {
        private IUserRepository userRepository = null;
        public UserController()
        {
            userRepository = new UserRepository();
        }

        public IHttpActionResult ValidateUser(UserCredential userCredential)
        {
            var guid = userRepository.ValidateUser(userCredential);
            return Ok(guid);
        }
    }
}
