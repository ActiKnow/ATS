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
        public ApiResult Create(List<TestQuestionMapModel> inputs)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    Utility.CopyEntity(out List<TestQuestionMapping> output, inputs);
                    var flag = unitOfWork.MapQuestionRepo.MapQuestions(output);

                    if (flag)
                    {
                        unitOfWork.Commit();
                        apiResult.Status = true;
                        apiResult.Message.Add("Created successfully");
                        //var map = output.FirstOrDefault();
                        //if (map != null)
                        //{
                        //    SimpleQueryModel query = new SimpleQueryModel { ModelName = nameof(TestQuestionMapping) };
                        //    query[nameof(TestQuestionMapping.TestBankId)] = map.TestBankId;
                        //    var result = Select(query);

                        //    if (result != null)
                        //    {
                        //        apiResult += result;
                        //    }
                        //}
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

        public ApiResult Delete(List<TestQuestionMapModel> inputs)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    var flag = false;
                    List<TestQuestionMapping> output = new List<TestQuestionMapping>();
                    Utility.CopyEntity(out output, inputs);
                    var withoutId = output.Where(x => x.Id == Guid.Empty).ToList();
                    foreach (var map in withoutId)
                    {
                        if (map.Id == Guid.Empty)
                        {
                            var result = unitOfWork.MapQuestionRepo.Select(x=>x.TestBankId == map.TestBankId && x.QId == map.QId).ToList();
                            if (result != null)
                            {
                                var data = (result as List<TestQuestionMapModel>).FirstOrDefault();
                                if (data != null)
                                {
                                    map.Id = data.Id;
                                    flag = unitOfWork.MapQuestionRepo.DeleteMappedQuestion(map);
                                }
                            }
                        }
                    }
                   // flag = unitOfWork.MapQuestionRepo.DeleteMappedQuestions(output);
                    if (flag)
                    {
                        apiResult.Status = true;
                        apiResult.Message.Add("Deleted successfully.");
                        unitOfWork.Commit();

                        //var result = Select(null);

                        //if (result != null)
                        //{
                        //    apiResult += result;
                        //}
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
                    throw;
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
                            if (result.Status && result.Data != null)
                            {
                                var quesFound = ((List<QuestionBankModel>)result.Data).FirstOrDefault();
                                if (quesFound != null)
                                {
                                    quesFound.MappedQuestion = map;
                                    questions.Add(quesFound);
                                }
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

        public ApiResult Update(TestQuestionMapModel input)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    TestQuestionMapping output = new TestQuestionMapping();
                    Utility.CopyEntity(out output, input);

                    var flag = unitOfWork.MapQuestionRepo.Update(ref output);

                    if (flag)
                    {
                        unitOfWork.Commit();
                        apiResult.Message.Add("Updated successfully");

                        var result = Select(null);

                        if (result != null)
                        {
                            apiResult += result;
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
