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
        
        public static ApiResult GetStatus()
        {
            ApiResult apiResult = null;
            try
            {
                var list=new List<SelectListItem>() { new SelectListItem { Text = "Active", Value = "True" }, new SelectListItem { Text = "Inactive", Value = "False" } };
                apiResult = new ApiResult(true,null, list);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
       
    }
}