using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Repository.Interface;
using ATS.Core.Model;
using ATS.Repository.Model;

namespace ATS.Repository.DAO
{
    public class TestBankRepository : BaseRepository, ITestRepository
    {
        public bool Create(TestBankModel input)
        {
            bool isCreated = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (input != null)
                        {
                           // context.TestBank.Add(input);
                            context.SaveChanges();

                            dbContextTransaction.Commit();
                            isCreated = true;
                        }
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
                return isCreated;
            }
        }              

        public bool Delete(TestBankModel input)
        {
            bool isDeleted = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        TestBank testBank = context.TestBank.AsNoTracking().Where(x => x.TestBankId == input.TestBankId).FirstOrDefault();
                        if (testBank != null)
                        {
                            //context.TestBank.Remove(input);
                            context.SaveChanges();

                            dbContextTransaction.Commit();
                            isDeleted = true;
                        }
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
                return isDeleted;
            }
        }
       
        public TestBankModel Retrieve(TestBankModel input)
        {
            TestBankModel testBank = null; ;
            using (var context = GetConnection())
            {
                try
                {
                    //testBank = context.TestBank.AsNoTracking().Where(x => x.TestBankId == input.TestBankId).FirstOrDefault();
                }
                catch
                {
                    throw;
                }
                return testBank;
            }
        }

        public List<TestBankModel> Select(params object[] inputs)
        {
            List<TestBankModel> testBank=null;
            using (var context = GetConnection())
            {
                try
                {
                    //testBank = context.TestBank.AsNoTracking().ToList();
                }
                catch
                {
                    throw;
                }
                return testBank;
            }
        }

        public bool Update(TestBankModel input)
        {
            bool isUpdated = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var testBank = context.TestBank.AsNoTracking().Where(x => x.TestBankId == input.TestBankId).FirstOrDefault();

                        if (testBank != null)
                        {
                            testBank.LastUpdatedBy = input.LastUpdatedBy;
                            testBank.LastUpdatedDate = input.LastUpdatedDate;
                            testBank.CategoryTypeId = input.CategoryTypeId;
                            testBank.Description = input.Description;
                            testBank.Duration = input.Duration;
                            testBank.Instructions = input.Instructions;
                            testBank.LavelTypeId = input.LavelTypeId;
                            testBank.Status = input.Status;
                            testBank.TestTypeId = input.TestTypeId;
                            testBank.TotalMarks = input.TotalMarks;                            

                            context.SaveChanges();
                            dbContextTransaction.Commit();
                            isUpdated = true;
                        }
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
                return isUpdated;
            }
        }        
    }
}
