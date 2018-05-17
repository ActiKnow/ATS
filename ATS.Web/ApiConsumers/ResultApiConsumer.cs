using ATS.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATS.Web.ApiConsumers
{
    public class ResultApiConsumer
    {
        public static ApiResult RetrieveResult(List<Guid> userId)
        {
            ApiResult apiResult = null;
            try
            {
                string url = string.Format("api/Result/Retrieve");     
                
                apiResult = ConsumerMethods.Post<List<TestBankModel>>(url, userId);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

    }
}