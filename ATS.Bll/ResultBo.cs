using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Global;
using ATS.Core.Helper;
using ATS.Core.Model;
using ATS.Repository.Model;
using ATS.Repository.Repo;
using ATS.Repository.Uow;

namespace ATS.Bll
{
   public class ResultBo
    {
        public ApiResult Retrieve(List<Guid>  userId)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                var queryable = unitOfWork.ResultRepo.Retrieve(userId);

               List<TestAssignmentModel> testBankModel = queryable.ToList();

                if (testBankModel != null)
                {
                    apiResult.Status = true;
                    apiResult.Data = testBankModel;
                }
                else
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("No record found for this user.");
                }
            }
            return apiResult;
        }

    }
}
