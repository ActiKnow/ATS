using ATS.Core.Model;
using ATS.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATS.Repository.Interface
{
    public interface IQuestionRepository : IRepository<QuestionBank>
    {
        IQueryable<QuestionBankModel> Retrieve(Guid qId);
        IQueryable<QuestionBankModel> Select(Func<QuestionBankModel,bool> condition);
    }
}
