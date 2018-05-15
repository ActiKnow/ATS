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
   public class TestBankBo
    {
        ApiResult apiResult = new ApiResult(false, new List<string>());

        public ApiResult Create(TestBank testBank)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    testBank.TestBankId = Guid.NewGuid();
                    var flag = unitOfWork.TestBankRepo.Create(ref testBank);

                    if (flag)
                    {
                        unitOfWork.Commit();
                        apiResult.Message.Add(testBank.Description + " created successfully");

                        var result = Select(null);

                        if (result != null)
                        {
                            apiResult+= result;
                        }
                    }
                    else
                    {
                        apiResult.Status = false;
                        apiResult.Message.Add(testBank.Description + " creation failed");
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

        public ApiResult Delete(TestBank testBank)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var flag = false;
                try
                {
                    flag = unitOfWork.TestBankRepo.Delete(testBank);

                    if (flag)
                    {
                        unitOfWork.Commit();
                        apiResult.Message.Add(testBank.Description + " deleted successfully.");

                        var result = Select(null);

                        if (result != null)
                        {
                            apiResult+= result;
                        }
                    }
                    else
                    {
                        apiResult.Message.Add(testBank.Description + " deletion failed.");
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
            using (var unitOfWork = new UnitOfWork())
            {
               var queryable = unitOfWork.TestBankRepo.Retrieve(guid);

                var typeInfo = queryable.FirstOrDefault();

                if (typeInfo != null)
                {
                    apiResult.Status = true;
                    apiResult.Data = typeInfo;
                }
                else
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("No record found for this TestBank");
                }
            }
            return apiResult;
        }

        public ApiResult Select(SimpleQueryModel qry)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                SimpleQueryBuilder<TestBankModel> simpleQry = new SimpleQueryBuilder<TestBankModel>();
                var queryable = unitOfWork.TestBankRepo.Select(simpleQry.GetQuery(query: qry).Compile());

                var testBanks = queryable.ToList();
                if (testBanks != null)
                {
                    apiResult.Status = true;
                    apiResult.Data = testBanks;
                }
                else
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("No record found");
                }
            }
            return apiResult;
        }

        public ApiResult Update(TestBank testBank)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    var flag = unitOfWork.TestBankRepo.Update(ref testBank);

                    if (flag)
                    {
                        unitOfWork.Commit();
                        apiResult.Message.Add(testBank.Description + " updated successfully");

                        var result = Select(null);

                        if (result != null)
                        {
                            apiResult+= result;
                        }
                    }
                    else
                    {
                        apiResult.Status = false;
                        apiResult.Message.Add(testBank.Description + " updation failed");
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
