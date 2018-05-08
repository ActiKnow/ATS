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
        public static ApiResult GetParentTypes()
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TypeDef/Select";
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
                var list=new List<SelectListItem>() { new SelectListItem { Text = "Active", Value = "1" }, new SelectListItem { Text = "Inactive", Value = "0" } };
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