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
    public class TypeDefController : ApiController
    {
        private ITypeRepository repository = null;
        public TypeDefController()
        {
            repository = new TypeDefRepository();
        }        

        [HttpPost]
        [Route("api/TypeDef/Create")]
        public IHttpActionResult Create(TypeDefModel typeDef)
        {
            var result = repository.Create(typeDef);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/TypeDef/Update")]
        public IHttpActionResult Update(TypeDefModel typeDef)
        {
            var result = repository.Update(typeDef);
            return Ok(result);
        }

        [HttpDelete]
        [Route("api/TypeDef/Delete")]
        public IHttpActionResult Delete(TypeDefModel typeDef)
        {
            var result = repository.Delete(typeDef);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/TypeDef/Retrieve")]
        public IHttpActionResult Retrieve(TypeDefModel typeDef)
        {
            var result = repository.Retrieve(typeDef);
            return Ok(result);
        }


        [HttpPost]
        [Route("api/TypeDef/Select")]
        public IHttpActionResult Select(params object[] inputs)
        {
            var result = repository.Select(null);
            return Ok(result);
        }
    }
}
