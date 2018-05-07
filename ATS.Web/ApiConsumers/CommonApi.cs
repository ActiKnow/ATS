using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATS.Core.Model;

namespace ATS.Web.ApiConsumers
{
    public class CommonApi
    {
        public static ApiResult GetParentTypes(TypeDefModel typeDef)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TypeDef/Retrieve";
                apiResult = ConsumerMethods.Get<TypeDefModel>(url);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
    }
}