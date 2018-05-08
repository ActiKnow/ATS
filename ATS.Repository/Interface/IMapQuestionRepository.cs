using ATS.Core.Model;
using ATS.Repository.DAO;
using ATS.Repository.Model;
using System;
using System.Collections.Generic;

namespace ATS.Repository.Interface
{
    public interface IMapQuestionRepository : ICRUD<TestQuestionMapModel>
    {
        void Create(ref TestQuestionMapModel input, ATSDBContext context);
        bool Create(List<TestQuestionMapModel> inputs);
        void Delete(TestQuestionMapModel input, ATSDBContext context);
        bool Delete(List<TestQuestionMapModel> inputs);
        void Update(TestQuestionMapModel input, ATSDBContext context);
        List<TestQuestionMapModel> Select(ATSDBContext context, Func<TestQuestionMapModel, bool> condition);
    }
}
