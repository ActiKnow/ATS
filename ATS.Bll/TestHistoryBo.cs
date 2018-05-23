using ATS.Core.Helper;
using ATS.Core.Model;
using ATS.Repository.Model;
using ATS.Repository.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Bll
{
    public class TestHistoryBo
    {
        public ApiResult Create(UserTestHistoryModel input)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            var flag = false;


            input.HistoryId = Guid.NewGuid();
            input.AssignedDate = DateTime.Now;
            input.ReusableDate = DateTime.Now;
            Utility.CopyEntity(out UserTestHistory userTestHistory, input);

            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    //User test history creation
                    flag = unitOfWork.TestHistoryRepo.Create(ref userTestHistory);

                    if (flag)
                    {
                        Utility.CopyEntity(out List<UserAttemptedHistory> attemptedQuestions, input.UserAttemptedHistories);

                        //User Test Attempt history
                        int count = attemptedQuestions.Count;
                        for (int indx = 0; indx < count; indx++)
                        {
                            var attempt = attemptedQuestions[indx];
                            attempt.Id = Guid.NewGuid();
                            attempt.History_Id = userTestHistory.HistoryId;
                            flag = unitOfWork.AttemptedHistoryRepo.Create(ref attempt);
                            if (!flag)
                            { break; }
                        }

                        if (flag)
                        {
                            unitOfWork.Commit();

                            apiResult.Status = true;
                            apiResult.Message.Add("User Test History created successfully.");
                        }
                        else
                        {
                            apiResult.Message.Add("User Test History creation failed.");
                            apiResult.Status = false;
                            unitOfWork.Dispose();
                        }
                    }
                    else
                    {
                        apiResult.Message.Add("User Test History creation failed.");
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

        public ApiResult Update(UserTestHistoryModel input)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            var flag = false;


            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    var historyFound = unitOfWork.TestHistoryRepo.Select(x => x.HistoryId == input.HistoryId).FirstOrDefault();
                    if (historyFound != null)
                    {
                        historyFound.IsFinished = input.IsFinished;
                        historyFound.LastUsedDate = DateTime.Now;
                        historyFound.TotalDuration = input.TotalDuration;
                        
                        Utility.CopyEntity(out UserTestHistory userTestHistory, historyFound);
                        //User test history Updation
                        flag = unitOfWork.TestHistoryRepo.Update(ref userTestHistory);

                        if (flag)
                        {
                            //Delete old history
                            var oldHistory = unitOfWork.AttemptedHistoryRepo.Select(x => x.History_Id == input.HistoryId).ToList();
                            Utility.CopyEntity(out List<UserAttemptedHistory> oldAttempted, oldHistory);
                            int count = oldHistory.Count;
                            for (int indx = 0; indx < count; indx++)
                            {
                                flag = unitOfWork.AttemptedHistoryRepo.Delete(oldAttempted[indx]);
                                if (!flag)
                                { break; }
                            }
                            if (flag)
                            {
                                Utility.CopyEntity(out List<UserAttemptedHistory> attemptedQuestions, input.UserAttemptedHistories);

                                //Create User Test Attempt history
                                count = attemptedQuestions.Count;
                                for (int indx = 0; indx < count; indx++)
                                {
                                    var attempt = attemptedQuestions[indx];
                                    attempt.Id = Guid.NewGuid();
                                    attempt.History_Id = userTestHistory.HistoryId;
                                    flag = unitOfWork.AttemptedHistoryRepo.Create(ref attempt);
                                    if (!flag)
                                    { break; }
                                }
                            }
                        }
                        if (flag)
                        {
                            unitOfWork.Commit();
                            apiResult.Status = true;
                            apiResult.Message.Add("User Test History created successfully.");
                        }
                        else
                        {

                            apiResult.Message.Add("User Test History creation failed.");
                            apiResult.Status = false;
                            unitOfWork.Dispose();
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

        public ApiResult Select(SimpleQueryModel qry)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());

            using (var unitOfWork = new UnitOfWork())
            {

                SimpleQueryBuilder<UserTestHistoryModel> simpleQry = new SimpleQueryBuilder<UserTestHistoryModel>();


                var testHistory = unitOfWork.TestHistoryRepo.Select(simpleQry.GetQuery(query: qry).Compile()).ToList();

                if (testHistory != null)
                {
                    foreach (var history in testHistory)
                    {
                        Guid tempHistoryId = history.HistoryId;
                        history.UserAttemptedHistories = unitOfWork.AttemptedHistoryRepo.Select(x => x.History_Id == tempHistoryId).ToList();
                    }

                    apiResult.Data = testHistory;
                    apiResult.Status = true;
                }
                else
                {
                    apiResult.Message.Add("No record found");
                    apiResult.Status = false;
                }
            }
            return apiResult;
        }
    }
}
