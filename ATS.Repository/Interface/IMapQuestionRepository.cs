using ATS.Core.Model;
using ATS.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATS.Repository.Interface
{
    public interface IMapQuestionRepository : IRepository<TestQuestionMapping>
    {
        bool DeleteMappedQuestions(List<TestQuestionMapping> inputs);
        bool MapQuestions(List<TestQuestionMapping> inputs);
        IQueryable<TestQuestionMapModel> Retrieve(Guid guid);
        IQueryable<TestQuestionMapModel> Select(Func<TestQuestionMapModel,bool> condition);
    }
}
