using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Repository.Interface;
using ATS.Core.Model;
using ATS.Repository.Model;

namespace ATS.Repository.Repo
{
    public class TestBankRepository : Repository<TestBank>, ITestBankRepository
    {
        private readonly ATSDBContext _context;
        public TestBankRepository(ATSDBContext context) : base(context)
        {
            this._context = new ATSDBContext();
        }

        //public bool Create(TestBankModel input)
        //{
        //    bool isCreated = false;
        //    using (var context = GetConnection())
        //    {
        //        using (var dbContextTransaction = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                if (input != null)
        //                {
        //                    TestBank testBank = new TestBank
        //                    {
        //                        CategoryTypeId = input.CategoryTypeId,
        //                        CreatedBy = input.CreatedBy,
        //                        CreatedDate = input.CreatedDate,
        //                        Description = input.Description,
        //                        Duration = input.Duration,
        //                        Instructions = input.Instructions,
        //                        LavelTypeId = input.LavelTypeId,
        //                        StatusId = input.StatusId,
        //                        TestBankId = input.TestBankId,
        //                        TestTypeId = input.TestTypeId,
        //                        TotalMarks = input.TotalMarks
        //                    };
        //                    context.TestBank.Add(testBank);
        //                    context.SaveChanges();

        //                    dbContextTransaction.Commit();
        //                    isCreated = true;
        //                }
        //            }
        //            catch
        //            {
        //                dbContextTransaction.Rollback();
        //                throw;
        //            }
        //        }
        //        return isCreated;
        //    }
        //}

        //public bool Delete(TestBankModel input)
        //{
        //    bool isDeleted = false;
        //    using (var context = GetConnection())
        //    {
        //        using (var dbContextTransaction = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                TestBank testBank = context.TestBank.AsNoTracking().Where(x => x.TestBankId == input.TestBankId).FirstOrDefault();
        //                if (testBank != null)
        //                {
        //                    context.TestBank.Remove(testBank);
        //                    context.SaveChanges();

        //                    dbContextTransaction.Commit();
        //                    isDeleted = true;
        //                }
        //            }
        //            catch
        //            {
        //                dbContextTransaction.Rollback();
        //                throw;
        //            }
        //        }
        //        return isDeleted;
        //    }
        //}

        //public TestBankModel Retrieve(TestBankModel input)
        //{
        //    TestBankModel testBank = null; ;
        //    using (var context = GetConnection())
        //    {
        //        try
        //        {
        //            var query = (from x in context.TestBank.AsNoTracking()
        //                         join l in context.TypeDef on x.CategoryTypeId equals l.TypeId
        //                         join m in context.TypeDef on x.LavelTypeId equals m.TypeId
        //                         join n in context.TypeDef on x.TestTypeId equals n.TypeId
        //                         select new TestBankModel
        //                         {
        //                             CategoryTypeId = x.CategoryTypeId,
        //                             CreatedBy = x.CreatedBy,
        //                             CreatedDate = x.CreatedDate,
        //                             Description = x.Description,
        //                             Duration = x.Duration,
        //                             Instructions = x.Instructions,
        //                             LavelTypeId = x.LavelTypeId,
        //                             StatusId = x.StatusId,
        //                             TestBankId = x.TestBankId,
        //                             TestTypeId = x.TestTypeId,
        //                             TotalMarks = x.TotalMarks,
        //                             LastUpdatedBy = x.LastUpdatedBy,
        //                             LastUpdatedDate = x.LastUpdatedDate,
        //                             CategoryTypeDescription = l.Description,
        //                             CategoryTypeValue = l.Value,
        //                             LavelTypeDescription = m.Description,
        //                             LavelTypeValue = m.Value,
        //                             TestTypeDescription = n.Description,
        //                             TestTypeValue = n.Value
        //                         });

        //            testBank = query.Where(x => x.TestBankId == input.TestBankId).FirstOrDefault();

        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //        return testBank;
        //    }
        //}

