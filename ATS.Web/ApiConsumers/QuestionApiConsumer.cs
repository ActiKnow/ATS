using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATS.Core.Model;

namespace ATS.Web.ApiConsumers
{
    public class QuestionApiConsumer
    {
        public static ApiResult CreateQuestion(QuestionBankModel QuestionView)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/Question/Create";
                apiResult = ConsumerMethods.Post<List<QuestionBankModel>>(url, QuestionView);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult UpdateQuestion(QuestionBankModel QuestionView)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/Question/Update";
                apiResult = ConsumerMethods.Post<List<QuestionBankModel>>(url, QuestionView);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

        public static ApiResult DeleteQuestion(QuestionBankModel QId)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/Question/Delete";
                apiResult = ConsumerMethods.Post<List<QuestionBankModel>>(url, QId);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult Select(SimpleQueryModel query=null)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/Question/Select";
                apiResult = ConsumerMethods.Post<List<QuestionBankModel>>(url,query);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
       
    }
}