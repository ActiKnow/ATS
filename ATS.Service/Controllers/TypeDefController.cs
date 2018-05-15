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
    public class TypeDefController : ApiController
    {
        private TypeDefBo typeDefBo;
        ApiResult apiResult = null;
        public TypeDefController()
        {
            typeDefBo = new TypeDefBo();
            apiResult = new ApiResult(false, new List<string>());
        }

        [HttpPost]
        [Route("api/TypeDef/Create")]
        public IHttpActionResult Create(TypeDefModel input)
        {
            try
            {
                apiResult = typeDefBo.Create(input);
            }
            catch (Exception ex)
            {
                apiResult.Status = false;
                apiResult.Message.Add(ex.GetBaseException().Message);
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/TypeDef/Update")]
        public IHttpActionResult Update(TypeDefModel input)
        {
            try
            {
                apiResult = typeDefBo.Update(input);
            }
            catch (Exception ex)
            {
                apiResult.Status = false;
                apiResult.Message.Add(ex.GetBaseException().Message);
            }
            return Ok(apiResult);
        }

        [HttpDelete]
        [Route("api/TypeDef/Delete")]
        public IHttpActionResult Delete(TypeDefModel input)
        {
            try
            {
                apiResult = typeDefBo.Delete(input);
            }
            catch (Exception ex)
            {
                apiResult.Status = false;
                apiResult.Message.Add(ex.GetBaseException().Message);
            }
            return Ok(apiResult);
        }

        [HttpGet]
        [Route("api/TypeDef/Retrieve/{typeValue}")]
        public IHttpActionResult Retrieve(int typeValue)
        {
            try
            {
                apiResult = typeDefBo.GetByValue(typeValue);
            }
            catch (Exception ex)
            {
                apiResult.Status = false;
                apiResult.Message.Add(ex.GetBaseException().Message);
            }
            return Ok(apiResult);
        }


        //[HttpGet]
        //[Route("api/TypeDef/Select/{isParentDependent}/{parentKey}")]
        //public IHttpActionResult Select(bool isParentDependent, int parentKey = 0)
        //{
        //    try
        //    {
        //        if (isParentDependent)
        //        {
        //            apiResult = typeDefBo.Select(x => x.ParentKey == parentKey);
        //        }
        //        else
        //        {
        //            apiResult = typeDefBo.Select(null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        apiResult.Status = false;
        //        apiResult.Message.Add(ex.GetBaseException().Message)  ;
        //    }
        //    return Ok(apiResult);
        //}

        [HttpGet]
        [Route("api/TypeDef/ValidateType/{typeName}")]
        public IHttpActionResult ValidateType(string typeName)
        {
            try
            {
                typeDefBo.Validate(typeName);
            }
            catch (Exception ex)
            {
                apiResult.Status = false;
                apiResult.Message.Add(ex.GetBaseException().Message);
            }
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("api/TypeDef/Select")]
        public IHttpActionResult Select(SimpleQueryModel query)
        {
            try
            {
                apiResult = typeDefBo.Select(query);
            }
            catch (Exception ex)
            {
                apiResult.Status = false;
                apiResult.Message.Add(ex.GetBaseException().Message);
            }
            return Ok(apiResult);
        }
    }
}
