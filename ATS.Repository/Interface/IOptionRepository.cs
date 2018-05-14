using ATS.Core.Model;
using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using System.Linq;

namespace ATS.Repository.Interface
{
    public interface IOptionRepository : IRepository<QuestionOption>
    {
        IQueryable<QuestionOptionModel> Select(Func<QuestionOptionModel, bool> condition);
    }
}
