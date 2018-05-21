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
                             LevelTypeDescription = q.Description,
                             TestTypeDescription = r.Description,
                         });

            return query;
        }
    }
}
