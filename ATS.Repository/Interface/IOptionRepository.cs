using ATS.Core.Model;
using ATS.Repository.DAO;
using System;
using System.Collections.Generic;

namespace ATS.Repository.Interface
{
    interface IOptionRepository : ICRUD<QuestionOptionModel>
    {
        void Create(ref QuestionOptionModel input, ATSDBContext context);
        void Update(QuestionOptionModel input, ATSDBContext context);
        void Delete(QuestionOptionModel input, ATSDBContext context);
        List<QuestionOptionModel> Select( ATSDBContext context, Func<QuestionOptionModel, bool> condition);
    }
}
