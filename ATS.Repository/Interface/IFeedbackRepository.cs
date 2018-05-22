using System;
using System.Collections.Generic;
using System.Linq;
using ATS.Repository.Model;

namespace ATS.Repository.Interface
{
    public interface IFeedbackRepository : IRepository<UserFeedback>
    {
        bool Disable(List<Guid> listId);
        IQueryable<UserFeedback> Retrieve(Guid Id);
        IQueryable<UserFeedback> Select(Func<UserFeedback, bool> condition);
        int Count();
    }
}