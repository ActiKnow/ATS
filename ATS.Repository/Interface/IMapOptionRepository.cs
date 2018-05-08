using ATS.Core.Model;
using ATS.Repository.DAO;
using System;
using System.Collections.Generic;

namespace ATS.Repository.Interface
{
   public interface IMapOptionRepository : ICRUD<QuestionOptionMapModel>
    {
        void Create(QuestionOptionMapModel input, ATSDBContext context);
        void Update(QuestionOptionMapModel input, ATSDBContext context);
        void Delete(QuestionOptionMapModel input, ATSDBContext context);
        List<QuestionOptionMapModel> Select(ATSDBContext context, Func<QuestionOptionMapModel, bool> condition);
    }
}
