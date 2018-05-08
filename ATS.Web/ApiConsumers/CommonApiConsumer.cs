using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Core.Model;

namespace ATS.Web.ApiConsumers
{
    public class CommonApiConsumer
    {
        public static ApiResult GetParentTypes(Guid? parentKey=null)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TypeDef/Select/parentKey/"+ parentKey;
                apiResult = ConsumerMethods.Get<List<TypeDefModel>>(url);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

        internal static ApiResult GetStatus()
        {
            ApiResult apiResult = null;
            try
            {
                var list=new List<SelectListItem>() { new SelectListItem { Text = "Active", Value = "true" }, new SelectListItem { Text = "Inactive", Value = "false" } };
                apiResult = new ApiResult(true, "", list);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
    }
}