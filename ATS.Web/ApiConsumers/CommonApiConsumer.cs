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
        public static ApiResult SelectTypes(bool isParentDependent,int parentKey=0)
        {
            ApiResult apiResult = null;
            try
            {
                string url =string.Format("api/TypeDef/Select/{0}/{1}", isParentDependent, parentKey);
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
                var list=new List<SelectListItem>() { new SelectListItem { Text = "Active", Value = "True" }, new SelectListItem { Text = "Inactive", Value = "False" } };
                apiResult = new ApiResult(true, null, list);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
    }
}