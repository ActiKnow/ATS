using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Global;
using ATS.Core.Helper;
using ATS.Core.Model;
using ATS.Repository.Model;
using ATS.Repository.Uow;

namespace ATS.Bll
{
   public class FeedbackBo
    {
        public ApiResult Create(UserFeedback input)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            var flag = false;

            input.StatusId =(int) CommonType.INACTIVE;   // first it will be in inactive mode.

            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    flag = unitOfWork.FeedbackRepo.Create(ref input);

                    if (flag)
                    {
                        unitOfWork.Commit();
                        apiResult.Status = true;
                        apiResult.Message.Add("Thanks for your feedback");                        
                    }
                    else
                    {
                        apiResult.Message.Add("Feedback creation failed.");
                        apiResult.Status = false;
                        unitOfWork.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    unitOfWork.Dispose();
                }
            }
            return apiResult;
        }

        public ApiResult Delete(List<Guid> Ids)
        {
            ApiResult apiResultDelete = new ApiResult(false, new List<string>());
            var flag = false;
            using (var unitOfWork = new UnitOfWork())
            {
                flag = unitOfWork.FeedbackRepo.Disable(Ids);
                              
                if (flag)
                {
                    unitOfWork.Commit();
                    apiResultDelete.Status = true;
                    apiResultDelete.Message.Add("Feedback deleted successfully.");

                    var result = Select(null);  // Getting all records, when we will pass null in Select method.

                    if (result != null)
                    {
                        apiResultDelete += result;
                    }
                }
                else
                {
                    apiResultDelete.Message.Add("Feedback deletion failed.");
                    apiResultDelete.Status = false;
                }
            }
            return apiResultDelete;
        }

        public ApiResult Count()
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                var count = unitOfWork.FeedbackRepo.Count();

                apiResult.Data = count;
                apiResult.Status = true;
            }
            return apiResult;
        }

        public ApiResult GetById(Guid Id)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                var queryable = unitOfWork.FeedbackRepo.Retrieve(Id);

                var userFeedback = queryable.FirstOrDefault();

                if (userFeedback != null)
                {
                    apiResult.Status = true;
                    apiResult.Data = userFeedback;
                }
                else
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("No record found for this feedback.");
                }
            }
            return apiResult;
        }

        public ApiResult Select(SimpleQueryModel qry)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());

            using (var unitOfWork = new UnitOfWork())
            {

                SimpleQueryBuilder<UserFeedback> simpleQry = new SimpleQueryBuilder<UserFeedback>();

                var queryableList = unitOfWork.FeedbackRepo.Select(simpleQry.GetQuery(query: qry).Compile());

                var feedBacks = queryableList.ToList();

                if (feedBacks != null)
                {
                    apiResult.Data = feedBacks;
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
