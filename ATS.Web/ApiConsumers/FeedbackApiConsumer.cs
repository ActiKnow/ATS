using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATS.Core.Model;

namespace ATS.Web.ApiConsumers
{
    public class FeedbackApiConsumer
    {
        public static ApiResult Retrieve(Guid id)   // Getting Feedback info by ID
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/Feedback/Retrieve/"+id;
                apiResult = ConsumerMethods.Get<UserFeedbackModel>(url);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

        public static ApiResult Select(SimpleQueryModel query = null)   // Getting Feedback List by using query
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/Feedback/Select";
                apiResult = ConsumerMethods.Post<List<UserFeedbackModel>>(url, query);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

        public static ApiResult Delete(List<Guid> Ids)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/Feedback/Delete";
                apiResult = ConsumerMethods.Post<List<UserFeedbackModel>>(url, Ids);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }       

        public static ApiResult Count(SimpleQueryModel query)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/Feedback/Count";
                apiResult = ConsumerMethods.Post<int>(url,query);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

    }
}