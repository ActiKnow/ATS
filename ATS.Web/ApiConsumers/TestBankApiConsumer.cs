using ATS.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATS.Web.ApiConsumers
{
    public class TestBankApiConsumer
    {
        public static ApiResult CreateTest(TestBankModel testModel)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TestBank/Create";
                apiResult = ConsumerMethods.Post<List<TestBankModel>>(url, testModel);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult UpdateTest(TestBankModel testModel)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TestBank/Update";
                apiResult = ConsumerMethods.Post<List<TestBankModel>>(url, testModel);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult RetrieveTest(TestBankModel testModel)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TestBank/Retrieve";
                apiResult = ConsumerMethods.Post<TestBankModel>(url, testModel);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

        public static ApiResult DeleteTest(TestBankModel testModel)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TestBank/Delete";
                apiResult = ConsumerMethods.Post<List<TestBankModel>>(url, testModel);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult Select(SimpleQueryModel query = null)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TestBank/Select";
                apiResult = ConsumerMethods.Post<List<TestBankModel>>(url, query);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult TestQuestionsSelect(SimpleQueryModel query = null)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TestBank/Questions/Select";
                apiResult = ConsumerMethods.Post<List<QuestionBankModel>>(url, query);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult TestQuestionsLink(List<TestQuestionMapModel> linkQuestion)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TestBank/Link/Questions";
                apiResult = ConsumerMethods.Post<List<TestQuestionMapModel>>(url, linkQuestion);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult TestQuestionsUnlink(List<TestQuestionMapModel> linkQuestion)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TestBank/Unlink/Questions";
                apiResult = ConsumerMethods.Post<List<TestQuestionMapModel>>(url, linkQuestion);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult AssignTest(List<TestAssignmentModel> testAssignmentModel)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TestBank/Assign";
                apiResult = ConsumerMethods.Post<List<TestAssignmentModel>>(url, testAssignmentModel);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult SelectMapped(Guid userId)
        {
            ApiResult apiResult = null;
            try
            {
                string url = string.Format("api/TestBank/SelectMapped/{0}", userId);
                apiResult = ConsumerMethods.Get<List<TestBankModel>>(url);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult SelectUnMapped(Guid userId)
        {
            ApiResult apiResult = null;
            try
            {
                string url = string.Format("api/TestBank/SelectUnmapped/{0}", userId);
                apiResult = ConsumerMethods.Get<List<TestBankModel>>(url);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
    }
}