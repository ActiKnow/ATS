using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Global;
using ATS.Core.Helper;
using ATS.Core.Model;
using ATS.Bll.Factory.Question;
using ATS.Repository.Model;
using ATS.Repository.Uow;

namespace ATS.Bll
{
    public class QuestionBankBo
    {
        public ApiResult Create(QuestionBankModel questionBank)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {                
                try
                {
                    //var flag = unitOfWork.QuestionRepo.Create(ref questionBank);
                    questionBank.QId = Guid.NewGuid();
                    QuestionFactory quesFactory = new QuestionFactory(unitOfWork, (CommonType)questionBank.QuesTypeValue);
                    var flag = quesFactory.Question.Create(ref questionBank);
                    if (flag)
                    {
                        unitOfWork.Commit();
                        apiResult.Message.Add("Created successfully");

                        var result = Select(null);

                        if (result != null)
                        {
                            apiResult += result;
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

        public ApiResult Delete(QuestionBankModel questionBankModel)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                var flag = false;
                try
                {
                    QuestionBank questionBank = new QuestionBank();
                    Utility.CopyEntity(out questionBank, questionBankModel);

                    flag = unitOfWork.QuestionRepo.Delete(questionBank);

                    if (flag)
                    {
                        unitOfWork.Commit();
                        apiResult.Message.Add("Deleted successfully.");

                        var result = Select(null);

                        if (result != null)
                        {
                            apiResult += result;
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
                var queryable = unitOfWork.QuestionRepo.Retrieve(guid);

                var questionBankModel = queryable.FirstOrDefault();
                if (questionBankModel != null)
                {
                    apiResult.Status = true;
                    apiResult.Data = questionBankModel;
                }
                else
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("No record found for this Question");
                }
            }
            return apiResult;
        }

        public ApiResult Select(SimpleQueryModel qry)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                SimpleQueryBuilder<QuestionBankModel> simpleQry = new SimpleQueryBuilder<QuestionBankModel>();
                var queryable = unitOfWork.QuestionRepo.Select(simpleQry.GetQuery(query: qry).Compile());

                List<QuestionBankModel> listQuestion = queryable.ToList();

                if (listQuestion != null)
                {
                    foreach (var ques in listQuestion)
                    {
                        QuestionFactory quesFactory = new QuestionFactory(unitOfWork, (CommonType)ques.QuesTypeValue);
                        var questionFound = quesFactory.Question.Select(x => x.QId == ques.QId).FirstOrDefault();
                        if (questionFound != null)
                        {
                            ques.AnsText = questionFound.AnsText;
                            ques.Options = questionFound.Options;
                            ques.MappedOptions = questionFound.MappedOptions;
                            ques.MappedQuestion = questionFound.MappedQuestion;
                        }
                    }
                    apiResult.Status = true;
                    apiResult.Data = listQuestion;
                }
                else
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("No record found");
                }
            }
            return apiResult;
        }

        public ApiResult Update(QuestionBankModel questionBank)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    //var flag = unitOfWork.QuestionRepo.Update(ref questionBank);
                    QuestionFactory quesFactory = new QuestionFactory(unitOfWork, (CommonType)questionBank.QuesTypeValue);
                    var flag = quesFactory.Question.Create(ref questionBank);
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
                    throw;
                }
            }
            return apiResult;
        }

        public ApiResult SelectQuetionsByType(Guid QId)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    var queryable = unitOfWork.QuestionRepo.Retrieve(QId);
                    var dataFound = queryable.FirstOrDefault();

                    if (dataFound != null)
                    {
                        TypeDefBo typeDefBo = new TypeDefBo();
                        var res = typeDefBo.GetByValue(dataFound.QuesTypeValue);

                        if (res != null && res.Status && res.Data != null)
                        {
                            var typeDef = (TypeDef)res.Data;
                            CommonType type = (CommonType)typeDef.Value;
                            var result = new List<QuestionBankModel>();
                            QuestionFactory selector = new QuestionFactory(unitOfWork, type);

                            if (selector.QuestionSelector != null)
                            {
                                result.Add(selector.QuestionSelector.Select(x => x.QId == QId).FirstOrDefault());
                                apiResult.Data = result;
                                apiResult.Status = true;
                            }
                            else
                            {
                                apiResult.Status = false;
                                apiResult.Message.Add("No Record found for question type");
                            }
                        }
                        else
                        {
                            apiResult.Status = false;
                            apiResult.Message.Add("No Record found");
                        }
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
    }
}
