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

    }
}