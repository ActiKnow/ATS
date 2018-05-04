using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATS.Repository.DAO;
using ATS.Repository.Interface;

namespace ATS.Service.Controllers
{
    public class TypeDefController : ApiController
    {
        private ITypeRepository typeRepository = null;
        public TypeDefController()
        {
            typeRepository = new TypeDefRepository();
        }  
        
    }
}
