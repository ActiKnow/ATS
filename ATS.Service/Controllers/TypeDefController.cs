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
            ApiResult apiResult = null;

            try
            {
                var result = repository.Create(typeDef);

                if (result)
                {
                    var typeDefs = repository.Select(null);

                    if (typeDefs != null)
                    {
                        apiResult = new ApiResult(true, "", typeDefs);
                    }
                    else
                    {
                        apiResult = new ApiResult(true, "Error in fetching records.", typeDefs);
                    }
                }
                else
                {
                    apiResult = new ApiResult(false, "Type not created.");
                }
            }
            catch(Exception ex)
            {
                apiResult = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/TypeDef/Update")]
        public IHttpActionResult Update(TypeDefModel typeDef)
        {
            ApiResult apiResult = null;

            try
            {
                var result = repository.Update(typeDef);

                if (result)
                {
                    var typeDefs = repository.Select(null);

                    if (typeDefs != null)
                    {
                        apiResult = new ApiResult(true, "", typeDefs);
                    }
                    else
                    {
                        apiResult = new ApiResult(true, "Error in fetching records.", typeDefs);
                    }
                }
                else
                {
                    apiResult = new ApiResult(false, "Type not updated.");
                }
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, ex.GetBaseException().Message);
            }
            return Ok(apiResult);
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


        [HttpGet]
        [Route("api/TypeDef/Select/{parentKey?}")]
        public IHttpActionResult Select(Guid? parentKey=null)
        {
            ApiResult apiResult = null;
             var result = repository.Select(x => x.ParentKey == parentKey);
            
            if (result != null)
            {
                apiResult = new ApiResult(true, "", result);
            }
            else
            {
                apiResult = new ApiResult(false, "No record found");
            }
            return Ok(apiResult);
        }
    }
}
