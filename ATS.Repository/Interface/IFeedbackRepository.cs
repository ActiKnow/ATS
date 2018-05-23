using System;
using System.Collections.Generic;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Model;

namespace ATS.Repository.Interface
{
    public interface IFeedbackRepository : IRepository<UserFeedback>
    {
        bool Disable(List<Guid> listId);
        IQueryable<UserFeedbackModel> Retrieve(Guid Id);
        IQueryable<UserFeedbackModel> Select(Func<UserFeedback, bool> condition);
        int Count(Func<UserFeedback, bool> condition);
    }
}