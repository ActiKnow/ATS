using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Model;
using ATS.Repository.Interface;
using ATS.Repository.Model;

namespace ATS.Repository.Repo
{
    public class TestHistoryRepository : Repository<UserTestHistory>, ITestHistoryRepository
    {
        private readonly ATSDBContext _context;
        public TestHistoryRepository(ATSDBContext context) : base(context)
        {
            this._context = context;
        }

        public IQueryable<UserTestHistoryModel> Retrieve(Guid historyId)
        {
            try
            {
                return Select(x => x.HistoryId == historyId).AsQueryable();
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<UserTestHistoryModel> Select(Func<UserTestHistoryModel, bool> condition)
        {
            try
            {
                var query = (from x in _context.UserTestHistory
                             select new UserTestHistoryModel
                             {
                               HistoryId = x.HistoryId,
                                TestbankId = x.TestbankId,
                                 UserId = x.UserId,
                                 AssignedDate=x.AssignedDate,
                                 LastUsedDate=x.LastUsedDate,
                                 IsFinished=x.IsFinished,
                                 ReusableDate=x.ReusableDate,
                                 TotalDuration=x.TotalDuration
                             }).Where(condition).AsQueryable();

                return query;
            }
            catch
            {
                throw;
            }
        }

    }
}
