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
    public class AttemptHistoryRepository : Repository<UserAttemptedHistory>, IAttemptHistoryRepository
    {
        private readonly ATSDBContext _context;
        public AttemptHistoryRepository(ATSDBContext context) : base(context)
        {
            this._context = context;
        }

        public IQueryable<UserAttemptHistoryModel> Retrieve(Guid qId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserAttemptHistoryModel> Select(Func<UserAttemptHistoryModel, bool> condition)
        {
            try
            {
                var query = (from x in _context.UserAttemptedHistory
                             select new UserAttemptHistoryModel
                             {
                                 History_Id = x.History_Id,
                                 Id = x.Id,
                                 Description = x.Description,
                                 OptionSelected_Id = x.OptionSelected_Id,
                                 QId = x.QId
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
