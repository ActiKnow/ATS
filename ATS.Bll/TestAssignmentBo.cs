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
                    var flag = false;
                    Utility.CopyEntity(out List<TestAssignment> output, testAssignmentModel);
                    var withoutId = output.Where(x => x.ID == Guid.Empty).ToList();
                    foreach (var map in withoutId)
                    {
                        if (map.ID == Guid.Empty)
                        {
                            var result = unitOfWork.TestAssignmentRepo.Select(x => x.TestBankId == map.TestBankId && x.UserId == map.UserId).ToList();
                            if (result.Count()<1)
                            {
                                flag = unitOfWork.TestAssignmentRepo.Assign(output);
                            }
                        }
                    }
                    if (flag)
                    {
                        unitOfWork.Commit();
                        apiResult.Status = true;
                        apiResult.Message.Add(" mapped successfully");
                    }
                    else
                    {
                        apiResult.Status = false;
                        apiResult.Message.Add(" mapping failed");
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
        public ApiResult Delete(TestAssignmentModel inputs)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    var flag = false;
                    TestAssignment output = new TestAssignment();
                    Utility.CopyEntity(out output, inputs);
                    var map = output;

                     if (map.ID == Guid.Empty)
                     {
                         var result = unitOfWork.TestAssignmentRepo.Select(x => x.TestBankId == map.TestBankId && x.UserId == map.UserId).ToList();
                         if (result != null)
                         {
                             var data = (result as List<TestAssignmentModel>).FirstOrDefault();
                             if (data != null)
                             {
                                 map.ID = data.ID;
                                 flag = unitOfWork.TestAssignmentRepo.DeleteMappedTest(map);
                             }
                         }
                     }

                    if (flag)
                    {
                        apiResult.Status = true;
                        apiResult.Message.Add("Deleted successfully.");
                        unitOfWork.Commit();
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

    }
}
