using ATS.Core.Model;
using ATS.Repository.Model;
using System;
using System.Linq;

namespace ATS.Repository.Interface
{
    public interface IAttemptHistoryRepository : IRepository<UserAttemptedHistory>
    {
        IQueryable<UserAttemptHistoryModel> Retrieve(Guid qId);
        IQueryable<UserAttemptHistoryModel> Select(Func<UserAttemptHistoryModel, bool> condition);
    }
}
