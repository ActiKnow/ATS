using ATS.Core.Model;
using ATS.Repository.DAO;
using System;
using System.Collections.Generic;

namespace ATS.Repository.Interface
{
    interface IOptionRepository : ICRUD<QuestionOptionModel>
    {
        void CreateTask(ref QuestionOptionModel input, ATSDBContext context);
        void UpdateTask(QuestionOptionModel input, ATSDBContext context);
        void DeleteTask(QuestionOptionModel input, ATSDBContext context);
        List<QuestionOptionModel> SelectTask( ATSDBContext context, Func<QuestionOptionModel, bool> condition);
    }
}
