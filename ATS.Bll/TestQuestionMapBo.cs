using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Helper;
using ATS.Core.Model;
using ATS.Repository.Model;
using ATS.Repository.Uow;

namespace ATS.Bll
{
    public class TestQuestionMapBo
    {       
        public ApiResult Create(List<TestQuestionMapping> input)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    var flag = unitOfWork.MapQuestionRepo.MapQuotions(input);

                    if (flag)
                    {
                        unitOfWork.Commit();

                        apiResult.Message.Add("Created successfully");

                        var result = Select(null);

                        if (result != null)
                        {
                            apiResult+= result;
                        }
                    }
                    else
                    {
                        apiResult.Status = false;
                        apiResult.Message.Add("Creation failed");
                    }
                }
                catch
                {
                    unitOfWork.Dispose();
                    throw;
                }
            }
            return apiResult;
        }

        public ApiResult Delete(List<TestQuestionMapping> inputs)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    var flag = false;

                    flag = unitOfWork.MapQuestionRepo.DeleteMappedQuestions(inputs);

                    if (flag)
                    {
                        apiResult.Message.Add("Deleted successfully.");
                        unitOfWork.Commit();

                        var result = Select(null);

                        if (result != null)
                        {
                            apiResult+= result;
                        }
                    }
                    else
                    {
                        apiResult.Message.Add("Deletion failed.");
                        apiResult.Status = false;
                    }
                }
                catch
                {
                    unitOfWork.Dispose();
                }
            }
            return apiResult;
        }

        public ApiResult GetById(Guid guid)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                var queryable = unitOfWork.MapQuestionRepo.Retrieve(guid);

                var testMapped = queryable.FirstOrDefault();

                if (testMapped != null)
                {
                    apiResult.Status = true;
                    apiResult.Data = testMapped;
                }
                else
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("No record found");
                }
            }
            return apiResult;
        }

        public ApiResult Select(SimpleQueryModel query)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {               
                SimpleQueryBuilder<TestQuestionMapModel> simpleQry = new SimpleQueryBuilder<TestQuestionMapModel>();
                var queryable = unitOfWork.MapQuestionRepo.Select(simpleQry.GetQuery(query).Compile());

                var dataMaps = queryable.ToList();
                List<QuestionBankModel> questions = null;               

                if (dataMaps != null)
                {
                    questions = new List<QuestionBankModel>();
                    QuestionBankBo questionBankBo = new QuestionBankBo();
                    foreach (var map in dataMaps)
                    {
                        var result = questionBankBo.SelectQuetionsByType(map.QId);
                        if (result != null)
                        {
                            if (result.Status && result.Data!=null)
                            {
                                var quesFound=(QuestionBankModel)result.Data;
                                quesFound.MappedQuestion = map;
                                questions.Add(quesFound);
                            }
                        }
                    }
                }

                if (questions != null)
                {
                    unitOfWork.Commit();
                    if (questions.Count > 0)
                    {
                        apiResult.Status = true;
                        apiResult.Data = questions;
                    }
                    else
                    {
                        apiResult.Status = false;
                        apiResult.Message.Add("No record found");
                    }
                }
                else
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("No record found");
                }                
            }
            return apiResult;            
        }

        public ApiResult Update(TestQuestionMapping input)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {  
                    var flag = unitOfWork.MapQuestionRepo.Update(ref input);

                    if (flag)
                    {
                        unitOfWork.Commit();
                        apiResult.Message.Add("Updated successfully");

                        var result = Select(null);

                        if (result != null)
                        {
                            apiResult+= result;
                        }
                    }
                    else
                    {
                        apiResult.Status = false;
                        apiResult.Message.Add("Updation failed");
                    }
                }
                catch
                {
                    unitOfWork.Dispose();
                }
            }
            return apiResult;
        }
    }
}