        //public List<TestBankModel> Select(Func<TestBankModel, bool> condition)
        //{
        //    List<TestBankModel> testBanks = null;
        //    using (var context = GetConnection())
        //    {
        //        try
        //        {
        //            var query = (from x in context.TestBank.AsNoTracking()
        //                         join l in context.TypeDef on x.CategoryTypeId equals l.TypeId
        //                         join m in context.TypeDef on x.LavelTypeId equals m.TypeId
        //                         join n in context.TypeDef on x.TestTypeId equals n.TypeId
        //                         select new TestBankModel
        //                         {
        //                             CategoryTypeId = x.CategoryTypeId,
        //                             CreatedBy = x.CreatedBy,
        //                             CreatedDate = x.CreatedDate,
        //                             Description = x.Description,
        //                             Duration = x.Duration,
        //                             Instructions = x.Instructions,
        //                             LavelTypeId = x.LavelTypeId,
        //                             StatusId = x.StatusId,
        //                             TestBankId = x.TestBankId,
        //                             TestTypeId = x.TestTypeId,
        //                             TotalMarks = x.TotalMarks,
        //                             LastUpdatedBy = x.LastUpdatedBy,
        //                             LastUpdatedDate = x.LastUpdatedDate,
        //                             CategoryTypeDescription = l.Description,
        //                             CategoryTypeValue = l.Value,
        //                             LavelTypeDescription = m.Description,
        //                             LavelTypeValue = m.Value,
        //                             TestTypeDescription = n.Description,
        //                             TestTypeValue = n.Value
        //                         });

        //            testBanks = query.Where(condition).ToList();
        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //        return testBanks;
        //    }
        //}

        //public bool Update(TestBankModel input)
        //{
        //    bool isUpdated = false;
        //    using (var context = GetConnection())
        //    {
        //        using (var dbContextTransaction = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var testBank = context.TestBank.AsNoTracking().Where(x => x.TestBankId == input.TestBankId).FirstOrDefault();

        //                if (testBank != null)
        //                {
        //                    testBank.LastUpdatedBy = input.LastUpdatedBy;
        //                    testBank.LastUpdatedDate = input.LastUpdatedDate;
        //                    testBank.CategoryTypeId = input.CategoryTypeId;
        //                    testBank.Description = input.Description;
        //                    testBank.Duration = input.Duration;
        //                    testBank.Instructions = input.Instructions;
        //                    testBank.LavelTypeId = input.LavelTypeId;
        //                    testBank.StatusId = input.StatusId;
        //                    testBank.TestTypeId = input.TestTypeId;
        //                    testBank.TotalMarks = input.TotalMarks;

        //                    context.SaveChanges();
        //                    dbContextTransaction.Commit();
        //                    isUpdated = true;
        //                }
        //            }
        //            catch
        //            {
        //                dbContextTransaction.Rollback();
        //                throw;
        //            }
        //        }
        //        return isUpdated;
        //    }
        //}

        public IQueryable<TestBankModel> Retrieve(Guid testBankId)
        {
            try
            {
                var query = Select().Where(x => x.TestBankId == testBankId).AsQueryable<TestBankModel>();
                return query;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<TestBankModel> Select(Func<TestBankModel, bool> condition)
        {
            try
            {
                var query = Select().Where(condition).AsQueryable<TestBankModel>();
                return query;
            }
            catch
            {
                throw;
            }
        }

        private IQueryable<TestBankModel> Select()
        {
            var query = (from x in _context.TestBank
                         join p in _context.TypeDef on x.CategoryTypeValue equals p.Value
                         join q in _context.TypeDef on x.LevelTypeValue equals q.Value
                         join r in _context.TypeDef on x.TestTypeValue equals r.Value
                         select new TestBankModel
                         {
                             CreatedBy = x.CreatedBy,
                             CreatedDate = x.CreatedDate,
                             Description = x.Description,
                             LastUpdatedBy = x.LastUpdatedBy,
                             LastUpdatedDate = x.LastUpdatedDate,
                             StatusId = x.StatusId,
                             CategoryTypeValue = x.CategoryTypeValue,
                             Duration = x.Duration,
                             Instructions = x.Instructions,
                             LevelTypeValue = x.LevelTypeValue,
                             TestBankId = x.TestBankId,
                             TestTypeValue = x.TestTypeValue,
                             TotalMarks = x.TotalMarks,
                             CategoryTypeDescription = p.Description,
                             LavelTypeDescription = q.Description,
                             TestTypeDescription = r.Description,
                         });

            return query;
        }
    }
}
