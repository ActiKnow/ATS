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
   public class TestAssignmentBo
    {

        public ApiResult Create(List<TestAssignmentModel> testAssignmentModel)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    Utility.CopyEntity(out List<TestAssignment> output, testAssignmentModel);

                    var flag = unitOfWork.TestAssignmentRepo.Assign(output);

                    if (flag)
                    {
                        unitOfWork.Commit();
                        apiResult.Message.Add(output + " created successfully");

                        var result = Select(null);

                        if (result != null)
                        {
                            apiResult += result;
                        }
                    }
                    else
                    {
                        apiResult.Status = false;
                        apiResult.Message.Add(output + " creation failed");
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
                SimpleQueryBuilder<TestAssignmentModel> simpleQry = new SimpleQueryBuilder<TestAssignmentModel>();
                var queryable = unitOfWork.TestAssignmentRepo.Select(simpleQry.GetQuery(query: qry).Compile());

                var testAssign = queryable.ToList();
                if (testAssign != null)
                {
                    apiResult.Status = true;
                    apiResult.Data = testAssign;
                }
                else
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("No record found");
                }
            }
            return apiResult;
        }

    }
}
