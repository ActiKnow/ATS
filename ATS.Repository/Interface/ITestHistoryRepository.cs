using ATS.Core.Model;
using ATS.Repository.Model;
using System;
using System.Linq;

namespace ATS.Repository.Interface
{
    public interface ITestHistoryRepository : IRepository<UserTestHistory>
    {
        IQueryable<UserTestHistoryModel> Retrieve(Guid qId);
        IQueryable<UserTestHistoryModel> Select(Func<UserTestHistoryModel, bool> condition);
    }
}
